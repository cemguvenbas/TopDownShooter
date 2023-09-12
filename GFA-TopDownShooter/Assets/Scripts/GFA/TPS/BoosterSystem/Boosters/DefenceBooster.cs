using GFA.TPS.Mediators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.BoosterSystem.Boosters
{
    [CreateAssetMenu(menuName = "Boosters/Defence")]
    public class DefenceBooster : Booster
    {
        [SerializeField]
        private float _value;
        public override void OnAdded(BoosterContainer container)
        {
            if (container.TryGetComponent<PlayerMediator>(out var mediator))
            {
                mediator.Attributes.Defence += _value;
                //mediator.Attributes.Defence = Mathf.Clamp(mediator.Attributes.Defence, 0, 0.95f);
            }
        }
        
    }
}

