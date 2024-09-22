using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyParent
{
    [SerializeField] private Animator _sickelAnimator;
    protected override void Fire()
    {
        _sickelAnimator.SetTrigger("Attack");
    }
    protected override void Attacking()
    {
        if (_agent.remainingDistance >= _agent.stoppingDistance)
        {
            StateChange(EState.Chasing);
        }
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= 1 / _fireRate)
        {
            _fireTimer = 0;
            Fire();
        }   
    }
}
