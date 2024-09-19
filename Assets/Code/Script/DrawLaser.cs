using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLaser : MonoBehaviour
{
    public void OnFireLaser(Vector3 pointA, Vector3 pointB, LineRenderer lineRenderer)
    {
        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);
        
        StartCoroutine(disable(lineRenderer));
    }
    
    
    private IEnumerator disable(LineRenderer lineRenderer)
    {
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}
