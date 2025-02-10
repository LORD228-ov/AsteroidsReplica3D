using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;
    private string stoel1 = "Student1";
    private string stoel2 = "Student2";
    public float ShipMovespeed = 1f;
    public float ShipRotationspeed = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        //StoelSwitch();
        //Debug.Log(stoel1 + " " + stoel2);
        //bulletPrefab = GetComponent<GameObject>();
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
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }

    private void StoelSwitch()
    {
        String buffer = stoel1;
        stoel1 = stoel2;
        stoel2 = buffer;
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, new Quaternion(0, 0, 0, 0));
    }
}
