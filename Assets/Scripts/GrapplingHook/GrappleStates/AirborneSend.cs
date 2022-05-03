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
        for (float distance = 0f; distance < ShootHookSystem.hookRange;
         distance = Vector3.Distance(ShootHookSystem.hook.transform.position, ShootHookSystem.Transform.position))
        {
            if (!ShootHookSystem.Collided)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                continue;
            }
            // if it collided, then it does this:
            ShootHookSystem.Collided = false; // set it back to false
            ShootHookSystem.SetState(new Grapple(ShootHookSystem));
            yield break;
        }

        ShootHookSystem.SetState(new Return(ShootHookSystem));
        yield break;
    }
}
