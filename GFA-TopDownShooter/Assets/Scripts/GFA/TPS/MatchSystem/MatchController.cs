using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.MatchSystem
{
    public class MatchController : MonoBehaviour
    {
        [SerializeField]
        private MatchInstance _matchInstance;

        private void Awake()
        {
            _matchInstance.Reset();
        }
        private void Update()
        {
            _matchInstance.AddTime(Time.deltaTime);
        }
    }
}

