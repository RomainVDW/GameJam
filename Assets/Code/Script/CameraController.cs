using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player;
    public Vector3 posOffset;
    private Vector3 velocity = Vector3.zero;
    private float _smoothPositionTime = 0.3f;
    void Start()
    {
        _player = GameManager.s_Instance.Player;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = Vector3.SmoothDamp(transform.position, _player.transform.position - posOffset, ref velocity, _smoothPositionTime);
        transform.position = position;
    }
}
