using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    [SerializeField] private float mag = 10.0f;
    [SerializeField] private float rough = 10.0f;
    [SerializeField] private float inTime = 1.0f;
    [SerializeField] private float outTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            EZCameraShake.CameraShaker.Instance.ShakeOnce(mag, rough, inTime, outTime);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
