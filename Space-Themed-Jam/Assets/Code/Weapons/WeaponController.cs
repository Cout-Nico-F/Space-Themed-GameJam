using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;
    [SerializeField] private float fireRate;

    private ObjectPool _objectPool;
    private float _timeBetweenShoots;

    private void Awake()
    {
        _objectPool = new ObjectPool("Projectile1", projectilePrefab, 20);
        _objectPool.Init();
    }

    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _timeBetweenShoots)
        {
            var projectile = _objectPool.SpawnFromPool(projectileSpawnpoint.position, projectileSpawnpoint.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = Vector3.right * 10f;
            _timeBetweenShoots = Time.time + fireRate;
        }
    }
}
