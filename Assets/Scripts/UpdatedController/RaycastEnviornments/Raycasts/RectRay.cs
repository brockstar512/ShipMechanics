using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectRay : EnvironmentRay
{
    [SerializeField] private float width = 2f;        // total width of the rectangle
    [SerializeField] private int rayCount = 5;        // number of parallel rays

    protected override void CastRays()
    {
        float step = rayCount > 1 ? width / (rayCount - 1) : 0f;
        Vector2 right = transform.right;
        Vector2 forward = transform.up;

        for (int i = 0; i < rayCount; i++)
        {
            float offset = -width / 2f + step * i;
            Vector2 origin = (Vector2)transform.position + right * offset;
            Cast(origin, forward);
        }
    }
}
