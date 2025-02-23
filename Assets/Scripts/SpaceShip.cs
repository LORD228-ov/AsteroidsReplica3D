﻿using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;
    public Transform shipPosition;
    private float shootCD = 0.5f;
    public float ShipMovespeed = 1f;
    public float ShipRotationspeed = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;

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
        shootCD -= Time.deltaTime;
        if (Input.GetKeyDown("space") && shootCD <=0)
        {
            Shoot();
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
}
