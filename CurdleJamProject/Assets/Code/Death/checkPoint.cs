using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    [SerializeField] private float bossProgress = 0;
    [SerializeField] private SplineWalker boss;

    private void OnTriggerEnter(Collider other)
    {
        lifeManager._instance.curCheckPoint = this;
    }

    public void SetSpawn()
    {
        lifeManager._instance.curCheckPoint = this;
    }

    public void Respawn(GameObject pPlayer)
    {
        pPlayer.transform.position = transform.position;
        pPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pPlayer.GetComponent<PlayerDash>().Respawn();

        if (boss != null)
            boss.Respawn(bossProgress); 
    }
}
