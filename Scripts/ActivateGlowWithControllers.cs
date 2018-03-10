using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGlowWithControllers : MonoBehaviour {

    ParticleSystem ps;
    Light light;
    bool lightTrigger = false;
    
    // Use this for initialization
    void Start () {
        ps = GetComponentInChildren<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;
        light = GetComponentInChildren<Light>();
        light.intensity = 0;
	}

    void OnTriggerEnter(Collider other) {
        var em = ps.emission;
        if (other.CompareTag("Player")) {
            em.enabled = true;
            lightTrigger = true;
        }
    }

    void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);
        
        // iterate each particles
        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.position = new Vector3(0, 0, 0);
            // assign force for particles
            p.velocity = new Vector3(0, 0, 0);
            particles[i] = p;
        }

        ps.SetParticles(particles, particles.Length);

        if (lightTrigger) {
            while (light.intensity <= 1.0) {
                light.intensity += 0.05f;
            }
        } else
        {
            light.intensity = 0.0f;
        }
    }

}
