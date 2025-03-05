using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;
    public Transform shipPosition;
    private float shootCD = 0.5f;
    public float ShipMovespeed = 1f;
    public float ShipRotationspeed = 1f;
    public Image[] livesImages;
    private int lives = 3;
    private bool isInvincible = false;
    private float invincibilityTime = 3f;
    private float blinkTime = 0.25f;
    [SerializeField] private MeshRenderer shipModel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        //shipModel = GetComponent<MeshRenderer>();

    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddRelativeForce(new Vector3(0, 0, ShipMovespeed));
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            rb.linearVelocity *= 0.98f;

            if (rb.linearVelocity.magnitude < 0.2f)
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.rotation *= Quaternion.AngleAxis(ShipRotationspeed, Vector3.up);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.rotation *= Quaternion.AngleAxis(ShipRotationspeed, Vector3.down);
        }
        Quaternion currentRotation = rb.rotation;
        rb.rotation = Quaternion.Euler(0f, currentRotation.eulerAngles.y, 0f);
    }
     void Update()
    {
        shootCD -= Time.deltaTime;
        if (Input.GetKeyDown("space") && shootCD <=0)
        {
            Shoot();
        }
        if (isInvincible)
        {
            //rb.rotation = Quaternion.identity;
            //rb.linearVelocity = Vector3.zero;
            invincibilityTime -= Time.deltaTime;
            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0f)
            {
                shipModel.enabled = !shipModel.enabled;
                blinkTime = 0.25f;
            }

            if (invincibilityTime <= 0f)
            {
                isInvincible = false;
                shipModel.enabled = true;
            }
        }

    }
    private void Shoot()
    {
        GameObject newBulletObject = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        Projectile newBullet = newBulletObject.GetComponent<Projectile>();

        if (newBullet != null)
        {
            newBullet.Initialize(shipPosition);
        }
        shootCD = 0.5f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("asteroid") && !isInvincible)
        {
            ReportPlayerHit();
        }
    }
    public void ReportPlayerHit()
    {
        if (lives > 0)
        {
            lives--;
            livesImages[lives].gameObject.SetActive(false);
            RespawnPlayer();

        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("Credits");

        }
    }
    private void RespawnPlayer()
    {
        isInvincible = true;
        invincibilityTime = 3f;
        blinkTime = 0.25f;
        rb.position = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.linearVelocity = Vector3.zero;
        shipModel.enabled = true;
    }
}
