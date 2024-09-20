
using UnityEngine;

public class RangedEnemy : EnemyParent
{   
    [SerializeField] private Laser _laser;
    
    public override void Start()
    {
        base.Start();
        _laser = GetComponent<Laser>();
    }
    
    protected override void Fire()
    {
        
        print("Fire!");
        _laser.FireLaser();

    }
}
