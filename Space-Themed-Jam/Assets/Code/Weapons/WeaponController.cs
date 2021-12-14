using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectilesConfiguration projectilesConfiguration;
    [SerializeField] private ProjectileId defaultProyectile;
    [SerializeField] private Transform projectileSpawnpoint;
    [SerializeField] private float fireRate;

    private ProjectileFactory _projectileFactory;
    private float _timeBetweenShoots;

    private void Awake()
    {
        var instance = Instantiate(projectilesConfiguration);
        _projectileFactory = new ProjectileFactory(instance);
        _projectileFactory.Init();
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
            var projectile = _projectileFactory.SpawnFromPool(defaultProyectile.Value, projectileSpawnpoint.position, projectileSpawnpoint.rotation);
            _timeBetweenShoots = Time.time + fireRate;
        }
    }
}
