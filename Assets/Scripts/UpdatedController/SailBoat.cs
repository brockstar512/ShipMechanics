using UnityEngine;

public class SailBoat : MonoBehaviour
{
    private Sail sail;
    private IGauge speed;
    private float lateralDrag = 2f; // how fast momentum bleeds when misaligned
    private float momentumDecay = 0.5f;
    private Vector2 momentum = Vector2.up * 0.5f;

    void Start()
    {
        speed = GetComponentInChildren<IGauge>();
        sail = GetComponentInChildren<Sail>();
    }

    void Update()
    {
        ApplyPaddlePower();
        ApplyWindPower();
        ApplyDrag();
        ApplyMovement();

        Debug.Log($"Momentum: {momentum.magnitude:F3} | Alignment: {CalculateAlignment():F3}");
    }

    private void ApplyWindPower()
    {
        momentum += sail.WindForce * Time.deltaTime;
    }

    void ApplyPaddlePower()
    {
        Vector2 targetMomentum = (Vector2)transform.up * speed.currentValue;
        momentum = Vector2.Lerp(momentum, targetMomentum, Time.deltaTime);
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

