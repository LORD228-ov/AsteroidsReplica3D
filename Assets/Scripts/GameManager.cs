using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    [SerializeField] public List<GameObject> spawnedAsteroids = new List<GameObject>();
    private int asteroidRequired = 3;
    public Asteroid asteroid;
    public int currentValue = 30;
    private int targetValue = 30;
    public TextMeshProUGUI numberText;
    [SerializeField] private GameObject gameManagerObj;
    private Coroutine updateCoroutine;

    void Start()
    {
        StartNewRound();
        //ScoreUp(currentValue);
        UpdateText();

    }

    //void Update()
    //{
    //    UpdateText();
    //}
    void StartNewRound()
    {
        spawnedAsteroids.Clear();
        asteroid.SpawnAsteroids(1, asteroidRequired);

        foreach (GameObject ast in GameObject.FindGameObjectsWithTag("asteroid"))
        {
            if (!spawnedAsteroids.Contains(ast))
            {
                spawnedAsteroids.Add(ast);
            }
        }
    }
    void EndRound()
    {

    }

    public void RemoveAsteroid(GameObject asteroidToRemove)
    {
        //doe iets met asteroidToRemove
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


}



