using UnityEngine;

public class Rudder : MonoBehaviour
{
    
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private float rotationDrag = 5f;

    private float currentRotationVelocity = 0f;

    void Update()
    {
        float targetVelocity = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))  targetVelocity =  rotationSpeed;
        if (Input.GetKey(KeyCode.RightArrow)) targetVelocity = -rotationSpeed;

        currentRotationVelocity = Mathf.Lerp(currentRotationVelocity, targetVelocity, rotationDrag * Time.deltaTime);
        transform.parent.Rotate(0f, 0f, currentRotationVelocity * Time.deltaTime);
    }
}

