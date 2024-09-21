using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        //_transform.position = transform.position;
        //_transform.LookAt(Camera.main.transform);
        //_rotation = _transform.rotation;
        //transform.rotation = Quaternion.Euler(_rotation.eulerAngles.x * _rotationAxis.x, _rotation.eulerAngles.y * _rotationAxis.y, _rotation.eulerAngles.z * _rotationAxis.z);
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
