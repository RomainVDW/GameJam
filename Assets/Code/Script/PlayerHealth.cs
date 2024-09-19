using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private float _health;
    [SerializeField] private float _maxHealth;
    private bool _canTakeDamage;
    [SerializeField] private float _invincibilityDuration = 2;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Heal(float heal)
    {
        if (_health + heal > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += heal;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!_canTakeDamage)
            return;
        if (_health - damage < _maxHealth)
        {
            _health = 0;
            OnDeath();
        }
        else
        {
            _health -= damage;
        }
    }
    public IEnumerator TemporaryInvincible()
    {
        if (!_canTakeDamage) yield break;
        else
        {
            _canTakeDamage = false;
            yield return new WaitForSeconds(_invincibilityDuration);
            _canTakeDamage = true;
        }
    }

    public IEnumerator TemporaryInvincible(float duration)
    {
        if (!_canTakeDamage) yield break;
        else
        {
            _canTakeDamage = false;
            yield return new WaitForSeconds(duration);
            _canTakeDamage = true;
        }
    }

    public void OnDeath()
    {
        GameManager.s_Instance.GameOver();
    }
}