using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// We could have try static class or singleton as well but in static class our referances will be deleted after compiling. It might be effect our workflow.
namespace GFA.TPS.MatchSystem
{
    [CreateAssetMenu(menuName = "MatchInstance")]
    public class MatchInstance : ScriptableObject
    {
        public float Time { get; private set; }

        public void AddTime(float delta)
        {
            Time += delta;
        }
        public void Reset()
        {
            Time = 0;
        }
    }
}

