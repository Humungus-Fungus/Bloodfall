using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS is idle! it describes exactly what I mean when I say "the hook is in the idle state"

public class Idle : State
{
    public Idle(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        Rigidbody hook = ShootHookSystem.hook;

        ShootHookSystem.rope.SetActive(false);

        hook.velocity = Vector3.zero;
        hook.transform.localPosition = Vector3.zero;

        hook.angularVelocity = Vector3.zero;
        hook.rotation = Quaternion.identity;
        
        ShootHookSystem.Follow = true;
        yield break;
    }

    public override IEnumerator Ready()
    {
        // Ready to be fired
        yield break;
    }
}
