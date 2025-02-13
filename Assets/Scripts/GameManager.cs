using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    [SerializeField] private List<GameObject> spawnedAsteroids = new List<GameObject>();
    private float spawnCD = 3.5f;

    void Start()
    {
        StartNewRound();   
    }

    void Update()
    {
        //spawnCD -= Time.deltaTime;
        //if (spawnCD <= 0 )
        //{
        //    spawnCD = 3.5f;
        //}
    }
    void StartNewRound() {
    }
}
