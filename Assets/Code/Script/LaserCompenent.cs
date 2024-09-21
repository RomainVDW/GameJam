using System;
using UnityEngine;

public class LaserCompenent : MonoBehaviour
{

    private int _layerMaskEntities;
    private int _layerMaskActor;
    
    private int _shieldLayer = 7;
    private int _environmentLayer = 1;
    private int _enemyLayer = 9;
    private int _playerLayer = 1;
    
    
    [field: SerializeField] public float LaserMaxLength { get; set;}
    [field: SerializeField] public float LaserMaxWidth{ get; set;}
    [field: SerializeField] public int LaserDamage{ get; set;}
    [field: SerializeField] public bool IsReflect{ get; set; }
    
    public Vector3 InitialPosition { get; set; }
    public Vector3 FinalPosition { get;  set; }
    public Vector3 InitialDirection { get; set; }
    public Collider[] HitColliders{ get; set;}
    public Transform LastActorHit { get; set; }
    
    
    public void Awake()
    {
        _layerMaskEntities = 1 << _shieldLayer | 1 << _environmentLayer;
        _layerMaskActor = 1 << _enemyLayer | 1 << _playerLayer | 1 << _shieldLayer;
        
        HitColliders = new Collider[20];
    }

    public virtual void Start()
    {
        CallEventEveryActor();
    }

    public Vector3 GetPostionToMakeRay()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(InitialPosition, InitialDirection, out hit, LaserMaxLength , _layerMaskEntities))
        {
            if (hit.collider.gameObject.layer == _shieldLayer)
            {
               IsReflect = true;
            }
            
            LastActorHit = hit.transform; //Si le dernier actor toucher reflect cela veux dire que c est le bouclier
            
            return hit.point;
        }
        else
        {
            return InitialPosition + InitialDirection * LaserMaxLength;
        }
    }
    
    public int GetActorMaxIndexRay()
    {
        FinalPosition = GetPostionToMakeRay();
        int hitCount = Physics.OverlapCapsuleNonAlloc(InitialPosition, FinalPosition, LaserMaxWidth,HitColliders, _layerMaskActor);
        
        return hitCount; //pourra etre utiliser pour lire les actor dans le HitColliders
    }
    
    public void CallEventEveryActor()
    {
        for (int i = 0; i < GetActorMaxIndexRay(); i++)
        {
            EventActor(HitColliders[i]);
        }
    }

    public virtual void EventActor(Collider actor) { }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(InitialPosition,LaserMaxWidth);
        Gizmos.DrawSphere(FinalPosition,LaserMaxWidth);
        
        
        
    }
}
