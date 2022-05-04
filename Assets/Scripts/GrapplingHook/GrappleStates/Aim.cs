using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the aim state!
// Where the hook is still deciding what to do
// because remember, right clicc can do one of two things- shoot out the hook or pull it back to you if it's already been
// shot out. This is the state where that's being decided

public class Aim : State
{
    public Aim(ShootHookSystem shootHookSystem) : base(shootHookSystem) {}

    public override IEnumerator Start()
    {
        if (ShootHookSystem.Follow) // Shoot the hook
            shootHook();
        else
            returnHook();
        
        yield break;
    }

    void shootHook()
    {
        ShootHookSystem.SetState(new Shoot(ShootHookSystem));
        ShootHookSystem.OnShoot();
    }

    void returnHook()
    {
            ShootHookSystem.SetState(new Return(ShootHookSystem)); // else bring the hook back
    }
}
