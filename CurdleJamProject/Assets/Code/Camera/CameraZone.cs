using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private Color color = new Color(1, 1, 1, 0.5f);

    public float maxX()
    {
        return transform.position.x + transform.localScale.x;
    }

    public float minX()
    {
        return transform.position.x - transform.localScale.x;
    }

    public float maxY()
    {
        return transform.position.y + transform.localScale.y;
    }

    public float minY()
    {
        return transform.position.y - transform.localScale.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, transform.localScale * 2);
        Gizmos.DrawWireCube(transform.position, transform.localScale * 2);
    }
}
