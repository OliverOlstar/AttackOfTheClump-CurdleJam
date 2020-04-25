using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnWall : MonoBehaviour
{
    public UnityEvent Landed;
    public UnityEvent Jumped;

    [Header("Physics")]
    [SerializeField] private Rigidbody _rb;
    private bool grounded = false;
    private bool walled = false;
    [SerializeField] [Range(-1, 1)] private float side = 0;

    private List<Collider> others = new List<Collider>();

    private void Start()
    {
        OnJumped();
    }

    private void Update()
    {
        if (others.Count > 0 && grounded == false && movingOntoWall())
        {
            if (walled == false)
                OnLanded();
        }
        else
        {
            if (walled == true)
                OnJumped();
        }
    }

    private bool movingOntoWall()
    {
        if (side > 0 && _rb.velocity.x >= 0)
        {
            return true;
        }

        if (side < 0 && _rb.velocity.x <= 0)
        {
            return true;
        }

        return false;
    }

    private void OnLanded()
    {
        walled = true;
        Landed.Invoke();
    }

    private void OnJumped()
    {
        walled = false;
        Jumped.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        others.Add(other);

        if (others.Count == 1)
            OnLanded();
    }

    private void OnTriggerExit(Collider other)
    {
        others.Remove(other);

        if (others.Count <= 0)
            OnJumped();
    }

    public void JumpedGround()
    {
        grounded = false;
    }

    public void LandedGround()
    {
        grounded = true;
    }
}
