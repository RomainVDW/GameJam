using System.Collections;
using UnityEngine;

public class LaserFire : LaserCompenent
{
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private GameObject _laserReflectPrefab;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _hitVFX;

    public void Start()
    {
        base.Start();
        _laser.SetPosition(0, InitialPosition);
        _laser.SetPosition(1, FinalPosition);
        StartCoroutine(disable(_laser.gameObject,_time));
        if (IsReflect)
        {
            SheildHeal sheildHeal = LastActorHit.GetComponent<SheildHeal>();
           
            if (sheildHeal.Active)
            {
                
                sheildHeal.TakeDamage(LaserDamage);
                
                Vector3 reflect = Vector3.Reflect(InitialDirection, LastActorHit.forward);
                GameObject laser = Instantiate(_laserReflectPrefab, transform.position, Quaternion.identity);
                laser.GetComponent<LaserFire>().IsReflect = false;
                laser.GetComponent<LaserFire>().InitialPosition = FinalPosition;
                laser.GetComponent<LaserFire>().InitialDirection = reflect;
            }
        }
        Instantiate(_hitVFX, FinalPosition, Quaternion.identity);
    }
    
    
    public override void EventActor(Collider actor)
    {
        if (actor.gameObject.TryGetComponent(out IHealth healthInterface))
        {
            healthInterface.TakeDamage(LaserDamage);
        }
    }
    
    
    private IEnumerator disable(GameObject ObjectToDestroy,float _time)
    {
        yield return new WaitForSeconds(_time);
       // Destroy(ObjectToDestroy);
    }
}
