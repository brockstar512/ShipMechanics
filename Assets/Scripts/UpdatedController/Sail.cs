using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour
{[Range(0f, 1f)]
    public float openAmount = 1f;

    public Vector2 windDirection = Vector2.up;

    [SerializeField] private Transform sailMesh;
    [SerializeField] private float openSpeed = 1f;

    public Vector2 WindForce
    { 
        get
        {
            if (openAmount == 0f) return Vector2.zero;
            return new Vector2(1, 1);
            // return CalculateWindForce();
        }
    }

    void Update()
    {
        HandleInput();
        ApplySailScale();
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            openAmount = Mathf.Clamp01(openAmount + openSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            openAmount = Mathf.Clamp01(openAmount - openSpeed * Time.deltaTime);
    }

    void ApplySailScale()
    {
        Vector3 scale = sailMesh.localScale;
        scale.y = openAmount;
        sailMesh.localScale = scale;
    }

    // Vector2 CalculateWindForce(float windStrength = 5f)
    // {
    //     Vector2 sailFacing = transform.up;
    //     windDirection = windDirection.normalized;
    //
    //     float cross = Vector3.Cross(sailFacing, windDirection).z;
    //     float dot = Vector2.Dot(sailFacing, windDirection);
    //
    //     float efficiency = Mathf.Abs(cross) * openAmount;
    //     return sailFacing * efficiency * Mathf.Sign(dot) * windStrength;
    // }
}