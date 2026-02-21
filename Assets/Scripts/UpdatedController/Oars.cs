using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this should just be the speed
public class Oars : MonoBehaviour, IGauge
{
    public float currentValue { get; set; } = 0.25f;
    private const float MAX = 10f;
    private const float MIN = -10f;
    private const float DECAY_RATE = 0.75f;
    private const float POSITIVE_REST = 0.25f;
    private const float NEGATIVE_REST = -0.25f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            currentValue += 1f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentValue -= 1f;
        }

        // Decay toward resting momentum based on which side of 0 we're on
        float restingValue = currentValue >= 0f ? POSITIVE_REST : NEGATIVE_REST;

        if (currentValue > restingValue)
        {
            currentValue -= DECAY_RATE * Time.deltaTime;
            if (currentValue < restingValue) currentValue = restingValue;
        }
        else if (currentValue < restingValue)
        {
            currentValue += DECAY_RATE * Time.deltaTime;
            if (currentValue > restingValue) currentValue = restingValue;
        }

        currentValue = Mathf.Clamp(currentValue, MIN, MAX);

    }
}