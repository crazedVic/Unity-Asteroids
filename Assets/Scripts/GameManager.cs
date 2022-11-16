using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int level = 1;

    int asteroidBaseCount = 2;

    [SerializeField]
    GameObject asteroidsContainer; //empty container that will hold the spawned asteroids

    [SerializeField]
    BoxCollider2D boundsCollider; // need to assign this to prefabs as they are instantiated.

    public GameObject[] asteroids;

    // Start is called before the first frame update
    void Start()
    {
        // initially let's instantiate some asteroids on the screen
        SpawnAsteroids();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelCleared()
    {
        // run this when level is cleared of all asteroids
        level++;
        SpawnAsteroids();

    }

    void SpawnAsteroids()
    {
        // based on current level, spawn asteroids
        int spawnCount = asteroidBaseCount + level;
        for (int m = 0; m < spawnCount; m++)
        {
            int prefabIndex = Random.Range(0, 3);
            // instantiate new asteroid inside asteroidContainer
            GameObject asteroid = Instantiate(asteroids[prefabIndex]);
            asteroid.GetComponent<Asteroid>().boundsCollider = boundsCollider;
            asteroid.GetComponent<Asteroid>().GetMoving();
            asteroid.transform.parent = asteroidsContainer.transform;
          
        }
        
       
    }
}
