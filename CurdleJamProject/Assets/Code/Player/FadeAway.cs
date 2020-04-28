using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float length = 0.6f;

    private SpriteRenderer sprite;

    [SerializeField] private Color startColor = new Color(0, 0, 0, 1);
    [SerializeField] private Color endColor = new Color(0, 0, 0, 0);

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Fade());
    }

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    private IEnumerator Fade()
    {
        sprite.color = startColor;

        yield return new WaitForSeconds(delay);

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / length;
            sprite.color = Color.Lerp(startColor, endColor, time);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
