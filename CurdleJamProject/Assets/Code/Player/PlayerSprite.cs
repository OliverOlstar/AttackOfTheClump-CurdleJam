using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private Animator _anim;
    [SerializeField] private float movingDeadzone = 0.1f;
    private Vector2 _moveInput = new Vector2(0, 0);

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalVel = _moveInput.x;

        if (horizontalVel != 0)
            transform.localScale = new Vector3(-1 * (Mathf.Abs(horizontalVel) / horizontalVel), 1, 1);
        _anim.SetBool("Moving", (Mathf.Abs(horizontalVel) > movingDeadzone));
    }

    public void OnMove(InputAction.CallbackContext pAction)
    {
        _moveInput = pAction.ReadValue<Vector2>();
    }
}
