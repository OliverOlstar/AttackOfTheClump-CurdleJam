using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIntoPlace : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 targetPos;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    public void TriggerMe()
    {
        StartCoroutine(Fall());
    }

    public void ResetMe()
    {
        StopAllCoroutines();
        transform.localPosition = startPos;
    }

    private IEnumerator Fall()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.parent.position + targetPos, 0.5f);
    }
}
