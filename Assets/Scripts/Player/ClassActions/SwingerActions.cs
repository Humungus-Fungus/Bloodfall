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
        InputSystem InputAction = InputSystem.Instance;
        InputAction.RegularAttack += SwingerAttack;
        InputAction.Ability += CallShootHook;
        InputAction.AltAbility += GoToHook;
    }

    public void GoToHook()
    {
        // player tugs on the hook, pulling the hook to them/them to the hook

    }

    void SwingerAttack()
    {
        Debug.Log("Huah!");
    }

}
