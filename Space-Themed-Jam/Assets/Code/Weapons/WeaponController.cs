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

    
    public void TryShoot()
    {
        if (Time.time > _timeBetweenShoots)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var projectile = _projectileFactory.SpawnFromPool(defaultProyectile.Value, projectileSpawnpoint.position, projectileSpawnpoint.rotation);
        _timeBetweenShoots = Time.time + fireRate;
    }
}
