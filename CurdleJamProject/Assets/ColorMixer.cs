using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particle;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(toRed());
    }

    private IEnumerator toRed()
    {
        float time = 0;
        Color startColor = sprite.color;

        while (time < 1)
        {
            time += Time.deltaTime;
            sprite.color = Color.Lerp(startColor, new Color(255, 0, 0), time);
            if (particle != null)
                particle.startColor = sprite.color;
            yield return null;
        }

        StartCoroutine(toGreen());
    }

    private IEnumerator toGreen()
    {
        float time = 0;
        Color startColor = sprite.color;

        while (time < 1)
        {
            time += Time.deltaTime;
            sprite.color = Color.Lerp(startColor, new Color(0, 255, 0), time);
            if (particle != null)
                particle.startColor = sprite.color;
            yield return null;
        }

        StartCoroutine(toBlue());
    }

    private IEnumerator toBlue()
    {
        float time = 0;
        Color startColor = sprite.color;

        while (time < 1)
        {
            time += Time.deltaTime;
            sprite.color = Color.Lerp(startColor, new Color(0, 0, 255), time);
            if (particle != null)
                particle.startColor = sprite.color;
            yield return null;
        }

        StartCoroutine(toRed());
    }
}
