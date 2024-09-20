using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyParent
{   
    [SerializeField] private Laser _laser;
    [SerializeField] private float _cooldownFeedBack;
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
        _laser.MakeLaser();
        yield return new WaitForSeconds(_cooldownFeedBack);
        _laser.FireLaser();
        _isFiring = false;
    }
}
