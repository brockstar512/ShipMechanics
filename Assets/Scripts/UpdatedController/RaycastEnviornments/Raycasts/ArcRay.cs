using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRay : EnvironmentRay
{
    [SerializeField] private float arcAngle = 90f;   // total angle of the slice
    [SerializeField] private int rayCount = 10;       // number of rays in the arc

    protected override void CastRays()
    {
        float halfAngle = arcAngle / 2f;
        float step = rayCount > 1 ? arcAngle / (rayCount - 1) : 0f;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = -halfAngle + step * i;
            Vector2 direction = Rotate(transform.up, angle);
            Cast(transform.position, direction);
        }
    }

    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}
