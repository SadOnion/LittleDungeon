using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    [SerializeField] float maxDelta;
    [SerializeField] float maxIntensity;
    [SerializeField] float minIntensity;
    Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    public void Flicker()
    {
        light.intensity = Mathf.Clamp(light.intensity+Random.Range(-maxDelta,maxDelta),minIntensity,maxIntensity);
    }


}
