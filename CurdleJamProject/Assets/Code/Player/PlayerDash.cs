using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private Transform sprite;

    public UnityEvent SwimmingToggle;
    public UnityEvent SwimmingToggle2;
    public UnityEvent Dashed;

    [SerializeField] private float dashDistance = 2;
    [SerializeField] private float dashLength = 1;

    private int dashes = 1;

    private Vector2 _moveInput = new Vector2(0, 0);

    [Header("Swimming")]
    [SerializeField] private float swimSpeed = 5;
    [SerializeField] private float swimRotDampening = 5;
    [SerializeField] private LayerMask sandLayer = new LayerMask();
    private bool swimming = false;
    private bool swimming2 = false;

    [SerializeField] private LayerMask swimmingCollisionLayer = new LayerMask();
    [SerializeField] private float swimmingCollisionCheckDistance = 1;

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
        if (swimming) return;

        if (pAction.performed && dashes > 0 && _moveInput != Vector2.zero)
        {
            StartCoroutine(dashRoutine());
            dashes--;
        }
    }

    private IEnumerator dashRoutine()
    {
        Dashed.Invoke();

        Vector3 moveInput = _moveInput;
        float endTime = Time.time + dashLength;
        while (Time.time < endTime)
        {
            // Check for colliding with sand
            if (Physics.OverlapSphere(transform.position + (Vector3.up * 0.6f) + new Vector3(_moveInput.x, _moveInput.y, 0), 0.8f, sandLayer).Length > 0)
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
        SwimmingToggle.Invoke();
        SwimmingToggle2.Invoke();
        swimming = true;
        swimming2 = true;
        _rb.isKinematic = true;
        float minSwimTime = Time.time + 0.2f;

        while (minSwimTime > Time.time || Physics.OverlapBox(transform.position + new Vector3(0, 0.3f, 0), new Vector3(1, 1.6f, 1) / 4, transform.rotation, sandLayer).Length > 0)
        {
            if (_moveInput != Vector2.zero && _moveInput != pMoveInput)
                pMoveInput = Vector2.Lerp(pMoveInput, _moveInput, Vector2.Distance(pMoveInput, _moveInput) * Time.deltaTime * swimRotDampening).normalized;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, pMoveInput, out hit, swimmingCollisionCheckDistance, swimmingCollisionLayer))
            {
                pMoveInput = Vector2.Reflect(pMoveInput, hit.normal).normalized;
            }

            transform.Translate(pMoveInput.normalized * swimSpeed * Time.deltaTime);

            // Rotate Sprite
            float angle = Mathf.Atan2(pMoveInput.y, pMoveInput.x) * Mathf.Rad2Deg;
            sprite.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            yield return null;
        }

        _rb.isKinematic = false;
        _rb.velocity = pMoveInput.normalized * swimSpeed;

        SwimmingToggle2.Invoke();
        swimming2 = false;

        yield return new WaitForSeconds(0.1f);

        SwimmingToggle.Invoke();
        swimming = false;
        dashes = 1;
    }

    public void Landed()
    {
        dashes = 1;
    }

    public void Respawn()
    {
        StopAllCoroutines();

        if (swimming)
        {
            swimming = false;
            SwimmingToggle.Invoke();
            _rb.isKinematic = false;
        }

        if (swimming2)
        {
            swimming2 = false;
            SwimmingToggle2.Invoke();
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position + (Vector3.up * 0.6f) + new Vector3(_moveInput.x, _moveInput.y, 0), 0.8f);
    //}
}
