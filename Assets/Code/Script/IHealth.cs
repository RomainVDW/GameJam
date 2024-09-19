using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void TakeDamage(float Damage);
    public void OnDeath();
    public void OnHeal(float Heal);
}
