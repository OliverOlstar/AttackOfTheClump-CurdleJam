using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private Vector3 axis = new Vector3(0, 0, 1);
    private float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(axis * (Random.value * 360));

        float value = Random.value - 0.2f;
        dir = (Random.value - 0.5f) * 0.25f + Mathf.Abs(value) / value;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis * Time.deltaTime * dir);
    }
}
