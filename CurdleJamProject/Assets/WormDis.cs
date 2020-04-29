using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormDis : MonoBehaviour
{
    [SerializeField] private int wormCount = 0;
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Display()
    {
        renderer.enabled = (Collect.instance.Collected >= wormCount);
    }
}
