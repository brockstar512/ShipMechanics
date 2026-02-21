using UnityEngine;

public class WindPower : MonoBehaviour
{ 
    [SerializeField] private EnvironmentRay ray;
    [SerializeField] private float windStrength = 5f;

    public Vector2? WindPowerValue { get; private set; }

    void Update()
    {
        WindPowerValue = CalculateWind();
    }

    private Vector2? CalculateWind()
    {
        if (ray.Hits.Count == 0) return null;

        Vector2 origin = transform.position;
        Vector2 totalForce = Vector2.zero;
        int validHits = 0;

        foreach (RaycastHit2D hit in ray.Hits)
        {
            SailPower sail = hit.collider.GetComponent<SailPower>();
            if (sail == null) continue;

            float distance = Vector2.Distance(origin, hit.point);

            // Close to origin = close to 1, far away = close to 0
            float inversedNormalizedDistance = 1f - Mathf.Clamp01(distance / ray.RayDistance);

            // Angle the wind hit the sail
            Vector2 toHit = (hit.point - origin).normalized;
            float angle = Vector2.SignedAngle(Vector2.up, toHit);

            sail.ReceiveWind(windStrength * inversedNormalizedDistance, angle);

            totalForce += toHit * windStrength * inversedNormalizedDistance;
            validHits++;
        }

        return validHits > 0 ? totalForce / validHits : null;
    }
}