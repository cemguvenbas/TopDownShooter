using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField]
        private float _xp;

        [SerializeField,Range(0,1)]
        private float _xpDropChance;

        [SerializeField]
        private XPCollectible _xpCollectiblePrefab;

        public void OnDied()
        {
            if (_xpCollectiblePrefab && Random.value > _xpDropChance)
            {
                var inst = Instantiate(_xpCollectiblePrefab, transform.position, Quaternion.identity);
                inst.XP = _xp;
            }
        }
    }
}

