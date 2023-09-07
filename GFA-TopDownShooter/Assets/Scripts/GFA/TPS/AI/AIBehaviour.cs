using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace GFA.TPS.AI
{
    public abstract class AIBehaviour : ScriptableObject
    {
        public abstract void Begin(AIController controller);
        public void OnUpdate(AIController controller)
        {
            Profiler.BeginSample($"AI Behaviour({name}).Execute");
            Execute(controller);
            Profiler.EndSample();
        }
        public abstract void End(AIController controller);
        protected abstract void Execute(AIController controller);
    }
}

