﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSplashTrigger : MonoBehaviour
{
    [SerializeField] private SecondChase boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            boss.StartSplash();
    }
}
