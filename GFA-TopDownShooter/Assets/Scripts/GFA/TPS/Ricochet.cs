using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFA.TPS.Movement;
using System;

namespace GFA.TPS
{
    public class Ricochet : MonoBehaviour
    {
        [SerializeField]
        private float _radius;

        [SerializeField]
        private float _ricochetCount = 5;

        [SerializeField]
        private bool _removeOnComplete;

        private ProjectileMovement _projectileMovement;
        private void Awake()
        {
            _projectileMovement = GetComponent<ProjectileMovement>();
        }
        private void OnEnable()
        {
            _projectileMovement.Impacted += OnImpacted;
        }
        private void OnDisable()
        {
            _projectileMovement.Impacted -= OnImpacted;
        }

        private void OnImpacted(RaycastHit raycastHit)
        {
            if (_ricochetCount <= 0)
            {
                if (_removeOnComplete)
                {
                    Destroy(this);
                }
                return;
            }
            var hits = Physics.OverlapSphere(raycastHit.point, _radius);
            foreach (var hit in hits)
            {
                if (hit.transform == raycastHit.transform) continue;
                if (hit.transform.TryGetComponent<IDamageable>(out var _))
                {
                    var dir = (hit.transform.position - transform.position).normalized;
                    transform.forward = dir;
                    _ricochetCount--;
                    return;
                }
            }
        }
    }
}

