using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalEffect : MonoBehaviour
{
    public UnityEvent end;

    [SerializeField] private Vector3 endPos;
    [SerializeField] private Vector3 endPos2;
    [SerializeField] private float distanceReduction = 1;
    private float distance = 0;
    public Transform player;
    private float orthoSize = 0;

    [SerializeField] private float newSize = 2;

    private void Start()
    {
        distance = endPos.magnitude;
        orthoSize = Camera.main.orthographicSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            StartCoroutine(routine());
            MusicPlayer.instance.PlayFinal();
            Camera.main.GetComponentInParent<CameraCont>().useZones = false;
            end.Invoke();
        }
    }

    private IEnumerator routine()
    {
        EZCameraShake.CameraShaker.Instance.StartShake(20, 10, 5);

        while (Vector3.Distance(transform.position + endPos, player.position) > distanceReduction)
        {
            Camera.main.orthographicSize = Mathf.Lerp(orthoSize, newSize, 1 - Vector3.Distance(transform.position + endPos, player.position) / distance);
            yield return null;
        }

        float time = 0;
        Vector3 pos = player.position;
        EZCameraShake.CameraShaker.Instance.ShakeInstances.Clear();

        while (time < 1)
        {
            time += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(newSize, 15, time);
            //player.position = Vector3.Lerp(pos, transform.position + endPos2, time);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + endPos, 1);
        Gizmos.DrawSphere(transform.position + endPos2, 1);
    }
}
