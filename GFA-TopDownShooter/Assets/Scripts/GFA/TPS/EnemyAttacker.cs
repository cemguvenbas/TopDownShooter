using GFA.TPS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _range;
    public float Range => _range;

    [SerializeField]
    private float _attackRate;

    private float _lastAttack;
    public bool CanAttack => _lastAttack + _attackRate < Time.time;
    public bool IsCurrentlyAttacking { get; private set; }
    public void Attack(IDamageable target)
    {
        if (!CanAttack) return;
        _lastAttack = Time.time;
        StartCoroutine(ApplyAttackDelayed(target));
    }

    private IEnumerator ApplyAttackDelayed(IDamageable target)
    {
        IsCurrentlyAttacking = true;
        yield return new WaitForSeconds(0.5f);
        IsCurrentlyAttacking = false;

        if (target is MonoBehaviour mb)
        {
            if (Vector3.Distance(mb.transform.position, transform.position) < _range )
            {
                target.ApplyDamage(_damage);
            }
        }
        else
        {
            target.ApplyDamage(_damage);
        }
    }
}
