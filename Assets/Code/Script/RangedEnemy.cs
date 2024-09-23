using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyParent
{   

    [SerializeField] private float _cooldownFeedBack;
    [SerializeField] private GameObject _laserPrefabDamage;
    [SerializeField] private GameObject _laserPrefabHeal;
    [SerializeField] private Transform _initPosToFire;
    [SerializeField] private Laser _laser;
    public override void Start()
    {
        base.Start();

    }
    
    protected override void Fire()
    {
        if (_isFiring) return;
        StartCoroutine(Cooldown());
      
    }


    public IEnumerator Cooldown()
    {
        _isFiring = true;
        
        _laser.OnLaserFeeback = true;
        yield return new WaitForSeconds(_cooldownFeedBack);
        _laser.OnLaserFeeback = false;

        GameObject laser;
        if (GameManager.s_laserState == GameManager.ELaserState.Damaging)
        { 
             laser = Instantiate(_laserPrefabDamage, transform.position, transform.rotation);
        }
        else
        {
             laser = Instantiate(_laserPrefabHeal, transform.position, transform.rotation);
            
        }
        
        laser.GetComponent<LaserFire>().InitialPosition = _initPosToFire.position;
        laser.GetComponent<LaserFire>().InitialDirection = transform.forward;
        
        _isFiring = false;
        
    }
}
