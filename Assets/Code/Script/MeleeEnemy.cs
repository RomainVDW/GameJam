using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyParent
{
    protected override void Fire()
    {
        
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
