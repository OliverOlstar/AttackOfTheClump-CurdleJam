using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect.instance.AddOne();
            GetComponentInChildren<AudioSource>().Play();
            GetComponentInChildren<ParticleSystem>().Play();
            Destroy(transform.GetChild(0).gameObject, 3);
            transform.GetChild(0).SetParent(null);
            Destroy(gameObject);
        }
    }
}
