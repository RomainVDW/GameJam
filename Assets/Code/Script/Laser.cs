using System;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserMaxLength;
    [SerializeField] private float _laserDamage;
    [SerializeField] private int _layerMask;
    [SerializeField] private LineRenderer _lineRenderer;
    
    public UnityEvent<Vector3,Vector3,LineRenderer> OnFireLaser;
    public UnityEvent OnMakeLaser;
    public void HealMode(IHealth healthFunction)
    {
        healthFunction.Heal(1);
    }
    
    public void DamageMode(IHealth healthFunction)
    {
        healthFunction.TakeDamage(1);
    }

    public void MakeLaser()
    {
        OnMakeLaser.Invoke();
    }
    
    public void FireLaser()
    {
        RaycastHit hit;
        Vector3 pointA = transform.position;
        Vector3 pointB = transform.forward * _laserMaxLength;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, _laserMaxLength))
        {
            print( hit.collider.gameObject.name);
            pointB = hit.point;
            if (hit.collider.CompareTag("Player"))
            { ;
                if (GameManager.s_laserState == GameManager.ELaserState.Damaging)
                {
                    DamageMode(hit.collider.GetComponent<IHealth>());
                }
                else if (GameManager.s_laserState == GameManager.ELaserState.Healing)
                {
                    HealMode(hit.collider.GetComponent<IHealth>());
                }
            }
        }
        
        OnFireLaser.Invoke(pointA, pointB ,_lineRenderer);
    }
}

