using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrapplingRope : MonoBehaviour
{
    Spring _spring;
    Vector3 randomAngle;
    public LineRenderer _lineRenderer;
    public Vector3 currentGrapplePosition;
    public ShootHookSystem hookSystem;
    public int graduations;
    public float damper, strength, velocity, waveCount, waveHeight;

    public AnimationCurve affectCurve;

    float prevDist = 0f;
    int cancelOffset = 1;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _spring = new Spring();
        _spring.SetTarget(0);

        ResetRope();

        // hookSystem.hookEnteredPickupRadius += ResetRope;
    }

    void ResetRope()
    {
        currentGrapplePosition = hookSystem.correctHookPos.position;
        _spring.Reset();
        if (_lineRenderer.positionCount > 0) _lineRenderer.positionCount = 0;
        cancelOffset = 1;
        prevDist = 0;
        randomAngle = Vector3.Slerp(Vector3.up, Vector3.down, Random.value);
    }


    // AABB bug is in here
    void DrawRope()
    {
        if (hookSystem.Follow)
        {
            ResetRope();
            return;
        }


        if (_lineRenderer.positionCount == 0)
        {
            _spring.SetVelocity(velocity);
            _lineRenderer.positionCount = graduations + 1;
        }

        if (Vector3.Distance(hookSystem.hook.transform.position, hookSystem.player.position) < prevDist)
        {
            cancelOffset = 0;
        }

        _spring.SetDamper(damper);
        _spring.SetStrength(strength);
        _spring.Update(Time.deltaTime);

        var grapplePoint = hookSystem.grapplePoint;
        var hookSource = hookSystem.correctHookPos.position;

        var viewVector = (grapplePoint - hookSource).normalized;
        var up = Quaternion.LookRotation(viewVector) * randomAngle;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        for (int i = 0; i < graduations + 1; i++)
        {
            var delta = i / (float)graduations;

            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * _spring.Value *
                affectCurve.Evaluate(delta);

            _lineRenderer.SetPosition(i, Vector3.Lerp(hookSource, hookSystem.hook.transform.position, delta) + offset * cancelOffset);
        }

        prevDist = Vector3.Distance(hookSystem.hook.transform.position, hookSystem.player.position);
    }

    void LateUpdate()
    {
        DrawRope();
    }
}
