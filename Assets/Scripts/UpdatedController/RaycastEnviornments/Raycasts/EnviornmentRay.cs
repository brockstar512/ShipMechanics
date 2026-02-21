using UnityEngine;
using System.Collections.Generic;

public abstract class EnvironmentRay : MonoBehaviour
{
    [SerializeField] protected float rayDistance = 5f;
    [SerializeField] protected LayerMask hitMask;
    [SerializeField] protected bool drawGizmos = true;

    protected List<RaycastHit2D> hits = new List<RaycastHit2D>();

    public IReadOnlyList<RaycastHit2D> Hits => hits;

    void Update()
    {
        hits.Clear();
        CastRays();
    }

    protected abstract void CastRays();

    protected void Cast(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, hitMask);
        if (hit.collider != null)
            hits.Add(hit);

        if (drawGizmos)
            Debug.DrawRay(origin, direction * rayDistance, hit.collider != null ? Color.red : Color.green);
    }
}