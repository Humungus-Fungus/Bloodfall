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

        for (float distance = 2f; distance > 1;
         distance = Vector3.Distance(hook.position, ShootHookSystem.correctHookPos.position))
        {
            hook.LookAt(ShootHookSystem.correctHookPos);
            // hook.position = Vector3.MoveTowards(hook.position, ShootHookSystem.correctHookPos.position, 70/distance * Time.deltaTime);
            hook.position = Vector3.Lerp(hook.position, ShootHookSystem.correctHookPos.position, t);
            t = (Time.time - startTime) / hookDuration;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        // Go back to idle
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
    }
}
