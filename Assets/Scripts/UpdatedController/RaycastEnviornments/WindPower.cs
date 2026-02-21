using UnityEngine;

public class WindPower : MonoBehaviour
{
    [SerializeField] private EnvironmentRay ray;
    [SerializeField] private float windStrength = 5f;

    public Vector2? WindPowerValue { get; private set; }

    void Update()
    {
        WindPowerValue = CalculateWind();
        if (WindPowerValue != null)
        {
            Debug.Log($"wind strength {WindPowerValue}");
        }
    }

    private Vector2? CalculateWind()
    {
        Debug.Log($"Hit the sail? {ray.Hits.Count != 0}");
        if (ray.Hits.Count == 0) return null;

        Vector2 origin = transform.position;
        Vector2 totalForce = Vector2.zero;

        foreach (RaycastHit2D hit in ray.Hits)
        {
            Vector2 toHit = hit.point - origin;
            Vector2 normalizedDistance = toHit.normalized;
            totalForce += normalizedDistance * windStrength;
        }

        return totalForce / ray.Hits.Count;
    }
}