using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SheildHeal : MonoBehaviour, IHealth
{
    [SerializeField] private Image _shieldImage;
    [SerializeField] private Sprite _shield0;
    [SerializeField] private Sprite _shield1;
    [SerializeField] private Sprite _shield2;
    [SerializeField] private Sprite _shield3;
    [SerializeField] private Sprite _shield4;
    [SerializeField] private Sprite _shield5;
    
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Material _shieldMaterial;

    private bool _canTakeDamage;
    public bool Active { get; set; } 

    [SerializeField] private float _invincibilityDuration = 2;


    private void Start()
    {
        
        _canTakeDamage = true;
        Active = true;
        _health = _maxHealth;
        
        
    }

    public void TakeDamage(float damage)
    {
        if (!Active)
        {
            _player.TakeDamage(damage);
            return;
        }
        if (!_canTakeDamage ) return;
        _health -= damage;
     
        StartCoroutine(TemporaryInvincible());
        UpdateStatus();
    }

    public void Heal(float heal)
    {
        _health = _health + heal > _maxHealth ? _maxHealth : _health + heal;
        UpdateStatus();
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
        }
        else Active = true;
        if (_health >= 0)
        {
            switch (_health)
            {
                case 5f:
                    _shieldImage.sprite = _shield5;
                    break;
                case 4f:
                    _shieldImage.sprite = _shield4;
                    break;
                case 3f:
                    _shieldImage.sprite = _shield3;
                    break;
                case 2f:
                    _shieldImage.sprite = _shield2;
                    break;
                case 1f:
                    _shieldImage.sprite = _shield1;
                    break;
                case 0f:
                    _shieldImage.sprite = _shield0;
                    break;
            }
        }
    }


    private void Update()
    {
        _shieldMaterial.SetFloat("_NormalizedHealth", _health / _maxHealth);
    }

    public IEnumerator TemporaryInvincible()
    {
        if (!_canTakeDamage) yield break;
        _canTakeDamage = false;
        yield return new WaitForSeconds(_invincibilityDuration);
        _canTakeDamage = true;
        
    }
}