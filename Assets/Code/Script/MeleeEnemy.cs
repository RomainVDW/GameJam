using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyParent
{
    [SerializeField] private Animator _sickelAnimator;
    [SerializeField] private Animator _slashAnimator;
    protected override void Fire()
    {
        _sickelAnimator.SetTrigger("Attack");
        _slashAnimator.SetTrigger("Slash");
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
