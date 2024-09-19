using System.Collections;
using UnityEngine;

public class BouclierHeal : MonoBehaviour, IHealth
{
    private float _health;
    [SerializeField] private float _maxHealth;

    private bool _canTakeDamage;
    [SerializeField] private bool _active = true;

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
        GameManager.s_Instance.BouclierIsActivated = false;
    }

    public void UpdateStatus()
    {
        if (_health <= 0)
        {
            OnDeath();
        }else if (_health > 0)
        {
            GameManager.s_Instance.BouclierIsActivated = true;
        }
    }

    public void ReflectLaser(Vector3 direction)
    {
        if (!_active) return;
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
}