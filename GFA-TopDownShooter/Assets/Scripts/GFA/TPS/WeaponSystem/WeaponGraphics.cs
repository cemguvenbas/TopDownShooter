using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.WeaponSystem
{
    public class WeaponGraphics : MonoBehaviour
    {
        [SerializeField]
        private Transform _shootTransform;

        public Transform ShootTransform => _shootTransform;

        public event Action Shot; 
        public void OnShot()
        {
            Shot?.Invoke();
        }
    }
}

