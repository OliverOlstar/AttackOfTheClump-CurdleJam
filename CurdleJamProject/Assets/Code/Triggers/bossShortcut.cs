using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShortcut : MonoBehaviour
{
    [SerializeField] private SplineWalker boss;
    [SerializeField] private float progress = 0;
    [SerializeField] private bool neverStop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (boss.progress < progress)
            boss.progress = progress;
        boss.neverStop = neverStop;

        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
