using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserMaxLength;
    [SerializeField] private float _laserDamage;
    [SerializeField] private float _layerMask;
    
    private Vector3 pointA;
    private Vector3 pointB;
    
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
        RaycastHit hit;
        pointA = transform.position;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, _laserMaxLength))
        {
            
            if (hit.collider.CompareTag("Player"))
            {
                pointB = hit.point;
                if (GameManager.s_laserState == GameManager.ELaserState.Damaging)
                {
                    DamageMode(hit.collider.GetComponent<IHealth>());
                }
                else if (GameManager.s_laserState == GameManager.ELaserState.Healing)
                {
                    HealMode(hit.collider.GetComponent<IHealth>());
                }
            }
        }else
        {
            pointB = transform.forward * _laserMaxLength;
        }
        
        print(pointB);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointA, pointB);
    }
}

