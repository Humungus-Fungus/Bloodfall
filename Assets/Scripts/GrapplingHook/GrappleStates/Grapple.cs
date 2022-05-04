using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Welcome to Grapple state! The place where the hook is supposed to stick to the stuff it hits!

public class Grapple : State
{
    public Grapple(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        Vector3 grapplePosition = ShootHookSystem.hook.transform.position;

        ShootHookSystem.hook.velocity = Vector3.zero;
        ShootHookSystem.hook.angularVelocity = Vector3.zero;
        ShootHookSystem.hook.drag = 100;

        yield return new WaitForSeconds(Time.deltaTime); // wait for a single frame
        ShootHookSystem.hook.transform.position = grapplePosition;

        yield break;
    }

    public override IEnumerator Grappled()
    {
        // Whatever happens when the hook is stucc
        yield break;
    }
}
