using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGround : MonoBehaviour
{
    public UnityEvent Landed;
    public UnityEvent Jumped;

    [Header("Physics")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float airDrag = 1;
    [SerializeField] private float groundDrag = 0;

    [SerializeField] private LayerMask layerMask = new LayerMask();

    private List<Collider> others = new List<Collider>();

    private bool swimming = false;

    private void Start()
    {
        OnJumped();
    }

    private void OnLanded()
    {
        Landed.Invoke();
        _rb.drag = groundDrag;

        // Smooth Landing
        float mag = _rb.velocity.magnitude;
        Vector2 vel = _rb.velocity;
        vel.y = vel.y / 10;
        vel = vel.normalized * mag;
        _rb.velocity = vel;
    }

    private void OnJumped()
    {
        if (swimming == false)
            Jumped.Invoke();
        _rb.drag = airDrag;
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

    public void OnSwimming() => swimming = !swimming;
}
