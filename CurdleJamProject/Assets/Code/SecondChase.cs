using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChase : MonoBehaviour
{
    [SerializeField] private SplineWalker boss;
    [SerializeField] private Animator sprite;
    private bool Started = false;
    [SerializeField] private float fallEndProgress = 0.1f;

    [SerializeField] private float fallSpeed = 1;
    private float defaultSpeed = 0;
    private float defaultDistanceSpeed = 0;

    private void Start()
    {
        defaultSpeed = boss.speedDefault;
        defaultDistanceSpeed = boss.speedDistanceMult;

        sprite.SetTrigger("GoingUp");
        Respawn();
    }

    void Update()
    {
        if (boss.enabled && boss.progress >= fallEndProgress)
        {
            sprite.SetTrigger("Landed");

            boss.lookAt = true;
            boss.neverStop = false;

            boss.speedDefault = defaultSpeed;
            boss.speedDistanceMult = defaultDistanceSpeed;

            enabled = false;
        }
    }

    public void Respawn()
    {
        boss.speedDefault = fallSpeed;
        boss.speedDistanceMult = 0;

        boss.lookAt = false;
        boss.neverStop = true;

        if (enabled == false)
            sprite.SetTrigger("GoingUp");
        sprite.ResetTrigger("Landed");

        enabled = true;
    }

    public void GoingUp()
    {
        sprite.SetTrigger("GoingUp");
    }
}
