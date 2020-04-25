using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float dashDistance = 2;
    [SerializeField] private float dashLength = 1;

    private int dashes = 1;

    private Vector2 _moveInput = new Vector2(0, 0);

    [Header("Swimming")]
    [SerializeField] private float swimSpeed = 5;
    [SerializeField] private float swimRotDampening = 5;
    [SerializeField] private LayerMask sandLayer = new LayerMask();
    private bool Swimming = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext pAction)
    {
        _moveInput = pAction.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext pAction)
    {
        if (Swimming) return;

        if (pAction.performed && dashes > 0)
        {
            StartCoroutine(dashRoutine());
            dashes--;
        }
    }

    private IEnumerator dashRoutine()
    {
        Vector3 moveInput = _moveInput;
        float endTime = Time.time + dashLength;
        while (Time.time < endTime)
        {
            // Check for colliding with sand
            if (Physics.OverlapSphere(transform.position + new Vector3(_moveInput.x, _moveInput.y, 0) / 2, 0.5f, sandLayer).Length > 0)
            {
                // Start Swimming
                StartCoroutine(swimmingRoutine(moveInput));
                break;
            }

            _rb.velocity = moveInput.normalized * (dashDistance / dashLength);
            yield return null;
        }
    }

    private IEnumerator swimmingRoutine(Vector2 pMoveInput)
    {
        _rb.isKinematic = true;
        float minSwimTime = Time.time + 0.5f;

        while (minSwimTime > Time.time || Physics.OverlapBox(transform.position, transform.localScale / 4, transform.rotation, sandLayer).Length > 0)
        {
            if (_moveInput != Vector2.zero && _moveInput != pMoveInput)
                pMoveInput = Vector2.Lerp(pMoveInput, _moveInput, Vector2.Distance(pMoveInput, _moveInput) * Time.deltaTime * swimRotDampening).normalized;

            transform.Translate(pMoveInput.normalized * swimSpeed * Time.deltaTime);
            yield return null;
        }

        _rb.isKinematic = false;
        _rb.velocity = pMoveInput * swimSpeed;
    }

    public void Landed()
    {
        dashes = 1;
    }
}
