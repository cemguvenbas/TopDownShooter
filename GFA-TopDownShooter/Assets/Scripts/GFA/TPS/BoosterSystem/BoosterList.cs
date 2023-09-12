using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.BoosterSystem
{
    [CreateAssetMenu(menuName ="BoosterList")]
    public class BoosterList : ScriptableObject
    {
        [SerializeField]
        private List<Booster> _boosters = new List<Booster>();
        //public List<Booster> Booster => _boosters;
        public int Length => _boosters.Count;
        public Booster Get(int i) => _boosters[i];
    }

}
