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
    public float shipMovespeed;
    public float shipRotationspeed;
    public Image[] livesImages;
    private int lives = 3;
    private bool isInvincible = false;
    private float invincibilityTime = 3f;
    private float blinkTime = 0.25f;
    private GameManager gameManager;
    [SerializeField] private MeshRenderer shipModel;
    public bool isSpeeded = false;
    [SerializeField] private GameObject particlePrefabPickup;
    private float particleTime = 0.7f;
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds;


    void Start()
    {
        //Initializing objects
        shipMovespeed = 2f;
        shipRotationspeed = 2f;
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        gameManager = FindObjectOfType<GameManager>();
        //shipModel = GetComponent<MeshRenderer>();

    }

    void FixedUpdate()
    {
        // moving player
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddRelativeForce(new Vector3(0, 0, shipMovespeed));
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
            rb.rotation *= Quaternion.AngleAxis(shipRotationspeed, Vector3.up);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.rotation *= Quaternion.AngleAxis(shipRotationspeed, Vector3.down);
        }
        Quaternion currentRotation = rb.rotation;
        rb.rotation = Quaternion.Euler(0f, currentRotation.eulerAngles.y, 0f);
    }
     void Update()
    {
        // some changes in cooldowns
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
        //creating bullet
        GameObject newBulletObject = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        Projectile newBullet = newBulletObject.GetComponent<Projectile>();

        if (newBullet != null)
        {
            // bullet sound
            audioSource.clip = sounds[1];
            audioSource.Play();
            newBullet.Initialize(shipPosition);
        }
        shootCD = 0.5f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //checking collisions
        if (collision.gameObject.CompareTag("asteroid") && !isInvincible)
        {
            ReportPlayerHit();
        }
       else if (collision.gameObject.CompareTag("pickup"))
        {
            GameObject particle = Instantiate(particlePrefabPickup, transform.position, transform.rotation);
            Destroy(particle, particleTime);
        }
    }
    public void ReportPlayerHit()
    {
        // player has been hitted
        if (lives > 0)
        {
            audioSource.clip = sounds[2];
            audioSource.Play();
            lives--;
            livesImages[lives].gameObject.SetActive(false);
            RespawnPlayer();

        }

        if (lives <= 0)
        {
            PlayerPrefs.SetInt("Score", gameManager.currentValue);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Credits");

        }
    }
    private void RespawnPlayer()
    {
        //  spawn in standart cordinate
        ShieldOn();
        rb.position = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.linearVelocity = Vector3.zero;
        
    }
    public void ShieldOn()
    {
        // making player inbincible
        isInvincible = true;
        invincibilityTime = 3f;
        blinkTime = 0.25f;
        shipModel.enabled = true;
    }
    public void Speedup()
    {
        // speed bonus
        audioSource.clip = sounds[0];
        audioSource.Play();
        shipMovespeed += 1f;
        shipRotationspeed += 1f;
    }
}
