using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneReceive : State
{
    public AirborneReceive (ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        // goes 15 away
        Transform hook = ShootHookSystem.hook.transform;

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
            UpdatePosition();
            yield return new WaitForSeconds(Time.deltaTime);
        }
        BackToIdle();
    }

    void UpdatePosition()
    {
        hook.position = Vector3.Lerp(hook.position, ShootHookSystem.correctHookPos.position, t);
        t = (Time.time - startTime) / hookDuration;
    }

    void GrappleTarget()
    {
        ShootHookSystem.Collided = false;
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
    }

    void BackToIdle()
    {
        ShootHookSystem.SetHook(false);
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
    }
}
