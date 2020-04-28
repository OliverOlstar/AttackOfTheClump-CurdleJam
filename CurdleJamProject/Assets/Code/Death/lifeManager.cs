using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class lifeManager : MonoBehaviour
{
    #region Singleton
    public static lifeManager _instance;

    private void Awake()
    {
        if (_instance != this)
        {
            if (_instance != null)
                Destroy(_instance);

            _instance = this;
        }
    }
    #endregion

    public UnityEvent playerDied;

    [Space]
    public checkPoint curCheckPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Transform cameraTrigger;
    [SerializeField] private CameraCont camera;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private RippleEffect ripple;

    [Header("Anim")]
    [SerializeField] private ParticleSystem bloodSplater;
    [SerializeField] private Transform orb;

    public void Respawn()
    {
        // Delay this and add a death effect
        
        bloodSplater.transform.position = player.transform.position;
        bloodSplater.Play();

        //EZCameraShake.CameraShaker.Instance.ShakeOnce(15, 5, 0.2f, 1.2f);

        StartCoroutine(RespawnDelay());
        playerDied.Invoke();
    }

    private IEnumerator RespawnDelay()
    {
        Vector3 startPos = player.transform.position;
        Vector3 endPos = curCheckPoint.transform.position;

        orb.gameObject.SetActive(true);
        orb.position = startPos;
        orb.parent.rotation = Quaternion.LookRotation(endPos - startPos);

        camera.useZones = false;

        player.transform.position = endPos;

        playerSprite.enabled = false;
        player.SetActive(false);

        float time = 0;

        while (time < 1)
        {
            yield return null;
            time += Time.deltaTime * 2;
            orb.position = Vector2.Lerp(startPos, endPos, time);
        }

        yield return null;

        curCheckPoint.Respawn(player);

        camera.useZones = true;

        player.SetActive(true);
        playerSprite.enabled = true;
        orb.gameObject.SetActive(false);

        particle.Play();
        ripple.Emit(Camera.main.WorldToViewportPoint(endPos));
    }
}
