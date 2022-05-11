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
        ShootHookSystem.reachedPlayer = false;
        Debug.Log("reachedPlayer made false");
        hook = ShootHookSystem.hook.transform;
        ShootHookSystem.hookEnteredPickupRadius = BackToIdle;
        ShootHookSystem.Collided = GrappleTarget;

        float t = 0; 
        float hookDuration = ShootHookSystem.hookRange / ShootHookSystem.hookSpeed;
        float startTime = Time.time;
        float startDist = Vector3.Distance(hook.position, ShootHookSystem.player.position);

        while (!ShootHookSystem.reachedPlayer)
        {
            UpdatePosition(startTime, hookDuration, ref t);
            yield return new WaitForSeconds(Time.deltaTime);
            // Debug.Log("I'm bugging out");
        }
        
        yield break;
    }

    void UpdatePosition(float startTime, float hookDuration, ref float t)
    {
        hook.LookAt(ShootHookSystem.correctHookPos);
        hook.position = Vector3.Lerp(hook.position, ShootHookSystem.correctHookPos.position, t);
        t = (Time.time - startTime) / hookDuration;
    }

    void GrappleTarget()
    {
        ShootHookSystem.reachedPlayer = true;
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
        Debug.Log("reachedPlayer made true");
    }

    void BackToIdle()
    {
        ShootHookSystem.reachedPlayer = true;
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
        Debug.Log("reachedPlayer made true");
    }
}
