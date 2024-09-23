using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    void Update()
    {
        _camera.transform.position = new Vector3(GameManager.s_Instance.Player.position.x, 10, GameManager.s_Instance.Player.position.z);
    }
}
