using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantParticlePosition : MonoBehaviour {

    ParticleSystem ps;
    // Use this for initialization
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;
    }

    void LateUpdate()
    {
        ps = GetComponent<ParticleSystem>();
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


    }
}
