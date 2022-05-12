using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneReceive : State
{
    public AirborneReceive (ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    Transform hook;
    bool reachedPlayer;
    float countingSpeed = 0.5f;

    public override IEnumerator Start()
    {
        // goes 15 away
        reachedPlayer = false;
        // Debug.Log("reachedPlayer made false");
        hook = ShootHookSystem.hook.transform;
        ShootHookSystem.hookEnteredPickupRadius = BackToIdle;
        ShootHookSystem.Collided = GrappleTarget;

        float t = 0; 
        float hookDuration = ShootHookSystem.hookRange / ShootHookSystem.hookSpeed;
        float startTime = Time.time;
        float startDist = Vector3.Distance(hook.position, ShootHookSystem.player.position);

        // while (!ShootHookSystem.reachedPlayer)
        while (!reachedPlayer && !ShootHookSystem.reset)
        {
            reachedPlayer = UpdatePosition(startTime, hookDuration, ref t);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield break;
    }

    bool UpdatePosition(float startTime, float hookDuration, ref float t)
    {
        hook.LookAt(ShootHookSystem.correctHookPos);
        hook.position = Vector3.Lerp(hook.position, ShootHookSystem.correctHookPos.position, t);

        t = Mathf.Clamp(t + Time.deltaTime * countingSpeed, 0, 1);

        return (t>=0.95);
    }

    void GrappleTarget()
    {
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
        // Debug.Log("reachedPlayer made true");
    }

    void BackToIdle()
    {
        reachedPlayer = true;
        ShootHookSystem.SetState(new Idle(ShootHookSystem));
        // Debug.Log("reachedPlayer made true");
    }
}
