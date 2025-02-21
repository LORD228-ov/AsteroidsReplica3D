using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    public int bulletSpeed = 10;
    private Vector3 shootDirection;
    private float particleTime = 1.2f;
    [SerializeField] private GameObject particlePrefab;

    public void Initialize(Transform shipTransform)
    {
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
        GameObject particle = Instantiate(particlePrefab, transform.position, transform.rotation);
        Destroy(particle, particleTime);

    }
}
