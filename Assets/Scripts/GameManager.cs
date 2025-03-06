using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.HighDefinition.ScalableSettingLevelParameter;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    [SerializeField] public List<GameObject> spawnedAsteroids = new List<GameObject>();
    private int asteroidRequired = 2;
    private int asteroidLevel;
    public Asteroid asteroid;
    public int currentValue = 30;
    private int targetValue = 30;
    public TextMeshProUGUI numberText;
    [SerializeField] private GameObject gameManagerObj;
    private Coroutine updateCoroutine;
    [SerializeField] private GameObject ScoreBonus;
    [SerializeField] private GameObject SpeedBonus;
    public Vector3 spawnArea;


    void Start()
    {
        StartNewRound();
        //ScoreUp(currentValue);
        UpdateText();
    }

    void FixedUpdate()
    {
        if (spawnedAsteroids.Count == 0)
        {
            asteroidRequired++;
            StartNewRound();
        }
    }
    void StartNewRound()
    {
        spawnArea = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        spawnedAsteroids.Clear();
        SpawnBonus();
        asteroidLevel = Random.Range(1, 4);
        asteroid.SpawnAsteroids(asteroidLevel, asteroidRequired - 1);
        asteroidLevel = Random.Range(1, 4);
        asteroid.SpawnAsteroids(asteroidLevel, asteroidRequired - 1);

        foreach (GameObject ast in GameObject.FindGameObjectsWithTag("asteroid"))
        {
            if (!spawnedAsteroids.Contains(ast))
            {
                spawnedAsteroids.Add(ast);
            }
        }
    }
    private void UpdateText()
    {
        numberText.text = currentValue.ToString();
    }

    public void ScoreUp(int amount)
    {
        currentValue += amount;
        //if (updateCoroutine == null)
        //{
        //    updateCoroutine = StartCoroutine(target);
        //}
        UpdateText();
    }

    private IEnumerator SmoothUpdate()
    {
        while (currentValue != targetValue)
        {
            currentValue = (int)Mathf.MoveTowards(currentValue, targetValue, Time.deltaTime * 50);
            UpdateText();
            yield return null;
        }
        updateCoroutine = null;
    }

    void SpawnBonus()
    {
        int randomIndex = Random.Range(0, 2);

        GameObject bonusToSpawn = randomIndex == 0 ? ScoreBonus : SpeedBonus;

        //Vector3 spawnPosition = new Vector3(
        //    Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
        //    Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
        //    Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        //);

        Instantiate(bonusToSpawn, spawnArea, Quaternion.identity);
    }


}



