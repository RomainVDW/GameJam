using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public abstract void TakeDamage(float damage);
    public abstract void Heal(float heal);
    public abstract void OnDeath();
}
