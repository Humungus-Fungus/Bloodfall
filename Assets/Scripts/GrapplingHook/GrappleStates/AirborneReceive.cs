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
                ShootHookSystem.Collided = false;
                ShootHookSystem.SetState(new Grapple(ShootHookSystem));
                yield break;
            }
            hook.position = Vector3.Lerp(hook.position, ShootHookSystem.correctHookPos.position, t);
            t = (Time.time - startTime) / hookDuration;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ShootHookSystem.SetHook(false);
        
        // Go back to idle
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
    }
}
