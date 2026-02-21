using UnityEngine;

public class SailBoat : MonoBehaviour
{
    private IGauge paddleMomentum;
    private SailPower sail;
    private float lateralDrag = 2f;
    private float momentumDecay = 0.5f;
    private Vector2 momentum = Vector2.up * 0.5f;

    void Start()
    {
        paddleMomentum = GetComponentInChildren<IGauge>();
        sail = GetComponentInChildren<SailPower>();
    }

    void Update()
    {
        ApplyPaddlePower();
        ApplyWindPower();
        ApplyDrag();
        ApplyMovement();

        // Debug.Log($"Momentum: {momentum.magnitude:F3} | Alignment: {CalculateAlignment():F3}");
    }

    void ApplyPaddlePower()
    {
        // Paddle always pushes along the boat's up axis
        Vector2 paddleTarget = (Vector2)transform.up * paddleMomentum.currentValue;
        momentum = Vector2.Lerp(momentum, paddleTarget, Time.deltaTime);
    }

    void ApplyWindPower()
    {
        if (!sail.WindForce.HasValue) return;

        // Wind pushes in whatever direction the sail calculated — independent of paddle
        Vector2 windTarget = momentum + sail.WindForce.Value;
        momentum = Vector2.Lerp(momentum, windTarget, Time.deltaTime);
    }

    float CalculateAlignment()
    {
        return Vector2.Dot(momentum.normalized, transform.up);
    }

    float CalculateDrag()
    {
        float alignment = CalculateAlignment();
        return Mathf.Lerp(momentumDecay, momentumDecay + lateralDrag, 1f - Mathf.Abs(alignment));
    }

    void ApplyDrag()
    {
        float drag = CalculateDrag();
        momentum *= Mathf.Clamp01(1f - drag * Time.deltaTime);
    }

    void ApplyMovement()
    {
        transform.position += (Vector3)momentum * Time.deltaTime;
    }
}
