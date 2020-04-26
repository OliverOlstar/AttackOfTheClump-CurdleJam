using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private particleOffVelocity runningParticle;
    [SerializeField] private particlePlayer jumpingParticle;

    [SerializeField] private particleOffVelocity wallSlideParticle;
    [SerializeField] private particlePlayer wallJumpingParticle;

    private bool swimming = false;

    public void Jumped()
    {
        if (swimming) return;

        jumpingParticle.TriggerParticle();
    }

    public void WallJumped()
    {
        if (swimming) return;

        wallJumpingParticle.TriggerParticle();
    }

    public void OnGround()
    {
        if (swimming) return;

        jumpingParticle.TriggerParticle();
        runningParticle.TriggerParticle(true);
    }

    public void OffGround()
    {
        if (swimming) return;

        runningParticle.TriggerParticle(false);
    }

    public void OnLeftWall()
    {
        if (swimming) return;

        wallJumpingParticle.TriggerParticle();
        wallSlideParticle.TriggerParticle(true);
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, -90);
    }

    public void OnRightWall()
    {
        if (swimming) return;

        wallJumpingParticle.TriggerParticle();
        wallSlideParticle.TriggerParticle(true);
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 90);
    }

    public void OffWall()
    {
        if (swimming) return;

        wallSlideParticle.TriggerParticle(false);
    }

    public void OnSwimming()
    {
        swimming = !swimming;

        if (swimming)
        {
            wallSlideParticle.TriggerParticle(false);
            runningParticle.TriggerParticle(false);
        }
    }
}
