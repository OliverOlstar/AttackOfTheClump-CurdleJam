using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChase : MonoBehaviour
{
    [SerializeField] private SplineWalker boss;
    [SerializeField] private Animator sprite;
    private BossPathEndFall falling;

    [Header("Part 1")]
    [SerializeField] private BezierSpline firstPath;

    [SerializeField] private int state = 0;
    // 0 - Falling Anim
    // 1 - First Chase
    // 2 - Second Chase
    // 3 - Waiting To Splash

    [SerializeField] private float fallEndProgress = 0.1f;
    [SerializeField] private float fallSpeed = 1;
    private float defaultSpeed = 0;
    private float defaultDistanceSpeed = 0;

    [Header("Part 2")]
    [SerializeField] private checkPoint firstCheckpoint;
    [SerializeField] private BezierSpline secondPath;

    [Header("Part 3")]
    [SerializeField] private checkPoint lastCheckpoint;
    [SerializeField] private BezierSpline thirdPath;

    private void Start()
    {
        falling = GetComponent<BossPathEndFall>();

        defaultSpeed = boss.speedDefault;
        defaultDistanceSpeed = boss.speedDistanceMult;

        sprite.SetTrigger("GoingUp");
        Respawn();
    }

    void Update()
    {
        switch (state)
        {
            // Falling Anim
            case 0:
                // Watch for end of falling
                if (boss.enabled && boss.progress >= fallEndProgress)
                {
                    sprite.ResetTrigger("GoingUp");
                    sprite.SetTrigger("Landed");

                    boss.lookAt = true;
                    boss.neverStop = false;

                    boss.speedDefault = defaultSpeed;
                    boss.speedDistanceMult = defaultDistanceSpeed;

                    state = 1;
                }
                break;

            case 1:

                break;
        }
    }

    public void Respawn()
    {
        StopAllCoroutines();

        // Return to falling state
        if (lifeManager._instance.curCheckPoint == firstCheckpoint)
        {
            boss.speedDefault = fallSpeed;
            boss.speedDistanceMult = 0;

            boss.lookAt = false;
            boss.neverStop = true;

            sprite.SetTrigger("GoingUp");
            sprite.ResetTrigger("Landed");

            switch(state)
            {
                case 2:
                    boss.spline = firstPath;
                    boss.progress = 0;
                    break;
            }

            state = 0;
        }
        else
        {
            sprite.SetTrigger("GoingUp");
            sprite.ResetTrigger("Splash");

            boss.lookAt = false;
            boss.neverStop = true;

            boss.spline = secondPath;

            state = 2;
        }

    }

    public void ReachedEnd()
    {
        switch(state)
        {
            // First Chase
            case 1:
                sprite.ResetTrigger("Landed");
                sprite.SetTrigger("GoingUp");

                boss.lookAt = false;
                boss.neverStop = true;

                boss.spline = secondPath;
                boss.progress = 0;

                boss.transform.rotation = Quaternion.Euler(0, -90, 0);

                StartCoroutine("GoingUpDelay");

                state = 2;
                break;

            case 2:
                boss.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;

            case 3:
                falling.StartFalling();
                lastCheckpoint.SetSpawn();
                break;
        }
    }

    private IEnumerator GoingUpDelay()
    {
        yield return null;
        boss.enabled = true;
    }

    public void StartSplash()
    {
        if (state == 2)
        {
            sprite.ResetTrigger("Landed");
            sprite.ResetTrigger("GoingUp");
            sprite.SetTrigger("Splash");

            boss.progress = 0;
            boss.spline = thirdPath;

            boss.neverStop = false;
            boss.lookAt = true;

            state = 3;

            EZCameraShake.CameraShaker.Instance.ShakeOnce(10, 5, 1, 3);

            StartCoroutine("SplashDelay");
        }
    }

    private IEnumerator SplashDelay()
    {
        yield return new WaitForSeconds(1);
        boss.enabled = true;
    }
}
