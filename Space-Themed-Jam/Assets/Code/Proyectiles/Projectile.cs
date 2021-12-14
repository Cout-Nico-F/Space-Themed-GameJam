using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IDamageable
{
    [SerializeField] private ProjectileId id;
    [SerializeField] protected Rigidbody2D Rb;
    [SerializeField] protected float Speed;
    [SerializeField] private float timeToDeactivate;
    [SerializeField] private int damage;

    protected Transform MyTransform;
    protected bool Active;
    public string Id => id.Value;

        
    public void Init(Vector3 position, Quaternion rotation)
    {
        MyTransform = transform;
        Active = true;
        DoInit(position, rotation);
        StartCoroutine(DeactivateIn(timeToDeactivate));
    }

    protected abstract void DoInit(Vector3 position, Quaternion rotation);

    private void FixedUpdate()
    {
        DoMove();
    }

    protected abstract void DoMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.AddDamage(damage);
    }
    
    private IEnumerator DeactivateIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DeactivateProjectile();
    }

    private void DeactivateProjectile()
    {
        DoDeactivate();
        Active = false;
        gameObject.SetActive(false);
    }

    protected abstract void DoDeactivate();
        
    public void AddDamage(int amount)
    {
        DeactivateProjectile();
    }
}