using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.Movement
{
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        private float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        [SerializeField]
        private bool _shouldDisableOnCollision;
        public bool ShouldDisableOnCollision
        {
            get => _shouldDisableOnCollision;
            set => _shouldDisableOnCollision = value;
        }

        private void Update()
        {
            var direction = transform.forward;
            var distance = _speed * Time.deltaTime;
            var targetPosition = transform.position + direction * distance;

            if (Physics.Raycast(transform.position,direction,out var hit, distance))
            {
                if (ShouldDisableOnCollision)
                {
                    enabled = false;
                }
                targetPosition = hit.point;
            }

            Debug.DrawLine(transform.position, targetPosition, Color.red);
            transform.position = targetPosition; // if an obstacle occurs in front of our projectile, it will stop
            Debug.DrawRay(transform.position, direction * distance, Color.blue);
        }
    }
}

