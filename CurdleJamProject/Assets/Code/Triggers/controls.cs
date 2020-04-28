using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    public static int controlScheme = 0;

    [SerializeField] private Sprite[] controlSprites = new Sprite[3];
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        renderer.sprite = controlSprites[controlScheme];
    }
}
