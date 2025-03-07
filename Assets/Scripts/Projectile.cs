using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    public int bulletSpeed = 10;
    private Vector3 shootDirection;
    private float particleTime = 0.7f;
    [SerializeField] private GameObject particlePrefabAsteroid;

    public void Initialize(Transform shipTransform)
    {
        //shooting
        shootDirection = (transform.position - shipTransform.position).normalized;
    }
    void Start()
    {
        Destroy(gameObject, 1.5f);
        bulletRB.linearVelocity = transform.forward * bulletSpeed * 3.5f;

    }
     void Update()
    {
        //particleTime -= Time.deltaTime;  
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       Destroy(gameObject);
        if (collision.gameObject.CompareTag("asteroid"))
        {
            // particle on asteroid destroy
            GameObject particle = Instantiate(particlePrefabAsteroid, transform.position, transform.rotation);
            Destroy(particle, particleTime);
        }


    }
}
