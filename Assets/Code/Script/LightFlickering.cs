using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    private Light _light;
    private float _intensity;
    [SerializeField] private float _flickeringSpeed = 0.1f;
    [SerializeField] private float _flickeringIntensity = 0.1f;

    private void Start()
    {
        _light = GetComponent<Light>();
        _intensity = _light.intensity;
    }
    void Update()
    {
        _light.intensity = Mathf.Lerp(_intensity, _intensity * Mathf.PerlinNoise1D(Time.time * _flickeringSpeed), _flickeringIntensity);  
    }
}
