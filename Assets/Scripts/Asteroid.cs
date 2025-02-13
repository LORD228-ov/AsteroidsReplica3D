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
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionRadius = 2f;
    //void Start()
    //{
    //    asteroidLevel = Random.Range(1,4);

    //    switch (asteroidLevel)
    //    {
    //        case 1:
    //            asteroidX = 1f;
    //            asteroidY = 1f;
    //            asteroidZ = 1f;
    //            break;
    //        case 2:
    //            asteroidX = 2f;
    //            asteroidY = 2f;
    //            asteroidZ = 2f;
    //            break;
    //        case 3:
    //            asteroidX = 3f;
    //            asteroidY = 3f;
    //            asteroidZ = 3f;
    //            break;
    //    }

    //    Vector3 newScale = new Vector3(asteroidX, asteroidY, asteroidZ);
    //    transform.localScale = newScale;

    //    float randomRotX = Random.Range(minRotation.x, maxRotation.x);
    //    float randomRotY = Random.Range(minRotation.y, maxRotation.y);
    //    float randomRotZ = Random.Range(minRotation.z, maxRotation.z);

    //    transform.rotation = Quaternion.Euler(randomRotX, randomRotY, randomRotZ);
    //}
    void Start()
    {
        asteroidLevel = Random.Range(1, 4);
        SetAsteroidSize();
        SetRandomRotation();
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

    private void SetRandomRotation()
    {
        transform.rotation = Quaternion.Euler(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (asteroidLevel == 3)
        //{
        //    Vector3 spawnPos = gameObject.transform.position;

        //}
        //else if (asteroidLevel == 2)
        //{

        //} else if (asteroidLevel == 1)
        //{
        //    Destroy(gameObject);
        //}
        Explode();
    }

    private void Explode()
    {
        Vector3 explosionPos = transform.position;

        if (asteroidRB != null)
        {
            asteroidRB.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
        }

        if (asteroidLevel == 3)
        {
            SpawnAsteroids(2, 2);
        }
        else if (asteroidLevel == 2)
        {
            SpawnAsteroids(1, 1);
        }

        Destroy(gameObject);
    }

    private void SpawnAsteroids(int newLevel, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.5f;
            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();
            if (asteroidScript != null)
            {
                asteroidScript.asteroidLevel = newLevel;
                asteroidScript.SetAsteroidSize();
            }
        }
    }
}
