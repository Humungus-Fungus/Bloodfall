using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerActions : MonoBehaviour
{
    private void Awake()
    {
        InputSystem.Instance.Ability += CallShootHook;
    }

    public void ReelHook()
    {
        // player tugs on the hook, pulling the hook to them/them to the hook

    }

    void CallShootHook()
    {
        ShootHook.Invoke();
        // Debug.Log("Shot the hook out");
    }

    public delegate void ShootHookCallback();
    public ShootHookCallback ShootHook;
}
