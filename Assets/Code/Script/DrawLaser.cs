using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLaser : MonoBehaviour
{
    public void Fire(LineRenderer lineRenderer)
    {
        lineRenderer.enabled = true;
        StartCoroutine(disable(lineRenderer));
    }
    
    
    private IEnumerator disable(LineRenderer lineRenderer)
    {
        
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
}
