using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOperationForIwakura : MonoBehaviour {
	ParticleSystem ps;
	[Range(0.0f, 10.0f)]
	public float pushStrength = 4.0f;
	[Range(0.0f, 10.0f)]
	public float rotateStrength = 6.0f;
	[Range(0.0f, 1.0f)]
	public float decay = 0.5f;
	public Vector2 decayRange = new Vector2(5, 15);
	[Range(0.0f, 30.0f)]
	public float axisRotationSpeed = 15.0f;
    [Range(0.0f, 1.0f)]
    public float randomizeLevel = 0.3f;

	void Start () {
		ps = GameObject.Find("ParticleAroundStone").GetComponent<ParticleSystem> ();
		var emitter = ps.emission;
		emitter.enabled = false;
		var space = ps.main.simulationSpace;
		space = ParticleSystemSimulationSpace.World;
        GetComponent<AudioSource>().playOnAwake = false;
        
    }
	

	void LateUpdate () {
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);

		// set a force field
		Vector3 stonePos = GameObject.Find("Iwakura").transform.position;
		//stonePos.y = 0;

		float rotSpeed = axisRotationSpeed * Time.deltaTime;
		Vector3 rotAxis1 = new Vector3 (Mathf.Sin (rotSpeed), Mathf.Sin (rotSpeed + 0.3f*Mathf.PI), Mathf.Sin (rotSpeed + 0.6f*Mathf.PI));
		Vector3 rotAxis2 = new Vector3 (Mathf.Sin (rotSpeed-0.5f*Mathf.PI), Mathf.Sin (rotSpeed - 0.8f*Mathf.PI), Mathf.Sin (rotSpeed - 1.1f*Mathf.PI));

		// iterate each particles
		for (int i = 0; i < particles.Length; i++) {
			ParticleSystem.Particle p = particles [i];

			// calculate vector for the force
			Vector3 pPos = p.position;
			Vector3 pushForce = Vector3.Normalize(pPos - stonePos);
            Vector3 rotForce;

            // make groups of particles and assign different rotation angle
            if(i%2 == 0) {
                rotForce = Vector3.Cross(rotAxis1, pushForce).normalized;

            } else {
                rotForce = Vector3.Cross(rotAxis2, pushForce).normalized * 1.5f;
            }
            

			// add some decay depending on the distance from the force source
			float decayedStrength = Mathf.Clamp(Mathf.Lerp(Vector3.Distance(stonePos, pPos), decayRange.x, decayRange.y), 0.0f, 1.0f) * (1.0f - decay);

			// assign force for particles
			p.velocity = rotForce * rotateStrength * Random.Range(1.0f-randomizeLevel, 1.0f) + pushForce * decayedStrength * pushStrength * Random.Range(1.0f - randomizeLevel, 1.0f);
			particles [i] = p;
		}
			
		ps.SetParticles (particles, particles.Length);
        
        
	}

	// trigger for emission

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")){
			var emitter = ps.emission;
			emitter.enabled = true;

            // activate audio
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            audio.Play(44100);
		}
	}
}
