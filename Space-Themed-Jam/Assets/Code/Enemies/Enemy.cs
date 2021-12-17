using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected Collider2D EnemyCollider;
    [SerializeField] private EnemyId id;
    [SerializeField] protected float Speed;
    [SerializeField] private int damage;

    protected Transform MyTransform;
    protected Rigidbody2D Rb;
    protected SpriteRenderer Renderer;

    public string Id => id.Value;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        MyTransform = transform;
        DoInit();
    }

    protected abstract void DoInit();

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
