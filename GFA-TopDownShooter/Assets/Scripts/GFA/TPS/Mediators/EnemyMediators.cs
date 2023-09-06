using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.Mediators
{
    public class EnemyMediators : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float _health;
        public void ApplyDamage(float damage, GameObject causer = null)
        {
            _health -= damage;
            if (_health <= 0)
            {
                gameObject.SetActive(false); 
            }
        }
    }
}

