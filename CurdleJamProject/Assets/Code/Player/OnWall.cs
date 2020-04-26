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

    [SerializeField] private LayerMask layerMask = new LayerMask();

    private List<Collider> others = new List<Collider>();

    private bool swimming = false;

    private void Update()
    {
        if (others.Count > 0 && grounded == false && swimming == false && movingOntoWall())
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
        if (side > 0 && _rb.velocity.x >= -0.1f)
        {
            return true;
        }

        if (side < 0 && _rb.velocity.x <= 0.1f)
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
        if (layerMask != (layerMask | (1 << other.gameObject.layer)))
            return;

        others.Add(other);

        if (others.Count == 1)
            OnLanded();
    }

    private void OnTriggerExit(Collider other)
    {
        if (layerMask != (layerMask | (1 << other.gameObject.layer)))
            return;

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

    public void OnSwimming()
    {
        swimming = !swimming;

        if (swimming && walled)
            OnJumped();
    }
}
