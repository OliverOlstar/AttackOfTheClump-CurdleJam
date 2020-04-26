using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTrigger : MonoBehaviour
{
    [SerializeField] private SplineWalker boss;
    [SerializeField] private BezierSpline bossPath;
    private float triggerDelayEndTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerDelayEndTime < Time.time)
        {
            Debug.Log("Boss Trigger");
            boss.spline = bossPath;
            boss.Respawn(0);
            boss.enabled = true;
            boss.target = other.transform;
            triggerDelayEndTime = Time.time + 1.0f;
            EZCameraShake.CameraShaker.Instance.ShakeOnce(10, 5, 3, 5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
