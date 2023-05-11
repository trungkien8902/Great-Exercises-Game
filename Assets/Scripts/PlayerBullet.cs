using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed; // the speed of the bullet

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // get the bullet's current position
        Vector2 position = transform.position;

        // compute the bulet's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // update the bullet's position
        transform.position = position;

        // if the bullet goes outside the screen on the top, we need to destroy it so it doesn't keep using memory
        
        // this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // if the bullet went outside the creen on the top, then destrouy the bullet
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // detect collision of the player's bullet with an enemy ship
        if (col.tag == "EnemyShipTag")
        {
            Destroy(gameObject);
        }
    }
}
