using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Welcome to the Shoot state!
// This is where the hook gets shot out

public class Shoot : State
{
    
    public Shoot(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Shooting()
    {
        RaycastHit hit;
        Vector3 targetPoint;

        ShootHookSystem.rope.SetActive(true);

        // Check if the shot hit or missed
        if (!ShootHookSystem.CheckShotHit(out hit))
        {
            // Still show the hook being fired, but then bring it back
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            targetPoint = ray.GetPoint(ShootHookSystem.hookRange);
        }
        else
            targetPoint = hit.point;

        // Make the hook move towards the point
        Vector3 direction = targetPoint - ShootHookSystem.hook.position;

        ShootHookSystem.Follow = false;
        ShootHookSystem.hook.AddForce(direction.normalized * ShootHookSystem.hookSpeed, ForceMode.Impulse);

        ShootHookSystem.SetState(new AirborneSend(ShootHookSystem));
        yield break;
    }
}
