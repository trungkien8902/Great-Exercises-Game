using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO; // reference to the text UI game object

    public GameObject ExplosionGO; // this is our explosion prehab
    float speed; // for the enemy speed
    int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        _score = scoreUITextGO.GetComponent<GameScore>().Score;

        speed = speedingUp(_score); // set speed
        //Debug.Log("speed = " + speed + "\tscore = " + _score);
    }

    // Update is called once per frame
    void Update()
    {
        // get the enemy current position
        Vector2 position = transform.position;

        if ((_score >= 800 && _score < 2000) || ((_score >= 3000 && _score < 5000)))
        {
            // compute the enemy new position
            position = new Vector2(position.x + speed * Time.deltaTime, position.y);

            // update the enemy position
            transform.position = position;

            // this is the bottom-left point of the screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(-10, 10));

            // if the enemy went outside the screen on the bottom, then destroy the enemy
            if (transform.position.y > min.y)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // compute the enemy new position
            position = new Vector2(position.x, position.y - speed * Time.deltaTime);

            // update the enemy position
            transform.position = position;

            // this is the bottom-left point of the screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            // if the enemy went outside the screen on the bottom, then destroy the enemy
            if (transform.position.y < min.y)
            {
                Destroy(gameObject);
            }
        }
    }

    float speedingUp(int _score) // increase the enemy speed
    {
        float speed = 2f;
        if (_score < 800)
        {
            speed = 2f;
        }
        else if (_score >= 800 && _score < 8000)
        {
            speed = 3f;
        }
        else if (_score >= 8000 && _score < 12000)
        {
            speed = 4f;
        }
        else
        {
            speed = 5f;
        }

        return speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // detect collision of the enemy ship with the player ship, or with a player's bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            // add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            _score = scoreUITextGO.GetComponent<GameScore>().Score;
            Destroy(gameObject); // destroy this enemy ship
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
