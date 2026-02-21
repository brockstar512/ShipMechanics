using UnityEngine;

public class WindPower : MonoBehaviour
{
    [SerializeField] private EnvironmentRay ray;
    [SerializeField] private float windStrength = 5f;
    private Transform lastHitTransform;
    private bool hasHit = false;

    private void Update()
    {
        hasHit = false;
        ApplyWindToSails();
        DrawDebug();
    }

    void ApplyWindToSails()
    {
        foreach (RaycastHit2D hit in ray.Hits)
        {
            SailPower sail = hit.collider.GetComponent<SailPower>();
            if (sail == null) continue;

            Vector2 force = CalculateWindForce(sail, hit);
            sail.ReceiveWind(force);

            lastHitTransform = hit.collider.transform;
            hasHit = true;
        }
    }

    Vector2 CalculateWindForce(SailPower sail, RaycastHit2D hit)
    {
        Vector2 windDirection = (hit.point - (Vector2)transform.position).normalized;
        Vector2 sailFacing = sail.transform.up;

        float windCatch = Mathf.Abs(Vector2.Dot(sailFacing, windDirection));

        // 1 when sail is close to origin, 0 when at max ray distance
        float distance = Vector2.Distance(transform.position, hit.point);
        float windFalloff = 1f - Mathf.Clamp01(distance / ray.RayDistance);

        return (Vector2)sail.transform.up * windCatch * windFalloff * windStrength;
    }

    void DrawDebug()
    {
        if (!hasHit || lastHitTransform == null) return;

        Vector2 sailCenter = lastHitTransform.position;
        Vector2 windDirection = ((Vector2)lastHitTransform.position - (Vector2)transform.position).normalized;
        Vector2 sailFacing = lastHitTransform.up;

        float windCatch = Mathf.Abs(Vector2.Dot(sailFacing, windDirection));
        float distance = Vector2.Distance(transform.position, lastHitTransform.position);
        float windFalloff = 1f - Mathf.Clamp01(distance / ray.RayDistance);
        Vector2 pushDirection = sailFacing * windCatch * windFalloff;

        // Push force arrow — cyan, scaled by windCatch so it shrinks when parallel
        Vector2 tip = sailCenter + pushDirection;
        Debug.DrawLine(sailCenter, tip, Color.cyan);
        Debug.DrawLine(tip, tip + (Vector2)(Quaternion.Euler(0, 0, 135) * (Vector3)pushDirection * 0.2f), Color.cyan);
        Debug.DrawLine(tip, tip + (Vector2)(Quaternion.Euler(0, 0, -135) * (Vector3)pushDirection * 0.2f), Color.cyan);

        // Sail facing arrow — yellow
        Vector2 upTip = sailCenter + sailFacing;
        Debug.DrawLine(sailCenter, upTip, Color.yellow);
        Debug.DrawLine(upTip, upTip + (Vector2)(Quaternion.Euler(0, 0, 135) * (Vector3)sailFacing * 0.2f), Color.yellow);
        Debug.DrawLine(upTip, upTip + (Vector2)(Quaternion.Euler(0, 0, -135) * (Vector3)sailFacing * 0.2f), Color.yellow);
    }
}