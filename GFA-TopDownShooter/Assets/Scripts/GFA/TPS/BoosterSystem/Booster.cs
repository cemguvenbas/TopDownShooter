using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.BoosterSystem
{
    public abstract class Booster : ScriptableObject
    {
        [SerializeField]
        private string _description;
        public string Description => _description;

        [SerializeField]
        private string _boosterName;
        public string BoosterName => _boosterName;

        public abstract void OnAdded(BoosterContainer container); // we are gonna run this function when boostercontainer added.
    }

}
