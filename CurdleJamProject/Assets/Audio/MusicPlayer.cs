using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    [SerializeField] private Transform player;
    private Transform boss;

    private bool playingSpooky = false;

    private AudioSource[] source;

    [SerializeField] private AudioClip NothingWrong;
    [SerializeField] private AudioClip EverythingWrong;
    [SerializeField] private AudioClip SpookyNoise;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        source = GetComponents<AudioSource>();
    }

    public void PlaySpookySong(Transform pBoss)
    {
        if (playingSpooky == false)
        {
            playingSpooky = true;
            boss = pBoss;

            source[0].clip = EverythingWrong;
            source[0].Play();

            source[1].clip = SpookyNoise;
            source[1].Play();
        }
    }


    public void PlayNormal()
    {
        if (playingSpooky == true)
        {
            playingSpooky = false;

            source[0].clip = NothingWrong;
            source[0].Play();

            source[1].Stop();
        }
    }

    private void Update()
    {
        if (playingSpooky)
        {
            source[1].volume = Mathf.Clamp(1 - Vector3.Distance(boss.position, player.position) / 20, 0.0f, 0.8f);
            Debug.Log(Vector3.Distance(boss.position, player.position));
        }
    }
}
