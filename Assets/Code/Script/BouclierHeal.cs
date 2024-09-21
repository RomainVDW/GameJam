using System.Collections;
using UnityEngine;

public class BouclierHeal : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;



    private bool _canTakeDamage;
    public bool Active { get; set; } 

    [SerializeField] private float _invincibilityDuration = 2;


    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
   
        if (!_canTakeDamage) return;
  
        _health -= damage;
        StartCoroutine(TemporaryInvincible());
        UpdateStatus();
    }

    public void Heal(float heal)
    {
        _health = _health + heal > _maxHealth ? _maxHealth : _health + heal;
    }

    public void OnDeath()
    {
        Active = false;
    }

    public void UpdateStatus()
    {
        if (_health <= 0)
        {
            OnDeath();
        }else if (_health > 0)
        {
            Active = true;
        }
    }

 



    public IEnumerator TemporaryInvincible()
    {
        if (!_canTakeDamage) yield break;
    
        
        _canTakeDamage = false;
        yield return new WaitForSeconds(_invincibilityDuration);
        _canTakeDamage = true;
        
    }
}