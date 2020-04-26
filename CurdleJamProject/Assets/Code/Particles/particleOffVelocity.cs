using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleOffVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float velDeadzone = 0.1f;
    private bool playing = false;

    void Update()
    {
        if (playing == false)
        {
            if (_rb.velocity.magnitude > velDeadzone)
            {
                particle.Play();
                playing = true;
            }
        }
        else
        {
            if (_rb.velocity.magnitude < velDeadzone)
            {
                particle.Stop();
                playing = false;
            }
        }
    }

    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void TriggerParticle(bool pOnOff)
    {
        enabled = pOnOff;

        if (enabled == false && playing)
        {
            particle.Stop();
            playing = false;
        }
    }
}
