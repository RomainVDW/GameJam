using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    private void EnableCollider()
    {
        _collider.enabled = true;
    }
    private void DisableCollider()
    {
        _collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out PlayerHealth player);
        player.TakeDamage(1.0f);
    }
}
