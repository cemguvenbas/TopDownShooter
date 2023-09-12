using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFA.TPS.Movement;
using GFA.TPS.Input;
using UnityEngine.InputSystem;
using System;

namespace GFA.TPS.Mediators
{
    public class PlayerMediator : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private Attributes _attributes;
        public Attributes Attributes => _attributes;

        private CharacterMovement _characterMovement;
        private Shooter _shooter;
        private XPCollectibleAttractor _xPCollectibleAttractor;

        private GameInput _gameInput;

        [SerializeField]
        private float _dodgePower;

        private int _level;
        public int Level => _level;

        private float _xp;

        public float MaxHP => (_level + 1) * 5;

        private Plane _plane = new Plane(Vector3.up, Vector3.zero); // our ground should be flat to use Plane

        private Camera _camera;

        [SerializeField]
        private float _health;

        public event Action<int> LevelledUp;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _shooter = GetComponent<Shooter>();
            _gameInput = new GameInput();
            _xPCollectibleAttractor = GetComponent<XPCollectibleAttractor>();
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Dodge.performed += OnDodgeRequested;
            _xPCollectibleAttractor.XPCollected += OnAttractorXPCollected;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Dodge.performed -= OnDodgeRequested;
            _xPCollectibleAttractor.XPCollected -= OnAttractorXPCollected;
        }

        private void OnAttractorXPCollected(float xp)
        {
            AddXP(xp);
        }

        private void AddXP(float value)
        {
            _xp += value;
            if (_xp >= MaxHP)
            {
                _level++;
                _xp = 0;
                LevelledUp?.Invoke(_level);                
            }

        }

        private void OnDodgeRequested(InputAction.CallbackContext obj)
        {
            _characterMovement.ExternalForces += _characterMovement.Velocity.normalized * _dodgePower;
        }

        private void Update()
        {
            HandleAttributes();
            HandleMovement();
            if (_gameInput.Player.Shoot.IsPressed())
            {
                _shooter.Shoot();
            }            
        }

        private void HandleAttributes()
        {
            _characterMovement.MovementSpeed = Attributes.MovementSpeed;
            _shooter.AttackSpeedMultiplier = Attributes.AttackSpeed;
            _shooter.BaseDamage = Attributes.Damage;
        }

        private void HandleMovement()
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

        public void ApplyDamage(float damage, GameObject causer = null)
        {

        }
    }
}

