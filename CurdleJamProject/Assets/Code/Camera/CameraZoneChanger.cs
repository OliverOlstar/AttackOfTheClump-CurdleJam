using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoneChanger : MonoBehaviour
{
    [SerializeField] private CameraCont camera;
    private List<CameraZone> otherZones = new List<CameraZone>();

    private void OnTriggerEnter(Collider other)
    {
        CameraZone newZone = other.GetComponent<CameraZone>();

        if (newZone != null)
        {
            camera.zone = newZone;
            otherZones.Add(newZone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CameraZone oldZone = other.GetComponent<CameraZone>();
        
        if (oldZone != null)
        {
            otherZones.Remove(oldZone);

            if (camera.zone == oldZone)
            {
                if (otherZones.Count > 0)
                    camera.zone = otherZones[otherZones.Count - 1];
            }
        }
    }
}
