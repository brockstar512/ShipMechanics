using System;
using UnityEngine;

public class SailPower : MonoBehaviour
{
    public SailMovement SailMovement;

    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private bool drawGizmos = true;

    private float windPower = 0f;
    private float windAngle = 0f;

    public Vector2? WindForce
    {
        get
        {
            if (SailMovement.openAmount == 0f || windPower == 0f) return null;
            return CalculateWindForce();
        }
    }

    private void Awake()
    {
        SailMovement = GetComponent<SailMovement>();
    }

    void Update()
    {
        CastBackRay();
    }

    public void ReceiveWind(float power, float angle)
    {
        windPower = power;
        windAngle = angle;
    }

    void CastBackRay()
    {
        Vector2 origin = transform.position;
        Vector2 direction = -transform.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, hitMask);

        if (drawGizmos)
            Debug.DrawRay(origin, direction * rayDistance, hit.collider != null ? Color.red : Color.green);
    }

    Vector2 CalculateWindForce()
    {
        Vector2 sailFacing = transform.up;
        Vector2 windDirection = new Vector2(Mathf.Sin(windAngle * Mathf.Deg2Rad), Mathf.Cos(windAngle * Mathf.Deg2Rad));

        float cross = Vector3.Cross(sailFacing, windDirection).z;
        float dot = Vector2.Dot(sailFacing, windDirection);

        float efficiency = Mathf.Abs(cross) * SailMovement.openAmount;
        return sailFacing * efficiency * Mathf.Sign(dot) * windPower;
    }
}