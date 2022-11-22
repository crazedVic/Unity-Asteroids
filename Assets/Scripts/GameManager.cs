using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int level = 1;

    int asteroidBaseCount = 2;

    [SerializeField]
    [Range(1.0f, 4.0f)]
    float asteroidBaseSpeed = 2f;

    [SerializeField]
    GameObject asteroidsContainer; //empty container that will hold the spawned asteroids

    [SerializeField]
    BoxCollider2D boundsCollider; // need to assign this to prefabs as they are instantiated.

    public GameObject[] asteroids;

    List<int> spawns = new List<int>();

    private void Awake()
    {
        Player.gameOver += ResetGame;
    }
    // Start is called before the first frame update
    void Start()
    {
        // initially let's instantiate some asteroids on the screen
        SpawnAsteroids();
    }

    void ResetGame()
    {
        level = 1;
        spawns.Clear();
        ClearAsteroids();
        SpawnAsteroids();
    }

    // Update is called once per frame
    void Update()
    {
        if(asteroidsContainer.transform.childCount <= 0)
        {
            level++;
            SpawnAsteroids();
        }
    }

    void LevelCleared()
    {
        // run this when level is cleared of all asteroids

        level++;
        spawns.Clear();
        SpawnAsteroids();
    }

    void ClearAsteroids()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("asteroid");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

    void SpawnAsteroids()
    {
        // based on current level, spawn asteroids
        int spawnCount = asteroidBaseCount + (level*2);
        for (int m = 0; m < spawnCount; m++)
        {
            int prefabIndex = Random.Range(0, 3);
           
            // get a unique spawnlocation
            int spawnRoll;
            do
            {
                spawnRoll = Random.Range(0, ScreenBounds.spawnLocations.Count);
            }
            while (spawns.Contains(spawnRoll));

            // instantiate new asteroid inside asteroidContainer
            GameObject asteroid = Instantiate(asteroids[prefabIndex]);
            asteroid.GetComponent<Asteroid>().boundsCollider = boundsCollider;
            asteroid.GetComponent<Asteroid>().GetMoving(asteroidBaseSpeed, ScreenBounds.spawnLocations[spawnRoll]);
            asteroid.transform.parent = asteroidsContainer.transform;
          
        }
        
       
    }
}
