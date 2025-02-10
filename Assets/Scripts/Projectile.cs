using UnityEngine;

public class Proje : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    public float bulletSpeed = 5f;
    public Transform bulletSpawnPosition2;
    void Start()
    {
        
    }

    void Update()
    {
        bulletRB.linearVelocity = bulletSpawnPosition2.forward * bulletSpeed;
    }
}
