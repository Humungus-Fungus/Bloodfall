using System;
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
        ShootHookSystem.unshootable = true;
        ShootHookSystem.hookEnteredPickupRadius = BackToIdle;

        float t = 0; 
        float hookDuration = ShootHookSystem.hookRange / ShootHookSystem.hookSpeed;
        float startTime = Time.time;

        ShootHookSystem.hook.drag = 0;

        while (Vector3.Distance(hook.position, ShootHookSystem.Transform.position) > 2)
        {
            // Debug.Log(Vector3.Distance(hook.position, ShootHookSystem.Transform.position));
            hook.LookAt(ShootHookSystem.correctHookPos);
            if (ShootHookSystem.Collided)
            {
                GrappleTarget();
                yield break;
            }
            UpdatePosition(startTime, hookDuration, ref t);
            yield return new WaitForSeconds(Time.deltaTime);
        }
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
        // ShootHookSystem.ReturnedHook = false;
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
    }
}
