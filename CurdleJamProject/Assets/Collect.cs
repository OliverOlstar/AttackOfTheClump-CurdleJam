using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public static Collect instance;
    private TextMesh text;

    public int Collected = 0;
    [SerializeField] private float stayTime = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        text = GetComponent<TextMesh>();
    }

    public void AddOne()
    {
        Collected++;
        StopAllCoroutines();
        StartCoroutine(displayRoutine());
    }

    private IEnumerator displayRoutine()
    {
        text.text = Collected + " / 6";
        yield return new WaitForSeconds(stayTime);
        text.text = " ";
    }
}
