using System.Collections;
using UnityEngine;

public class LaserReflect : LaserCompenent
{
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private float _time;
    public void Start()
    {
        base.Start();
        _laser.SetPosition(0, InitialPosition);
        _laser.SetPosition(1, FinalPosition);
        StartCoroutine(disable(_laser.gameObject,_time));
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
