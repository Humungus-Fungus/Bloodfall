using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRadius : MonoBehaviour
{
    public ShootHookSystem shs;

    void Awake()
    {
        float radius = shs.hookRange;
        transform.localScale = new Vector3(radius, radius, radius);
    }
}
