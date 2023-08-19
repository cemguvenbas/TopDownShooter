using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFA.TPS.Movement;
using GFA.TPS.Input;
using UnityEngine.InputSystem;
using System;

namespace GFA.TPS.Mediators
{
    public class PlayerMediator : MonoBehaviour
    {
        private CharacterMovement _characterMovement;

        private GameInput _gameInput;

        [SerializeField]
        private float _dodgePower;

        private Plane _plane = new Plane(Vector3.up, Vector3.zero); // our ground should be flat to use Plane

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _gameInput = new GameInput();
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Dodge.performed += OnDodgeRequested;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
        }

        private void OnDodgeRequested(InputAction.CallbackContext obj)
        {
            _characterMovement.ExternalForces += _characterMovement.Velocity.normalized * _dodgePower;
        }

        private void Update()
        {
            var movementInput = _gameInput.Player.Movement.ReadValue<Vector2>();
            _characterMovement.MovementInput = movementInput;
        }
    }
}

