using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public ShootHookSystem shootHookSystem;
    
    // The line of code right under this one activates whenever the hook collides or runs into something else
    public void OnCollisionEnter(Collision other)
    {
        if (shootHookSystem.Follow) return;

        int collisionLayerMask = 1 << other.gameObject.layer;
        if ( (shootHookSystem.unhookable.value & collisionLayerMask) != 0)
        {
            shootHookSystem.SetState(new Return(shootHookSystem));
            return;
        }
        shootHookSystem.Collided = true;
        Debug.Log(other.transform.name);
    }
}


// UnhookableMask - CollidedLayer = RestOfMask
// .: UnhookableMask - RestOfMask = CollidedLayer
// UnhookableMask = PlayerLayer + UnhookableLayer
// .: PlayerLayer + UnhookableLayer - CollidedLayer = RestOfMask

// This file is called "Collision Detector". It detects any collisions that the hook has with anything. There must be a
// Mistake here. let's do a simple test