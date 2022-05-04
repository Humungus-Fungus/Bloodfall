using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Welcome to the Shoot state!
// This is where the hook gets shot out

public class Shoot : State
{
    
    public Shoot(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        Vector3 targetPoint;

        ShootHookSystem.rope.SetActive(true);

        targetPoint = GetTargetPoint();

        // Make the hook move towards the point
        Vector3 direction = targetPoint - ShootHookSystem.hook.position;

        ShootHookSystem.Follow = false;
        // to prevent the hook getting stuck, we'll make it jump forwards initially
        ShootHookSystem.hook.transform.position += direction.normalized * 1f;
        ShootHookSystem.hook.AddForce(direction.normalized * ShootHookSystem.hookSpeed, ForceMode.Impulse);

        ShootHookSystem.SetState(new AirborneSend(ShootHookSystem));
        yield break;
    }

    Vector3 GetTargetPoint()
    {
        RaycastHit hit;

        // Check if the shot hit or missed
        if (ShootHookSystem.CheckShotHit(out hit)) return hit.point;
        // Still show the hook being fired, but then bring it back
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return ray.GetPoint(ShootHookSystem.hookRange);
    }

    public override IEnumerator Shooting()
    {
        yield break;
    }
}
