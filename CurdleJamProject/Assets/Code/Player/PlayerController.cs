using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent groundJumped;
    public UnityEvent wallJumped;
    private Rigidbody _rb;

    [SerializeField] private float moveForce = 5;
    [SerializeField] private float jumpForce = 2;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(0, 0);
    private bool grounded = false;
    private bool walled = false;
    private bool Swimming = false;

    public float wallDir = 0;
    private float wallGrace = 0;

    private float lastJumpTime = 0;
    [SerializeField] private float jumpCooldown = 0.1f;

    [Header("Input")]
    [SerializeField] private float inputInfluGround = 1.0f;
    [SerializeField] private float inputInfluAir = 0.6f;
    private float inputInfluence = 1;

    private Vector2 _moveInput = new Vector2(0, 0);

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Swimming) return;

        _rb.AddForce(_moveInput.x * Vector2.right * moveForce * inputInfluence * Time.deltaTime);
    }

    public void OnJump(InputAction.CallbackContext pAction)
    {
        if (Swimming) return;

        float input = pAction.ReadValue<float>();

        // Pressed
        if (input == 1)
        {
            if (lastJumpTime + jumpCooldown > Time.time) return;

            Vector3 vel = _rb.velocity;
            if (grounded)
            {
                groundJumped.Invoke();
                vel.y = jumpForce;
            }
            else if (walled || wallGrace > Time.time)
            {
                wallJumped.Invoke();
                vel.y = wallJumpForce.y;
                vel.x = wallJumpForce.x * wallDir;
                wallGrace = 0;
            }

            _rb.velocity = vel;
            lastJumpTime = Time.time;
        }
        // Released
        else if (input == 0)
        {
            if (_rb.velocity.y > 1.0f)
            {
                Vector3 vel = _rb.velocity;
                vel.y *= 0.7f;
                _rb.velocity = vel;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext pAction)
    {
        _moveInput = pAction.ReadValue<Vector2>();
    }

    #region Ground
    public void Landed()
    {
        grounded = true;
        inputInfluence = inputInfluGround;
    }

    public void Jumped()
    {
        grounded = false;
        inputInfluence = inputInfluAir;
    }


    public void WallLandedRight()
    {
        wallDir = 1;
        walled = true;
    }

    public void WallLandedLeft()
    {
        wallDir = -1;
        walled = true;
    }

    public void WallJumped()
    {
        walled = false;
        if (lastJumpTime + jumpCooldown < Time.time)
            wallGrace = Time.time + 0.2f;
    }
    #endregion

    public void OnSwimming() => Swimming = !Swimming;
}
