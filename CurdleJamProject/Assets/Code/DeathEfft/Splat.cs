using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    [SerializeField] private float minSize = 0.8f;
    [SerializeField] private float maxSize = 1.5f;

    public Sprite[] sprites = new Sprite[0];

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int pOrder)
    {
        // Random Sprite
        if (sprites.Length > 0)
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Random Size
        transform.localScale *= Random.Range(minSize, maxSize);

        // Random Rotation
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        // Set Order
        spriteRenderer.sortingOrder = pOrder;
    }
}
