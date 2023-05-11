using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speedBullet; // the bullet speed
    Vector2 _direction; // the direction of the bullet
    bool isReady; // to know when the bullet direction is set
    int count = 0;

    // set default values in Awake function
    public void Awake()
    {
        if (count < 5)
        {
            speedBullet = 8f;
        }
        else if (count >= 5 && count <= 15)
        {
            speedBullet = 10f;
        }
        else if (count >= 16 && count <= 30)
        {
            speedBullet = 12f;
        }
        else
        {
            speedBullet = 15f;
        }
        isReady = false;

        count++;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // function to set the bullet's direction
    public void SetDirection(Vector2 direction)
    {
        // set the direction normalized, to get an unit vector
        _direction = direction.normalized;
        isReady = true; // set flag to true
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            // get the bullet's current position
            Vector2 position = transform.position;

            // compute the bullet's new position
            position += _direction * speedBullet * Time.deltaTime;

            // update the bullet's position
            transform.position = position;

            // Next we need to remove the bullet form outr game
            // if the bullet goes outside the screen

            // this is the bottom-left point of the screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(-1, -1));

            // this is the top-right point of the screen
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(2, 2));

            // if the bulet goes outside the screen, then destroy it
            if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
                (transform.position.x < min.y) || (transform.position.x > max.y))
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // detect collision of an enemy's bullet with the player ship
        if (col.tag == "PlayerShipTag")
        {
            Destroy(gameObject); // destroy this enemy's bullet
        }
    }
}
