using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource[] audioSources = new AudioSource[0];
    private int index = 0;

    public bool isSwimming = false;

    [SerializeField] private AudioClip jumped;
    [SerializeField] private AudioClip landed;
    [SerializeField] private AudioClip[] hurt = new AudioClip[0];
    [SerializeField] private AudioClip swimming;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip collectable;
    [SerializeField] private AudioClip dash;

    private AudioSource swimmingSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    private AudioSource PlaySound(AudioClip pClip, bool pLoop = false)
    {
        index++;
        if (index == audioSources.Length) 
            index = 0;

        audioSources[index].loop = pLoop;
        audioSources[index].pitch = Random.Range(0.7f, 1.3f);
        audioSources[index].clip = pClip;
        audioSources[index].Play();

        return audioSources[index];
    }

    public void OnJumped()
    {
        PlaySound(jumped);
    }

    public void OnLanded()
    {
        if (isSwimming) 
            return;

        PlaySound(landed);
    }

    public void OnSwimming()
    {
        Debug.Log("Swim SFX");
        isSwimming = !isSwimming;

        if (isSwimming)
            swimmingSource = PlaySound(swimming, true);
        else
        {
            swimmingSource.loop = false;
            swimmingSource.Stop();
        }
    }

    public void Died()
    {
        int rand = Mathf.RoundToInt(Random.value * (hurt.Length - 1));
        PlaySound(hurt[rand]);
    }

    public void Lives()
    {
        PlaySound(respawn);
    }

    public void Collectable()
    {
        PlaySound(collectable);
    }

    public void OnDash()
    {
        PlaySound(dash);
    }
}
