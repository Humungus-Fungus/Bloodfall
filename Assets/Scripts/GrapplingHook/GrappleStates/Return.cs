using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : State
{
    public Return(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        Rigidbody hook = ShootHookSystem.hook;

        // Vector3 breakForce = -hook.velocity * 2f;
        // hook.AddForce(breakForce * Time.deltaTime, ForceMode.Impulse);
        ShootHookSystem.hook.velocity = Vector3.zero;
        ShootHookSystem.hook.angularVelocity = Vector3.zero;

        Vector3 direction = ShootHookSystem.correctHookPos.position - hook.transform.position;
        hook.AddForce(direction.normalized * ShootHookSystem.hookSpeed, ForceMode.Impulse);
        ShootHookSystem.SetState(new AirborneReceive(ShootHookSystem));

        yield break;
    }

    public override IEnumerator Returning()
    {
        // Return the hook to the player
        yield break;
    }
}
