using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyParent
{   
    [SerializeField] private Laser _laser;
    [SerializeField] private float _cooldownFeedBack;
    [SerializeField] private LineRenderer _laserLine;
    public override void Start()
    {
        base.Start();
        _laser = GetComponent<Laser>();
    }
    
    protected override void Fire()
    {
        StartCoroutine(Cooldown());
    }


    public IEnumerator Cooldown()
    {
        _isFiring = true;


        _laser.OnLaserFeeback = true;
        yield return new WaitForSeconds(_cooldownFeedBack);
        _isFiring = false;
        _laser.FireLaser(transform.position, transform.forward, "Player", _laserLine);
        _laser.OnLaserFeeback = false;
        
    }
}
