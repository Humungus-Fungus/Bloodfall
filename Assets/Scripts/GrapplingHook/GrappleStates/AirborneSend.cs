using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Welcome to AirborneSend!
// This state makes the hook check if it collided with anything.

public class AirborneSend : State
{
    public AirborneSend (ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        Transform hook = ShootHookSystem.hook.transform;
        Transform startPoint = ShootHookSystem.correctHookPos;
        float range = ShootHookSystem.hookRange;

        for (float distance = 0f; distance < range; distance = Vector3.Distance(hook.position, startPoint.position))
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!ShootHookSystem.Collided) continue;
            GrappleTarget();
            yield break;
        }

        ShootHookSystem.SetState(new Return(ShootHookSystem));
        yield break;
    }

    void GrappleTarget()
    {
        ShootHookSystem.Collided = false; // set it back to false
        ShootHookSystem.SetState(new Grapple(ShootHookSystem));
    }
}
