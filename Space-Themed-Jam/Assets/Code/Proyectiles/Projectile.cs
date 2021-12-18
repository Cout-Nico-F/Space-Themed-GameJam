﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileId id;
    [SerializeField] protected float Speed;
    [SerializeField] private float timeToDeactivate;
    [SerializeField] private int damage;

    protected Rigidbody2D Rb;
    protected Collider2D Collider2D;
    protected Transform MyTransform;
    protected bool Active;
    public string Id => id.Value;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

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
        {
            damageable.RecieveDamage(damage);
            DeactivateProjectile();
        }
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
}