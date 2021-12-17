using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyId id;
    [SerializeField] protected Rigidbody2D Rb;
    [SerializeField] protected float Speed;
    [SerializeField] private int damage;

    protected Transform MyTransform;

    public string Id => id.Value;

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


    public void AddDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
}
