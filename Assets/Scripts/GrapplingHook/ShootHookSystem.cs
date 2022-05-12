using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the brains of the hook. The file is literally called the ShootHookSystem aka, the system that shoots the hook xP
// we have some values here

public class ShootHookSystem : StateMachine
{
    public delegate void hookEnteredPickupRadiusCallback();
    public hookEnteredPickupRadiusCallback hookEnteredPickupRadius;

    public delegate void CollidedCallback();
    public CollidedCallback Collided;

    #region Fields and Properties

    public SwingerActions _swingerActions;
    public PlayerMovement _pm;

    public float hookRange = 10f, hookSpeed = 1f, hookWeight = 5f, hookShootTime = 0.75f;
    public Transform cam;
    public Rigidbody hook;

    // public bool reachedPlayer = false;

    [System.NonSerialized]
    public bool reset;

    [System.NonSerialized]
    public Vector3 lastGrapplePoint = Vector3.zero;

    [System.NonSerialized]
    public Vector3 grapplePoint;

    bool _unshootable = false;
    public bool Unshootable
    {
        get => _unshootable;
        set { _unshootable = value; }
    }

    public Transform correctHookPos;
    
    private bool _follow;
    public bool Follow
    {
        get => _follow;
        set { _follow = value; }
    }

    // public Transform hookHand;
    public LayerMask unhookable;

    // public GameObject rope;

    // public float lastGrappleTime = 0f;

    Transform _transform;

    public Transform player;
    
    #endregion

    #region cacheing transform
    private void Awake()
    {
        _transform = transform;
        _swingerActions.ShootHook += AimTarget;
        unhookable = 1 << 8 | 1 << 3;
    }
    #endregion

    private void Start()
    {
        SetState(new Idle(this));
    }

    public void AimTarget()
    {
        if (_unshootable) return;
        SetState(new Aim(this));
    }

    // Checks if the shot landed
    public bool CheckShotHit(out RaycastHit outputHit)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return (Physics.Raycast(ray, out outputHit, hookRange, unhookable));
    }

    // When grappled, if the player's distance from the hook is locked to hookRange
    // Impose limits on the player's movement, but only in directions away from the hook
    void ClampPlayerToHookRange()
    {
        float distance = Vector3.Distance(_transform.position, hook.transform.position);
        if (distance < hookRange) return;
        // prevent player from moving away from hook, but still towards it
    }
}
