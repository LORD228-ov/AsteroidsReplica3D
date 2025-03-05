using NUnit.Framework;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody asteroidRB;
    [SerializeField] private GameObject asteroidPrefab;
    private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 maxScale = new Vector3(2f, 2f, 2f);
    private Vector3 minRotation = new Vector3(0f, 0f, 0f);
    private Vector3 maxRotation = new Vector3(360f, 360f, 360f);
    private float asteroidX;
    private float asteroidY;
    private float asteroidZ;
    public int asteroidLevel;
    private int[] asteroidScoreCost = {25, 35, 50};
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionRadius = 2f;
    public GameManager gameManager;
    [SerializeField] private BoxCollider playerCollider;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        //GameObject obj = GameObject.Find("Player");
        //if (obj != null)
        //{
        //    playerCollider = obj.GetComponent<BoxCollider>();
        //    Debug.Log("Initiated");
        //}
        asteroidRB = GetComponent<Rigidbody>();
        //gameManager = GetComponent<GameManager>();
        asteroidLevel = Random.Range(1, 4);
        SetAsteroidSize();
        SetRandomRotation();
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPos;
        do
        {
            Vector3 randomOffset = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            spawnPos = transform.position + randomOffset;
        }
        while (IsInsidePlayerCollider(spawnPos));

        return spawnPos;
    }

    //private bool IsInsidePlayerCollider(Vector3 position)
    //{
    //    if (playerCollider == null) return false;

    //    Bounds bounds = playerCollider.bounds;
    //    return bounds.Contains(position);
    //}
    private bool IsInsidePlayerCollider(Vector3 position)
    {
        if (playerCollider == null) return false;

        Collider[] colliders = Physics.OverlapBox(playerCollider.bounds.center, playerCollider.bounds.extents);
        foreach (Collider col in colliders)
        {
            if (col == playerCollider) return true;
        }
        return false;
    }

    private void SetAsteroidSize()
    {
        switch (asteroidLevel)
        {
            case 1:
                asteroidX = 1f;
                asteroidY = 1f;
                asteroidZ = 1f;
                break;
            case 2:
                asteroidX = 2f;
                asteroidY = 2f;
                asteroidZ = 2f;
                break;
            case 3:
                asteroidX = 3f;
                asteroidY = 3f;
                asteroidZ = 3f;
                break;
        }
        transform.localScale = new Vector3(asteroidX, asteroidY, asteroidZ);
    }

    private Quaternion SetRandomRotation()
    {
        //transform.rotation = Quaternion.Euler(
        //    Random.Range(0f, 360f),
        //    Random.Range(0f, 360f),
        //    Random.Range(0f, 360f)
        //);

        Quaternion randomRotation = Quaternion.Euler(
    Random.Range(0f, 360f),
    Random.Range(0f, 360f),
    Random.Range(0f, 360f)
);
        return randomRotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        //gameManager.enabled = true;
        Vector3 explosionPos = transform.position;

        if (asteroidRB != null)
        {
            asteroidRB.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
        }

        if (asteroidLevel == 3)
        {
            SpawnAsteroids(2, 1);
            if (gameManager != null && gameManager.isActiveAndEnabled)
            {
                gameManager.ScoreUp(asteroidScoreCost[0]);
                //gameManager.currentValue += asteroidScoreCost;

            }
            else
            {
                Debug.LogError("GameManager is null!");
            }
        }
        else if (asteroidLevel == 2)
        {
            SpawnAsteroids(1, 1);
            if (gameManager != null && gameManager.isActiveAndEnabled)
            {
                gameManager.ScoreUp(asteroidScoreCost[1]);

            }
            else
            {
                Debug.LogError("GameManager is null!");
            }

        }
        else if (asteroidLevel == 1)
        {
            //Destroy(gameObject);
            if (gameManager != null && gameManager.isActiveAndEnabled)
            {
                gameManager.ScoreUp(asteroidScoreCost[2]);
            }
            else
            {
                Debug.LogError("GameManager is null!");
            }


        }

        Destroy(gameObject);
        gameManager.spawnedAsteroids.Remove(gameObject);
    }

    public void SpawnAsteroids(int newLevel, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPos, SetRandomRotation());
            Vector3 randomDirection = new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z).normalized;
            float randomSpeed = Random.Range(1f, 5f);
            Rigidbody newAsteroidRB = newAsteroid.GetComponent<Rigidbody>();
            newAsteroidRB.linearVelocity = randomDirection * randomSpeed * 2;
            gameManager.spawnedAsteroids.Add(newAsteroid);
            Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();
            asteroidScript.gameManager = gameManager;
            asteroidScript.asteroidLevel = newLevel;
            asteroidScript.SetAsteroidSize();
        }
    }
}
