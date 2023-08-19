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

        private Camera _camera;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _gameInput = new GameInput();
            _camera = Camera.main;
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
            var ray = _camera.ScreenPointToRay(_gameInput.Player.PointerPosition.ReadValue<Vector2>());
            var gamepadLookDir = _gameInput.Player.Look.ReadValue<Vector2>();
            if (gamepadLookDir.magnitude > 0.1f)
            {
                var angle = -Mathf.Atan2(gamepadLookDir.y, gamepadLookDir.x) * Mathf.Rad2Deg + 90; 
                _characterMovement.Rotation = angle;
            }
            else
            {
                if (_plane.Raycast(ray, out float enter)) // We are sending Raycast to Plane
                {
                    var worldPosition = ray.GetPoint(enter); // Plane and ray's intersection point 
                    var dir = (worldPosition - transform.position).normalized;
                    var angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + 90; //Quaternion.LookRotation(dir).eulerAngles.y; works as well instead of Atan2.
                    _characterMovement.Rotation = angle;
                }
            }
            
        }
    }
}

