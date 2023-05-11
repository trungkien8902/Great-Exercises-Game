using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerBulletGO; // this is put player's bullet prefabs
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO; // this is pur explosion prehab

    public static LevelManager pause;

    private AudioSource music;
    public Text LivesUIText; // reference to the lives UI text
    const int MaxLives = 5; // maximum player lives
    int lives = MaxLives; // current player lives
    int check = 0, temp = 0;
    public float speed;    // speed of our player ship

    float accelStartY; // to get the accelerometer y value at the start of the game

    public void Init()
    {
        lives = MaxLives;

        // update the lives UI text
        LivesUIText.text = lives.ToString();

        // reset the player position to the center of the screen
        transform.position = new Vector2(0, 0);

        // set this player game object to active
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString();

        gameObject.SetActive(true);

        // get the initial accelerometer y value
        accelStartY = Input.acceleration.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        //PC
        // fire bullets when the spacebar is pressed
        if (Input.GetKeyDown("space"))
        {
            // play the laser sound effect
            music = GetComponent<AudioSource>();
            music.Play();

            // instantiate the first bullet
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;  // set the bullet initial position

            // instantiate the second bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;   // set the bullet initial position
        }

        // get the input from the keyboard with AWSD or the arrows
        float x = Input.GetAxisRaw("Horizontal"); // the value will be -1, 0, or 1 (for left, no imput and right)
        float y = Input.GetAxisRaw("Vertical"); // the value will be -1, 0, 1, (for down, no input and up)
        // now based on the input we compute a direction vector, and we normalize it to get a unit vector
        Vector2 direction = new Vector2(x, y).normalized;
        

        ////Mobile
        //// get input from the accelerometer ...
        //float x = Input.acceleration.x;
        //float y = Input.acceleration.y - accelStartY;
        //// create a vector with the accelerometer input values
        //Vector2 direction = new Vector2(x, y);



        temp += 1;
        if (temp > 1000 && temp < 1400)
        {
            check = 1;
            // instantiate the first bullet
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;  // set the bullet initial position

            // instantiate the second bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;   // set the bullet initial position
        }
        if (check == 1 && temp >= 1500)
        {
            check = 0;
            temp = 0;
            lives += 1;
            LivesUIText.text = lives.ToString(); // update lives UI text
        }

        // clamp the length of the vector to a maximum of 1...       
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        // now we call the function that computes and sets the player's position
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        // find the screen limits to the player's movement (left, right, top and bottom edges of the screen) 
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // this is the bottom-left point (corner) of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // this is the top-right point (corner) of the screen

        max.x = max.x - 0.225f; // subtracct the player sprite half width
        min.x = min.x + 0.225f; // add the player sprite half width

        max.y = max.y - 0.285f; // subtract the player sprite half height
        min.y = min.y + 0.285f; // add the player sprite hald height

        Vector2 pos = transform.position; // Get the player's current position

        pos += direction * speed * Time.deltaTime; // Calculate the new position

        // Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // update the player's position
        transform.position = pos;
    }

    public void Shoot()
    {
        // play the laser sound effect
        music = GetComponent<AudioSource>();
        music.Play();

        // instantiate the first bullet
        GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
        bullet01.transform.position = bulletPosition01.transform.position;  // set the bullet initial position

        // instantiate the second bullet
        GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
        bullet02.transform.position = bulletPosition02.transform.position;   // set the bullet initial position
    }

    // this function will trigger when their is a collisionof our game objects.
    void OnTriggerEnter2D(Collider2D col)
    {
        // detect collision of the player ship with an enemy ship, or with an enemy bullet
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--; // subtract one live
            LivesUIText.text = lives.ToString(); // update lives UI text

            if (lives == 0) // destroy the player's ship
            {
                SceneManager.LoadScene("Win Screen");
                gameObject.SetActive(false); // hide the player's ship
            }
        }
    }

    // function to instantiate an explosion
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        // set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
