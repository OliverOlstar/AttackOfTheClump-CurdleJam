using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public checkPoint curCheckPoint;
    [SerializeField] private GameObject player;

    public void Respawn()
    {
        // Delay this and add a death effect
        curCheckPoint.Respawn(player);
    }
}
