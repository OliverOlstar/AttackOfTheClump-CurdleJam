using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsWatcher : MonoBehaviour
{
    public void ARROW()
    {
        controls.controlScheme = 0;
        Destroy(gameObject);
    }

    public void WASD()
    {
        controls.controlScheme = 1;
        Destroy(gameObject);
    }

    public void GAMEPAD()
    {
        controls.controlScheme = 2;
        Destroy(gameObject);
    }
}
