using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyParent
{   

    [SerializeField] private float _cooldownFeedBack;
    [SerializeField] private GameObject _laserPrefab;
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
        GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<LaserFire>().InitialPosition = _initPosToFire.position;
        laser.GetComponent<LaserFire>().InitialDirection = transform.forward;
        
        _isFiring = false;
        
    }
}
