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

    private bool grounded = false;
    private int walled = 0;
    private bool swimming = false;

    private Vector3 swimmingOffset = new Vector3(0, 0.5f, 0);

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalVel = _moveInput.x;

        if (swimming)
            return;

        if (walled == 0 || grounded)
        {
            if (horizontalVel != 0)
                transform.localScale = new Vector3(-1 * (Mathf.Abs(horizontalVel) / horizontalVel), 1, 1);
        }
        else 
        {
            transform.localScale = new Vector3(walled, 1, 1);
        }

        _anim.SetBool("Moving", (Mathf.Abs(horizontalVel) > movingDeadzone));
    }

    public void OnMove(InputAction.CallbackContext pAction)
    {
        _moveInput = pAction.ReadValue<Vector2>();
    }

    public void OnGround()
    {
        grounded = true;
        _anim.SetBool("Grounded", true);
    }

    public void OffGround()
    {
        grounded = false;
        _anim.SetBool("Grounded", false);
    }

    public void OnLeftWall()
    {
        walled = -1;
        _anim.SetBool("Walled", true);
    }

    public void OnRightWall()
    {
        walled = 1;
        _anim.SetBool("Walled", true);
    }

    public void OffWall()
    {
        walled = 0;
        _anim.SetBool("Walled", false);
    }

    public void OnSwimming()
    {
        swimming = !swimming;
        _anim.SetTrigger("Swimming");
        _anim.SetBool("SwimmingBool", swimming);

        if (swimming)
        {
            StopAllCoroutines();
            transform.localPosition += swimmingOffset;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localPosition -= swimmingOffset;
            //transform.rotation = Quaternion.identity;
            StartCoroutine(rotateBack());
        }
    }

    private IEnumerator rotateBack()
    {
        while (Quaternion.Angle(transform.rotation, Quaternion.identity) > 4)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 10f * Time.deltaTime);
            yield return null;
        }

        transform.rotation = Quaternion.identity;
    }

    public void Respawn()
    {
        if (swimming)
        {
            transform.localPosition -= swimmingOffset;
        }

        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
    }
}
