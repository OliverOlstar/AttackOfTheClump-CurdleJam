﻿using System.Collections;
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
    [SerializeField] private AudioClip Acending;

    private bool swimming = false;

    private float defaultPitch = 1;
    private float defaultVolume = 1;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        source = GetComponents<AudioSource>();

        defaultVolume = source[0].volume;
        defaultPitch = source[0].pitch;
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

    public void PlayFinal()
    {
        if (playingSpooky == false)
        {
            playingSpooky = true;

            source[0].clip = Acending;
            source[0].Play();

            source[1].Stop();

            boss = null;
        }
    }

    private void Update()
    {
        if (playingSpooky && boss != null)
        {
            source[1].volume = Mathf.Clamp(1 - Vector3.Distance(boss.position, player.position) / 20, 0.0f, 0.8f);
        }
    }

    public void Swimming()
    {
        swimming = !swimming;

        if (swimming && playingSpooky == false)
        {
            source[0].volume = defaultVolume - 0.3f;
            source[0].pitch = defaultPitch - 0.3f;
        }
        else
        {
            source[0].volume = defaultVolume;
            source[0].pitch = defaultPitch;
        }
    }
}
