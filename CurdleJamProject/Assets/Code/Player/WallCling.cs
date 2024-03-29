﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCling : MonoBehaviour
{
    private float strength = 0;
    [SerializeField] private float maxStrength = 5;
    [SerializeField] private float strengthRate = 5;
    [SerializeField] private float strengthDeadzone = 10;

    [Space]
    [SerializeField] private float delayLength = 0.2f;
    private float delayEndTime = 0;

    private bool Swimming = false;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    #region Ground
    public void Landed()
    {
        strength = maxStrength;
    }

    // Landed On a Wall
    public void WallLanded()
    {
        if (Swimming) return;

        if (strength > 1)
        {
            StartCoroutine(StartSlowFall());
        }
    }

    // Left Wall
    public void WallJumped()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        StopAllCoroutines();
    }
    #endregion

    private IEnumerator StartSlowFall()
    {
        while (Time.time < delayEndTime || _rb.velocity.y > 0)
        {
            yield return null;
        }

        StartCoroutine(SlowFall());
    }

    private IEnumerator SlowFall()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        while (strength > 1)
        {
            Vector3 vel = _rb.velocity;

            if (strength > strengthDeadzone)
                vel.y = 0;
            else
            {
                _rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                vel.y = vel.y / strength;
            }

            _rb.velocity = vel;

            yield return null;
            strength -= Time.deltaTime * strengthRate;
        }
    }

    // Any type of jump happened
    public void Jumped()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        delayEndTime = Time.time + delayLength;
    }

    public void OnSwimming() => Swimming = !Swimming;
}
