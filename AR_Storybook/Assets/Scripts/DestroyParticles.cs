using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {

    private ParticleSystem particlesystem;

	// Use this for initialization
	void Start () {
        particlesystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!particlesystem.IsAlive())
            Destroy(gameObject);
	}
}
