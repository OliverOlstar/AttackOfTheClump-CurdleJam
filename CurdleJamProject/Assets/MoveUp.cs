using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private Vector3 defaultPos;
    [SerializeField] private Vector3 targetPos;

    private Animator anim;

    private void Start()
    {
        defaultPos = transform.localPosition;
        anim = GetComponent<Animator>();
    }

    public void TriggerMe()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        //yield return new WaitForSeconds(1);
        anim.ResetTrigger("Reset");
        anim.SetTrigger("Go");

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(defaultPos, targetPos, time);
            yield return null;
        }
    }

    public void ResetMe()
    {
        StopAllCoroutines();
        transform.localPosition = defaultPos;
        anim.SetTrigger("Reset");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.parent.position + targetPos, 1);
    }
}
