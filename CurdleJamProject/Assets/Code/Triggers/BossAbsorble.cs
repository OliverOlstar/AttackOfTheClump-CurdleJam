using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbsorble : MonoBehaviour
{
    [SerializeField] private GameObject breakingSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            Instantiate(breakingSound).GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1f);
            Destroy(gameObject);
        }
    }
}
