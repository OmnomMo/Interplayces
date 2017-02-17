using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotation : MonoBehaviour {

    ParticleSystem.Particle[] allParticles;
    ParticleSystem s_Particles;

    // Use this for initialization
    void Start () {
        allParticles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().main.maxParticles];
        s_Particles = GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void LateUpdate () {

        if (GameState.Instance != null && GameState.Instance.isPlayerCaptain())
        {
            s_Particles.emissionRate = Vector3.Magnitude(SpaceshipParts.Instance.GetComponent<Rigidbody>().velocity * 1f);
        } else
        {
            s_Particles.emissionRate = 0;
        }

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = s_Particles.GetParticles(allParticles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            // allParticles[i].rotation = SpaceshipParts.Instance.gameObject.transform.eulerAngles.y;
            allParticles[i].rotation = Quaternion.LookRotation(SpaceshipParts.Instance.GetComponent<Rigidbody>().velocity, Vector3.up).eulerAngles.y;

            allParticles[i].startSize3D = new Vector3(1, Vector3.Magnitude(SpaceshipParts.Instance.GetComponent<Rigidbody>().velocity) * 0.08f, 1);



            allParticles[i].color = new Color(1, 1, 1, Vector3.Magnitude(SpaceshipParts.Instance.GetComponent<Rigidbody>().velocity) * 0.05f);


            //Debug.Log(SpaceshipParts.Instance.gameObject.transform.eulerAngles.y);

        }

        // Apply the particle changes to the particle system
        s_Particles.SetParticles(allParticles, numParticlesAlive);
    }
}
