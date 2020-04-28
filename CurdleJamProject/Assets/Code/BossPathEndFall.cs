using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathEndFall : MonoBehaviour
{
    [SerializeField] private float gravity = 5;
    [SerializeField] private Vector3 rot = new Vector3(1, 0, 0);
    [SerializeField] private float endSpeed = 5;
    [SerializeField] private float delay = 1;

    public void StartFalling()
    {
        StartCoroutine("Falling");
    }

    public IEnumerator Falling()
    {
        yield return new WaitForSeconds(delay);

        float vel = 0;

        while (vel <= endSpeed)
        {
            yield return null;
            vel += Time.deltaTime * gravity;
            transform.Translate(Vector3.down * vel, Space.World);
            transform.Rotate(rot * Time.deltaTime);
        }

        MusicPlayer.instance.PlayNormal();
    }
}
