using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out PlayerHealth player);
        player.TakeDamage(1.0f);
    }
}
