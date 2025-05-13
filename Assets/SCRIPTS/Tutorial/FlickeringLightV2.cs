using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLightV2 : MonoBehaviour
{
    public Light2D lamp;
    public float minIntensity = 1.5f;
    public float maxIntensity = 2.0f;
    public float flickerSoeed = 1.0f;
    private float nextChange;
    private float targetIntensity;
    private void Start()
    {
        nextChange = Time.time + Random.Range(0.0f, flickerSoeed);
        targetIntensity = maxIntensity;
    }
    private void Update()
    {
        if (Time.time > nextChange)
        {
            lamp.intensity = Random.Range(minIntensity, targetIntensity);
            nextChange = Time.time + flickerSoeed;
            targetIntensity = (targetIntensity == maxIntensity) ? minIntensity : maxIntensity;
        }
    }
}
