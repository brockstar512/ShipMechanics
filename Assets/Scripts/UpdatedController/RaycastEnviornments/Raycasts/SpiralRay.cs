using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRay : EnvironmentRay
{
    [SerializeField] private int rayCount = 20;
    [SerializeField] private float arcAngle = 360f;     // slice or full circle
    [SerializeField] private float circleRadius = 5f;   // how far out the swirl reaches
    [SerializeField] private float swirlTightness = 2f; // how much the origins spiral inward
    [SerializeField] private int arms = 1;              // number of swirl arms

    protected override void CastRays()
    {
        float angleStep = arcAngle / rayCount;

        for (int i = 0; i < rayCount; i++)
        {
            float t = (float)i / rayCount;
            float angle = t * arcAngle;

            // Origin spirals outward from center as angle increases
            float originRadius = Mathf.Lerp(0f, circleRadius, t);
            Vector2 originOffset = Rotate(transform.up, angle * arms) * originRadius;
            Vector2 origin = (Vector2)transform.position + originOffset;

            // Each ray points outward toward the circle edge from its spiraled origin
            Vector2 outward = Rotate(transform.up, angle);
            Vector2 target = (Vector2)transform.position + outward * circleRadius;
            Vector2 direction = (target - origin).normalized;
            float distance = Vector2.Distance(origin, target);

            CastSwirl(origin, direction, distance);
        }
    }

    private void CastSwirl(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, hitMask);
        if (hit.collider != null)
            hits.Add(hit);

        if (drawGizmos)
            Debug.DrawRay(origin, direction * distance, hit.collider != null ? Color.red : Color.green);
    }

    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}