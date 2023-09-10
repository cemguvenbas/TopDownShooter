using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS
{
    public class SmoothFollower : MonoBehaviour
    {
        public Transform Target { get; set; }
        private const float FOLLOW_SPEED = 8;

        private const float ACCEPTANCE_RADIUS = 1;

        public event Action ReachedDestination;

        private bool _reachedDestination;

        private void Update()
        {
            if (Target)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.position, FOLLOW_SPEED * Time.deltaTime); //SmoothDamp

                if (Vector3.Distance(Target.position,transform.position) <= ACCEPTANCE_RADIUS)
                {
                    if (!_reachedDestination)
                    {
                        ReachedDestination?.Invoke();
                        _reachedDestination = true;
                    }
                }
                else
                {
                    _reachedDestination = false;   
                }
            }
        }
    }
}

