using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Welcome to AirborneSend!
// This state makes the hook check if it collided with anything.

public class AirborneSend : State
{
    public AirborneSend (ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    bool _grappled = false;

    public override IEnumerator Start()
    {
        ShootHookSystem.Collided = GrappleTarget;
        _grappled = false;
        Transform hook = ShootHookSystem.hook.transform, player = ShootHookSystem.player;
        float range = ShootHookSystem.hookRange;

        while (Vector3.Distance(hook.position, player.position) < range)
        {
            if (ShootHookSystem.reset) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (_grappled || ShootHookSystem.reset) yield break;

        ReturnToPlayer();
    }

    void GrappleTarget()
    {
        _grappled = true;
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
    }

    void ReturnToPlayer()
    {
        if (ShootHookSystem.Unshootable) return;
        ShootHookSystem.SetState(new Return(ShootHookSystem));
    }
}
