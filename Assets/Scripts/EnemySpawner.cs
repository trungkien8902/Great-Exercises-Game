using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO; // this is our enemy prefab
    GameObject scoreUITextGO; // reference to the text UI game object

    float maxSpawnRateInSeconds = 3f; // when we run the game, enemy ships should be instantiate after 3 seconds.
    int count = 1, _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        // increase spawn rate every 15 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 15f);


    }

    // Update is called once per frame
    void Update()
    {

    }

    // function to spawn an 
    // create an enemy prefab positioned on the top edge of the screen, and randomly between the left and right edge of the screen
    void SpawnEnemy()
    {
        // this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        // this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Debug.Log("minX = " + min.x + "\tmaxX = " + max.x);

        if (count <= 5)
        {
            createEnemy((int)min.x, (int)(max.x - min.x) - 1, 1);  // 5 x (turn 1, create 1 enemy)
        }
        else if (count >= 6 && count <= 10)
        {
            createEnemy((int)min.x, (int)(max.x - min.x) / 2, 2); // 5 x (turn 2, create 2 enemy)
        }
        else if (count >= 11 && count <= 15)
        {
            createEnemy((int)min.x, (int)(max.x - min.x) / 3, 3); // 5 x (turn 3, create 3 enemy)
        }
        else
        {
            createEnemy(min.x + (float)0.5, (float)3.5, 5); // 5 x (turn 4++, create 5 enemy)
        }
        count++;

        // schedule when to spawn next enemy
        ScheduleNextEnemySpawn();
    }

    void createEnemy(float a, float t, float n)
    {
        // Get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        _score = scoreUITextGO.GetComponent<GameScore>().Score;

        if ((_score >= 800 && _score < 2000) || ((_score >= 3000 && _score < 5000)))
        {
            GameObject anEnemy;
            // instantiate an enemy
            anEnemy = (GameObject)Instantiate(EnemyGO);
            anEnemy.transform.position = new Vector2(-8, 1); // an enemy goes left to right on screen
        }
        else
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            GameObject anEnemy;
            float b = a + t;

            for (int i = 0; i < n; ++i)
            {
                // instantiate an enemy
                anEnemy = (GameObject)Instantiate(EnemyGO);
                anEnemy.transform.position = new Vector2(Random.Range(a, b), max.y);

                if (b + t < (int)max.x)
                {
                    a = b + (float)1;
                    b += t;
                }
                else
                {
                    a = b + (float)0.7;
                    b = max.x - (float)0.5;
                }
            }
        }
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            // pick a number between 1 and maxSpawnRateInSeconds
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInNSeconds = 1f;
        }
        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    // function to increase the dificulty of the game
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
            
        }
        
        if (maxSpawnRateInSeconds == 1f)
        {
            
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    // function to start enemy spawner
    public void ScheduleEnemySpawner()
    {
        // reset max spawn rate
        maxSpawnRateInSeconds = 3f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        // increase spawn rate every 15 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 15f);
    }

    // function to stop enemy spawner
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
