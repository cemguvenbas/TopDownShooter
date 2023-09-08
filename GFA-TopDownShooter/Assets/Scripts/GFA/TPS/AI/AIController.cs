using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFA.TPS.AI;
using GFA.TPS.AI.States;

namespace GFA.TPS.AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private AIBehaviour _aiBehaviour;
        public AIBehaviour AIBehaviour
        {
            get => _aiBehaviour;
            set
            {
                if (_aiBehaviour)
                {
                    _aiBehaviour.End(this);
                }
                _aiBehaviour = value;

                if (_aiBehaviour)
                {
                    _aiState = _aiBehaviour.CreateState();
                    _aiBehaviour.Begin(this);
                }
            }
        }

        private AIState _aiState;

        private void Awake()
        {
            if (_aiBehaviour) _aiBehaviour.Begin(this);
        }
        private void Update()
        {
            if (AIBehaviour)
            {
                AIBehaviour.OnUpdate(this);
            }
        }
        public bool TryGetState<T>(out T state) where T : AIState
        {
            if (_aiState is T casted)
            {
                state = casted;
                return true;
            }
            else
            {
                state = null;
                return false;
            }
        }
    }
}

