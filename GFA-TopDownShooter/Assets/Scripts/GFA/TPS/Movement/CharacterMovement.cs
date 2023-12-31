using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController _characterController;
        public Vector3 ExternalForces { get; set; }
        public Vector2 MovementInput { get; set; }

        public float Rotation { get; set; } // Rotation on y-axis

        [SerializeField]
        private float _movementSpeed = 4;
        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }

        public Vector3 Velocity => _characterController.velocity; // to detect our dodge axis.
            
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var movement = new Vector3(MovementInput.x, 0, MovementInput.y); // We have to turn Vector2 input from keyboard to Vector3.
            transform.eulerAngles = new Vector3(0, Rotation);
            _characterController.SimpleMove(movement * _movementSpeed + ExternalForces);
            ExternalForces = Vector3.Lerp(ExternalForces, Vector3.zero, 8 * Time.deltaTime);
            MovementInput = Vector2.zero;
        }
    }
}

