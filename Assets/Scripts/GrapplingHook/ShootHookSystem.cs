using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the brains of the hook. The file is literally called the ShootHookSystem aka, the system that shoots the hook xP
// we have some values here

public class ShootHookSystem : StateMachine
{
    #region Fields and Properties

    public float hookRange = 10f, hookSpeed = 1f, hookWeight = 5f;
    public Transform cam;
    public Rigidbody hook;

    public bool _returnedHook = false;

    public Transform correctHookPos;
    
    private bool _follow;
    public bool Follow
    {
        get => _follow;
        set { _follow = value; }
    }
    
    private bool _collided;
    public bool Collided
    {
        get => _collided;
        set { _collided = value; }
    }

    public Transform hookHand;
    public LayerMask unhookable;

    public GameObject rope;

    Transform _transform;
    public Transform Transform => _transform;
    
    #endregion

    #region cacheing transform
    private void Awake()
    {
        _transform = transform;
        unhookable = 1 << 8 | 1 << 3;
    }
    #endregion


    // you can tell that it's from the start of the game, because of this "Start" line under me-
    private void Start()
    {
        SetState(new Idle(this));
    }

    void Update()
    {
        CheckButtonPress();
    }

    public void SetHook(bool value)
    {
        _returnedHook = value;
    }

    public bool GetHook()
    {
        return _returnedHook;
    }

    public void CheckButtonPress()
    {
        if (!Input.GetButtonDown("Fire2")) return;
        SetState(new Aim(this));
    }

    // Checks if the shot landed
    public bool CheckShotHit(out RaycastHit outputHit)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return (Physics.Raycast(ray, out outputHit, hookRange, unhookable));
    }

    public void OnShoot()
    {
        StartCoroutine(State.Shooting());
    }

    public void OnGrab()
    {

        StartCoroutine(State.Grappled());
    }

    public void OnReturn()
    {

        StartCoroutine(State.Returning());
    }
}
