using System;
using UnityEngine;
using UnityEngine.Events;


public class Laser : MonoBehaviour
{
    [Header("FeedBack")]
    
    [SerializeField] private LineRenderer _lineRendererFeedBack;
    [SerializeField] private LineRenderer _lineRendererFeedBackReflect;
    
    [field:SerializeField] public bool OnLaserFeeback { get; set; }
    [SerializeField] private float _laserMaxLength;
    
    private int _shieldLayer = 8;
    private int _environmentLayer = 7;
    private Vector3 _oldPosition;
    
  
    public void Update()
    {
        if (OnLaserFeeback)
        {
            _lineRendererFeedBack.gameObject.SetActive(true);
            MakeRay(transform.position, transform.forward);
        }
        else 
        {
            _lineRendererFeedBack.gameObject.SetActive(false);
            _lineRendererFeedBackReflect.gameObject.SetActive(false);
            _oldPosition = transform.position + transform.forward * _laserMaxLength;
        }
    }
    
    
    public void MakeRay(Vector3 initPosition,Vector3 dir)
    {
        RaycastHit hit;
        int layerMask = 1 << _shieldLayer | 1 << _environmentLayer;
        Vector3 finalPosition = initPosition + dir * _laserMaxLength;
        
        if (Physics.Raycast(initPosition, dir, out hit, _laserMaxLength, layerMask))
        {
           
            finalPosition = hit.point;
            if (GameManager.s_laserState == GameManager.ELaserState.Damaging && hit.collider.gameObject.layer == _shieldLayer)
            {
                Vector3 reflectDir = Vector3.Reflect(dir, hit.transform.forward);
                Vector3 finalReflectPosition = Vector3.Lerp(_oldPosition, finalPosition, 0.3f);
                
                RaycastHit hit1;
                if (Physics.Raycast(finalPosition, reflectDir, out hit1, _laserMaxLength, layerMask))
                {
                    
                    finalReflectPosition = hit1.point;
                }else
                {
                    finalReflectPosition = finalPosition + reflectDir * _laserMaxLength;
                }
                
                setPositionLineRenderer( _lineRendererFeedBackReflect, finalPosition, finalReflectPosition);
                _lineRendererFeedBackReflect.gameObject.SetActive(true);
                _oldPosition = finalReflectPosition;
            }
            else 
            {
                _lineRendererFeedBackReflect.gameObject.SetActive(false);
            }
        }else
        {
            _lineRendererFeedBackReflect.gameObject.SetActive(false);
        }
        
        setPositionLineRenderer(_lineRendererFeedBack , initPosition, finalPosition);
    }
    
    public void setPositionLineRenderer(LineRenderer lineRenderer , Vector3 initialPos, Vector3 finalPos)
    {
        lineRenderer.SetPosition(0, initialPos);
        lineRenderer.SetPosition(1, finalPos);
    }
}

