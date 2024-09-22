using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private enum DestroyType
    {
        ThroughAnimation,
        Delayed
    }
    [SerializeField] private DestroyType _destroyType;
    [SerializeField] private float _destroyDelay = 0.0f;
    void Start()
    {
        switch (_destroyType)
        {
            case DestroyType.ThroughAnimation:
                break;
            case DestroyType.Delayed:
                SeflDestroy(_destroyDelay);
                break;
        }
    }

    private void SeflDestroy(float delay)
    {
        Destroy(gameObject, delay);
    }
}
