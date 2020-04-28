using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Rigidbody targetRigidbody;
    private Vector3 velocityOffset = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    public CameraZone zone;
    [SerializeField] private float dampening = 5;
    [SerializeField] private float velDampening = 5;
    [SerializeField] private float velocityInfluenceMult = 1;

    [SerializeField] private Color color = new Color(1, 1, 1, 0.5f);
    [SerializeField] private Vector2 scale = new Vector2(1, 1);

    [HideInInspector] public bool useZones = true;

    void Update()
    {
        velocityOffset = Vector3.Lerp(velocityOffset, targetRigidbody.velocity * velocityInfluenceMult, velDampening * Time.deltaTime);

        // Get Target Position
        Vector2 targetPos = target.position + velocityOffset + offset;

        // Clamp To Current Camera Zone
        if (zone != null && useZones)
        {
            targetPos.x = Mathf.Clamp(targetPos.x, zone.minX() + scale.x, zone.maxX() - scale.x);
            targetPos.y = Mathf.Clamp(targetPos.y, zone.minY() + scale.y, zone.maxY() - scale.y);
        }

        // Get Next Position
        Vector3 nextPos = Vector2.Lerp(transform.position, targetPos, dampening * Time.deltaTime);
        nextPos.z = -10;

        // Set Position
        transform.position = nextPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, scale * 2);
        Gizmos.DrawWireCube(transform.position, scale * 2);
    }
}
