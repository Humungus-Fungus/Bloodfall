using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    #region Singleton

    public static InputSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public delegate void RegularAttackCallback();
    public RegularAttackCallback RegularAttack;
    void InvokeRegularAttack() => RegularAttack.Invoke();

    public delegate void AbilityCallback();
    public AbilityCallback Ability;
    void InvokeAbility() => Ability.Invoke();

    public delegate void AltAbilityCallback();
    public AltAbilityCallback AltAbility;
    void InvokeAltAbility() => AltAbility.Invoke();

    Dictionary<string, string> controlsMap = new Dictionary<string, string>()
    {
        { "Regular Attack", "Fire1" },
        { "Ability0", "Fire2" },
        { "Ability1", "Mouse ScrollWheel" }
    };

    Dictionary<string, Delegate> funcMap = new Dictionary<string, Delegate>();

    private void Start()
    {
        funcMap["Regular Attack"] = new Action(InvokeRegularAttack);
        funcMap["Ability0"] = new Action(InvokeAbility);
        funcMap["Ability1"] = new Action(InvokeAltAbility);
    }

    void Update()
    {
        CheckActionUsed();
    }

    void CheckActionUsed()
    {
        if (!Input.anyKey) return;
        foreach (KeyValuePair<string, string> pair in controlsMap)
        {
            if (!Input.GetButtonDown(controlsMap[pair.Key])) continue;
            funcMap[pair.Key].DynamicInvoke();
            // CheckActionUsed(pair.Key);
        }
    }
}
