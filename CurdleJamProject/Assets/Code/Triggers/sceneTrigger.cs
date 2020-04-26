using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTrigger : MonoBehaviour
{
    static List<int> LoadedScenes = new List<int>();

    [SerializeField] private int scene;
    [SerializeField] private bool unload;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (unload)
            {
                if (LoadedScenes.Contains(scene))
                {
                    LoadedScenes.Remove(scene);
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
            else
            {
                if (!LoadedScenes.Contains(scene))
                {
                    LoadedScenes.Add(scene);
                    SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                }
            }
        }
    }
}
