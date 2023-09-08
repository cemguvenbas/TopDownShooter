using GFA.TPS.Movement;
using UnityEngine;

namespace GFA.TPS.AI.States
{
    public class BasicAIState : AIState
    {
        public CharacterMovement CharacterMovement { get; set; }
        public EnemyAttacker Attacker { get; set; }
        public IDamageable PlayerDamageable { get; set; }
    }

    //public class BossAIState : AIState
    //{
    //    public CharacterMovement CharacterMovement { get; set; }
    //    public Animator Animator { get; set; }
    //}
}

