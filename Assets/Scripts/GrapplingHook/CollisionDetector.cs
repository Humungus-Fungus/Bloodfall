using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public ShootHookSystem shootHookSystem;
    
    // The line of code right under this one activates whenever the hook collides or runs into something else
    public void OnCollisionEnter(Collision other)
    {
        int collisionLayerMask = 1 << other.gameObject.layer;
        if ( (shootHookSystem.unhookable & collisionLayerMask) != 0) return;
        shootHookSystem.Collided = true; // This line is referencing the same "collided" variable!
        Debug.Log(other.transform.name);
    }
}




// This file is called "Collision Detector". It detects any collisions that the hook has with anything. There must be a
// Mistake here. let's do a simple test