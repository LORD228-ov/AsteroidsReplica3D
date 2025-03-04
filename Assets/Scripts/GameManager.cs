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
    private float spawnCD = 3.5f;
    private int asteroidRequired = 3;
    public Asteroid asteroid;
    public int currentValue;
    private int targetValue = 0;
    public TextMeshProUGUI numberText;
    [SerializeField] private GameObject gameManagerObj;
    private Coroutine updateCoroutine;

    void Start()
    {
        StartNewRound();
        gameManagerObj.gameObject.SetActive(true);
        ScoreUp(currentValue);

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
    private void UpdateText()
    {
        numberText.text = currentValue.ToString();
    }

    public void ScoreUp(int amount)
    {
        targetValue += amount;
        if (updateCoroutine == null)
        {
            updateCoroutine = StartCoroutine(SmoothUpdate());
        }
    }

    private IEnumerator SmoothUpdate()
    {
        while (Mathf.Abs(targetValue - currentValue) > 0.01f)
        {
            currentValue = Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, Time.deltaTime * 5));
            UpdateText();
            yield return null;
        }
        currentValue = targetValue;
        UpdateText();
        updateCoroutine = null;
    }


    //private IEnumerator SmoothIncrease(int targetValue)
    //{
    //    int startValue = currentValue;
    //    float duration = 1f;
    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        float t = elapsed / duration;
    //        currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
    //        UpdateText();
    //        yield return null;
    //    }

    //    currentValue = targetValue;
    //    UpdateText();
    //}
}



