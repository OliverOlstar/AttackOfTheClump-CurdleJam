using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem splatParticles;
    [SerializeField] private GameObject splatPrefab;
    private Transform splatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        //splatHolder = new GameObject().transform;
        //splatHolder.name = "SplatHolder";
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(splatParticles, other, collisionEvents);

        foreach (ParticleCollisionEvent collision in collisionEvents)
        {
            GameObject splat = Instantiate(splatPrefab, collision.intersection, Quaternion.identity) as GameObject;
            if (collision.colliderComponent.CompareTag("Bloodable"))
            {
                splat.GetComponent<Splat>().Init(-1);
                splat.transform.SetParent(collision.colliderComponent.transform, true);
            }
            else
            {
                splat.GetComponent<Splat>().Init(3);
                splat.transform.SetParent(splatHolder, true);
            }
        }
    }
}
