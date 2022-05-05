using System.Collections;
using System.Collections.Generic;
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

    public delegate void AbilityCallback();
    public AbilityCallback Ability;

    Dictionary<string, string> controlsMap = new Dictionary<string, string>()
    {
        { "Regular Attack", "Fire1" },
        { "Ability", "Fire2" }
    };

    void Update()
    {
        CheckRegAttackUse();
        CheckAbilityUse();
    }

    void CheckRegAttackUse()
    {
        if (!Input.GetButtonDown(controlsMap["Regular Attack"])) return;
        RegularAttack.Invoke();
        Debug.Log("Regular attack");
    }

    void CheckAbilityUse()
    {
        if (!Input.GetButtonDown(controlsMap["Ability"])) return;
        Ability.Invoke();
        Debug.Log("Ability used");
    }
}
