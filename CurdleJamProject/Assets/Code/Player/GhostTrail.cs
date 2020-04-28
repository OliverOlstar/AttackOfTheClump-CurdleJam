using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private ParticleSystem dashEffect;

    [SerializeField] private float startDelay = 0.1f;
    [SerializeField] private float delayBetween = 0.3f;

    private SpriteRenderer[] ghosts = new SpriteRenderer[0];

    private void Awake()
    {
        ghosts = GetComponentsInChildren<SpriteRenderer>();
    }

    public void Play()
    {
        StartCoroutine(Trail());
    }

    public void Stop()
    {
        StopAllCoroutines();
        dashEffect.Stop();
    }

    private IEnumerator Trail()
    {
        dashEffect.Play();

        yield return new WaitForSeconds(startDelay);

        foreach (SpriteRenderer ghost in ghosts)
        {
            ghost.gameObject.SetActive(true);
            ghost.sprite = player.sprite;

            ghost.transform.position = player.transform.position;
            ghost.transform.localScale = player.transform.localScale;
            ghost.transform.rotation = player.transform.rotation;

            yield return new WaitForSeconds(delayBetween);
        }

        dashEffect.Stop();
    }
}
