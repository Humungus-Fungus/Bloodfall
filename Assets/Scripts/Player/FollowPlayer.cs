using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float radius;
    public Vector3 offset;

    public ShootHookSystem shootHookSystem;
    public bool fixedd = false;

    Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    void LateUpdate()
    {
        if (!shootHookSystem.Follow && !fixedd) return;
        _transform.position = player.position + (player.right * radius) + offset;
        _transform.rotation = player.rotation;
    }
}
