using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneReceive : State
{
    public AirborneReceive (ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    Transform hook;

    public override IEnumerator Start()
    {
        // goes 15 away
        hook = ShootHookSystem.hook.transform;
        float t = 0; 
        float hookDuration = ShootHookSystem.hookRange / ShootHookSystem.hookSpeed;
        float startTime = Time.time;

        ShootHookSystem.hook.drag = 0;

        while (!ShootHookSystem.GetHook())
        {
            hook.LookAt(ShootHookSystem.correctHookPos);
            if (ShootHookSystem.Collided)
            {
                GrappleTarget();
                yield break;
            }
            UpdatePosition(startTime, hookDuration, ref t);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        BackToIdle();
    }

    void UpdatePosition(float startTime, float hookDuration, ref float t)
    {
        hook.position = Vector3.Lerp(hook.position, ShootHookSystem.correctHookPos.position, t);
        t = (Time.time - startTime) / hookDuration;
    }

    void GrappleTarget()
    {
        ShootHookSystem.Collided = false;
        float timeDiff = Time.deltaTime - ShootHookSystem.lastGrappleTime;
        if (timeDiff < 0.1f) return;
        ShootHookSystem.lastGrappleTime = Time.time;
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
    }

    void BackToIdle()
    {
        ShootHookSystem.SetHook(false);
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
    }
}
