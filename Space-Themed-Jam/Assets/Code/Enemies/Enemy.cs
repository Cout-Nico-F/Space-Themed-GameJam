using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected Collider2D EnemyCollider;
    [SerializeField] private EnemyId id;
    [SerializeField] protected float Speed;
    [SerializeField] private int damage;
    [SerializeField] private int pointsToAdd;

    protected Transform MyTransform;
    protected Rigidbody2D Rb;
    protected SpriteRenderer Renderer;
    protected HealthController HealthController;

    public string Id => id.Value;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        HealthController = GetComponent<HealthController>();
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
            damageable.RecieveDamage(damage);
    }


    public void RecieveDamage(int amount)
    {
        var isDead = HealthController.ReciveDamage(amount);
        if (isDead)
        {
            Destroy(gameObject);
            var enemyDestroyedEvent = new EnemyDestroyedEvent(pointsToAdd, GetInstanceID());
            ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(enemyDestroyedEvent);
        }
    }
}
