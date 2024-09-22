using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : LaserCompenent
{
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private GameObject _laserReflectPrefab;
    [SerializeField] private float _time;
    public void Start()
    {
        base.Start();
        _laser.SetPosition(0, InitialPosition);
        _laser.SetPosition(1, FinalPosition);
        StartCoroutine(disable(_laser.gameObject,_time));
        if (IsReflect)
        {
            Vector3 reflect = Vector3.Reflect(InitialDirection, LastActorHit.forward);
            GameObject laser = Instantiate(_laserReflectPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<LaserReflect>().InitialPosition = LastActorHit.position;
            laser.GetComponent<LaserReflect>().InitialDirection = reflect;
        }
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
