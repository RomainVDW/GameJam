using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _player;
    public Vector3 posOffset;
    private Vector3 velocity = Vector3.zero;
    void Awake()
    {
        GameManager.s_Instance.Player = _player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.SmoothDamp(transform.position, _player.transform.position - posOffset, ref velocity, Time.deltaTime);
    }
}
