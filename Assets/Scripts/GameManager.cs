using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    [SerializeField] public List<GameObject> spawnedAsteroids = new List<GameObject>();
    private float spawnCD = 3.5f;
    private int asteroidRequired = 3;
    public Asteroid asteroid;

    void Start()
    {
        StartNewRound();   
    }

    void Update()
    {
    }
    void StartNewRound() {
        asteroid.SpawnAsteroids(1, asteroidRequired);
        //for (int i = 0; i < asteroidRequired; ++i)
        //{

        //}
    }
    void EndRound()
    {

    }

    public void RemoveAsteroid(GameObject asteroidToRemove)
    {
        //doe iets met asteroidToRemove
    }

}
