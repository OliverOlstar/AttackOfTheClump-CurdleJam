using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlePlayer : MonoBehaviour
{
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void TriggerParticle()
    {
        particle.Play();
    }
}
