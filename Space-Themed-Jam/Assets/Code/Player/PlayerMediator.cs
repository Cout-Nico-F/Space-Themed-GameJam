using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediator : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private WeaponController weaponController;

    private IInput _input;
    private Vector2 _direction;


    private void Start()
    {
        _input = ServiceLocator.Instance.GetService<IInput>();
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
}
