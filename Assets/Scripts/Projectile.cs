using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    public int bulletSpeed = 10;
    private Vector3 shootDirection;

    public void Initialize(Transform shipTransform)
    {
        shootDirection = (transform.position - shipTransform.position).normalized;
    }
    void Start()
    {
        Destroy(gameObject, 1.5f);
        bulletRB.linearVelocity = transform.forward * bulletSpeed * 3.5f;

    }
    private void OnCollisionEnter(Collision collision)
    {
       Destroy(gameObject);
    }
}
