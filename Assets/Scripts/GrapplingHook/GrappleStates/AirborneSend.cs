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
        
        yield return new WaitForSeconds(ShootHookSystem.hookShootTime);
        if (_grappled) yield break;
        ReturnToPlayer();
    }

    void GrappleTarget()
    {
        _grappled = true;
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
    }

    void ReturnToPlayer()
    {
        ShootHookSystem.SetState(new Return(ShootHookSystem));
    }
}
