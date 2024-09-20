using System;
using UnityEngine;
using UnityEngine.Events;


public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserMaxLength;
    [SerializeField] private float _laserDamage;
    [SerializeField] private String _tagMask;

    [Header("Fire")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LineRenderer _lineRendererReflect;
    [Header("FeedBack")]
    [SerializeField] private LineRenderer _lineRendererFeedBack;
    [SerializeField] private LineRenderer _lineRendererFeedBackReflect;
    [field:SerializeField] public bool OnLaserFeeback { get; set; }


    public void Update()
    {
        if (OnLaserFeeback)
            MakeRay(transform.position, transform.forward);
    }


    public void HealMode(IHealth healthFunction)
    {
        healthFunction.Heal(1);
    }
    
    public void DamageMode(IHealth healthFunction)
    {
        healthFunction.TakeDamage(1);
    }

    public void MakeRay(Vector3 a,Vector3 dir)
    {
        RaycastHit hit;
        Vector3 pointA = a;
        Vector3 pointB = a + dir * _laserMaxLength;
      
        if (Physics.Raycast(a, dir, out hit, _laserMaxLength))
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

                      
                        Vector3 reflectDir = Vector3.Reflect(dir, hitObject.transform.forward);
                        _lineRendererFeedBackReflect.SetPosition(0, pointB);
                        _lineRendererFeedBackReflect.SetPosition(1, pointB + reflectDir * _laserMaxLength);
                        _lineRendererFeedBackReflect.enabled = true;

                    }
                    else 
                    {

                        _lineRendererFeedBackReflect.enabled = false;

                    }
                }
            }
        }

        _lineRendererFeedBack.SetPosition(0, pointA);
        _lineRendererFeedBack.SetPosition(1, pointB);


    }

    public void FireLaser(Vector3 intPos, Vector3 dir,String tag, LineRenderer lineRenderer)
    {
        RaycastHit hit;
        Vector3 hitPos = intPos + dir * _laserMaxLength;


        if (Physics.Raycast(intPos, dir, out hit, _laserMaxLength))
        {
            GameObject hitObject = hit.collider.gameObject;
            hitPos = hit.point;

            lineRenderer.SetPosition(0, intPos);
            lineRenderer.SetPosition(1, hitPos);


            if (hit.collider.CompareTag(tag)){
                if (GameManager.s_laserState == GameManager.ELaserState.Damaging)
                {
                  
                    DamageMode(hit.collider.GetComponent<IHealth>());
                 
                    if (hitObject.TryGetComponent( out BouclierHeal bouclierHeal))
                    {
                    
                        Vector3 reflectDir = Vector3.Reflect(dir, hitObject.transform.forward);
                        FireLaser(hitPos, reflectDir, "Ennemy", _lineRendererReflect);
                      
                    }
                }
                else if (GameManager.s_laserState == GameManager.ELaserState.Healing)
                {
                    DamageMode(hit.collider.GetComponent<IHealth>());
                    
                }
            }
        }

        lineRenderer.SetPosition(0, intPos);
        lineRenderer.SetPosition(1, hitPos);
    }
}

