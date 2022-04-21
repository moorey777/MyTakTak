using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EmissionLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem longLauncher;
    public GameObject longObject;

    private float switchTime;
    // Start is called before the first frame update
    void Start()
    {
        particleLauncher = GetComponent<ParticleSystem>();
        longLauncher = longObject.GetComponent<ParticleSystem>();
        switchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - switchTime > 4.0f)
        {
            var emission = particleLauncher.emission;
            var emission2 = longLauncher.emission;
            if (emission.enabled == true)
            {
                emission2.enabled = false;
                emission.enabled = false;
                switchTime = Time.time;
            }
            else
            {
                emission2.enabled = true;
                emission.enabled = true;
                switchTime = Time.time;
            }
            
        }
    }


    
    
}
