using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.VolumeComponent;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserMaxLength;
    [SerializeField] private float _laserDamage;
    [SerializeField] private String _tagMask;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private LineRenderer _lineRendererFeedBack;
    [SerializeField] private LineRenderer _lineRendererFeedBackReflect;
    [SerializeField] public bool _onLaserFeeback { get; set; }

    public UnityEvent<Vector3,Vector3> HitSheild;
    public UnityEvent<Vector3, Vector3> HitSheildFeedBack;

    public UnityEvent<Vector3,Vector3,LineRenderer> OnFireLaser;
    public UnityEvent<Vector3, Vector3, LineRenderer> OnMakeLaser;

    public void Update()
    {
        
        MakeRay(transform.position, transform.forward, _lineRendererFeedBack);
    }


    public void HealMode(IHealth healthFunction)
    {
        healthFunction.Heal(1);
    }
    
    public void DamageMode(IHealth healthFunction)
    {
        healthFunction.TakeDamage(1);
    }

    public Vector3 ReflectLaser(Vector3 direction, Vector3 normal)
    {
 
        Vector3 reflectedDirection = Vector3.Reflect(direction, normal);
        return reflectedDirection;
    }
 

    public void MakeRay(Vector3 a,Vector3 b,LineRenderer lineRenderer)
    {
        RaycastHit hit;
        Vector3 pointA = a;
        Vector3 pointB = b;

        if (Physics.Raycast(pointA, pointB, out hit, _laserMaxLength))
        {
            GameObject hitObject = hit.collider.gameObject;
            pointB = hit.point;

            if (hit.collider.CompareTag(_tagMask))
            {
                if (GameManager.s_laserState == GameManager.ELaserState.Damaging)
                {
                    DamageMode(hit.collider.GetComponent<IHealth>());
                    if (hitObject.TryGetComponent(out BouclierHeal bouclierHeal))
                    {
                        Vector3 reflectDir = ReflectLaser( b, hitObject.transform.forward);

                        _lineRendererFeedBackReflect.SetPosition(0, pointB);
                        _lineRendererFeedBackReflect.SetPosition(1, pointB + reflectDir * _laserMaxLength);
                    }
                }
            }
        }

        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);


    }

    public void FireLaser()
    {
        RaycastHit hit;
        Vector3 pointA = transform.position;
        Vector3 pointB = transform.forward * _laserMaxLength;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, _laserMaxLength))
        {
            GameObject hitObject = hit.collider.gameObject;
            pointB = hit.point;
            
            if (hit.collider.CompareTag(_tagMask)) {
                if (GameManager.s_laserState == GameManager.ELaserState.Damaging)
                {
                    DamageMode(hit.collider.GetComponent<IHealth>());
                    if (hitObject.TryGetComponent( out BouclierHeal bouclierHeal))
                    {
                        HitSheild.Invoke(hit.point, transform.forward);
                    }
                }
                else if (GameManager.s_laserState == GameManager.ELaserState.Healing)
                {
                    HealMode(hit.collider.GetComponent<IHealth>());

                    DamageMode(hit.collider.GetComponent<IHealth>());
                    if (hitObject.TryGetComponent(out BouclierHeal bouclierHeal))
                    {
                        HitSheild.Invoke(hit.point, transform.forward);
                    }
                }
                OnFireLaser.Invoke(pointA, pointB, _lineRenderer);
            }
        }
    }
}

