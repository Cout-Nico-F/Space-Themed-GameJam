using UnityEngine;

public class PlayerMediator : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private HealthController healthController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private float fireRate;
    [SerializeField] private int maxHealth;

    private Collider2D _collider;
    private IInput _input;
    private Vector2 _direction;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _input = ServiceLocator.Instance.GetService<IInput>();
        healthController.Init(maxHealth);
        weaponController.Configure(fireRate);
    }


    private void Update()
    {
        _direction = _input.GetDirection();

        if (_input.IsActionFirePressed())
        {
            weaponController.TryShoot();
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move(_direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.RecieveDamage(100);
    }

    public void RecieveDamage(int amount)
    {
        var isDead = healthController.ReciveDamage(amount);
        if (isDead)
        {
            // si el PLayer muere lanzar evento de GameOver
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }
}
