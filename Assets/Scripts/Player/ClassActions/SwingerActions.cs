using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerActions : MonoBehaviour
{
    public delegate void ShootHookCallback();
    public ShootHookCallback ShootHook;
    void CallShootHook() => ShootHook.Invoke();
    
    private void Awake()
    {
        InputSystem.Instance.Ability += CallShootHook;
        InputSystem.Instance.AltAbility += GoToHook;
    }

    public void GoToHook()
    {
        // player tugs on the hook, pulling the hook to them/them to the hook

    }

}
