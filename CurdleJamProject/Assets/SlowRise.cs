using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlowRise : MonoBehaviour
{
    public UnityEvent StartFalling;

    [SerializeField] private float speed = 1;
    private bool moving = false;
    private Vector3 startingPos;

    [SerializeField] private float targetY = 0;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (moving == false && other.CompareTag("Player"))
        {
            moving = true;
            StartFalling.Invoke();
            StartCoroutine(MovingRoutine());
            EZCameraShake.CameraShaker.Instance.ShakeOnce(16, 9, 4, 5);
            MusicPlayer.instance.PlaySpookySong(null);
        }
    }

    private IEnumerator MovingRoutine()
    {
        while (transform.position.y < targetY)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            yield return null;
        }
    }

    public void ResetMe()
    {
        StopAllCoroutines();
        moving = false;
        transform.position = startingPos;
        MusicPlayer.instance.PlayNormal();
    }
}
