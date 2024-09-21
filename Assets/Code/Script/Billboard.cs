using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Quaternion _rotation;
    private Transform _transform;
    [SerializeField] private Vector3 _rotationAxis = new(1, 0, 1);

    void Update()
    {
        _transform.position = transform.position;
        _transform.LookAt(Camera.main.transform);
        _rotation = _transform.rotation;
        transform.rotation = Quaternion.Euler(_rotation.eulerAngles.x * _rotationAxis.x, _rotation.eulerAngles.y * _rotationAxis.y, _rotation.eulerAngles.z * _rotationAxis.z);
    }
}
