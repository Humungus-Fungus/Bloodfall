using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS is idle! it describes exactly what I mean when I say "the hook is in the idle state"

public class Idle : State
{
    public Idle(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    Rigidbody hook;

    public override IEnumerator Start()
    {
        hook = ShootHookSystem.hook;
        ShootHookSystem.unshootable = false;

        ShootHookSystem.rope.SetActive(false);

        EndMotionAndSpin();
        
        ShootHookSystem.Follow = true;
        yield break;
    }

    void EndMotionAndSpin()
    {
        hook.velocity = Vector3.zero;
        hook.transform.localPosition = Vector3.zero;

        hook.angularVelocity = Vector3.zero;
        hook.rotation = Quaternion.identity;
        hook.transform.LookAt(ShootHookSystem.Transform.forward * 100);
    }

    public override IEnumerator Ready()
    {
        // Ready to be fired
        yield break;
    }
}
