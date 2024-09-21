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
    private int _environmentLayer = 1;
    private Vector3 _oldPosition;
    
  
    public void Update()
    {
        if (OnLaserFeeback)
        {
            _lineRendererFeedBack.enabled = true;
            MakeRay(transform.position, transform.forward);
        }
        else 
        {
            _lineRendererFeedBack.enabled = false;
            _lineRendererFeedBackReflect.enabled = false;
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
                setPositionLineRenderer( _lineRendererFeedBackReflect, finalPosition, finalReflectPosition + reflectDir * _laserMaxLength);
                _lineRendererFeedBackReflect.enabled = true;
                _oldPosition = finalReflectPosition;
            }
            else 
            {
                _lineRendererFeedBackReflect.enabled = false;
            }
        }else
        {
            _lineRendererFeedBackReflect.enabled = false;
        }
        
        setPositionLineRenderer(_lineRendererFeedBack , initPosition, finalPosition);
    }
    
    public void setPositionLineRenderer(LineRenderer lineRenderer , Vector3 initialPos, Vector3 finalPos)
    {
        lineRenderer.SetPosition(0, initialPos);
        lineRenderer.SetPosition(1, finalPos);
    }
}

