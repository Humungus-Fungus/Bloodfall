using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public ShootHookSystem shootHookSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7 || shootHookSystem.Follow) return;
        // Debug.Log("Hook entered");
        shootHookSystem.hookEnteredPickupRadius.Invoke();
    }
}
