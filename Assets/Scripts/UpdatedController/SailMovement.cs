using UnityEngine;

public class SailMovement : MonoBehaviour
{
    [Range(0f, 1f)]
    public float openAmount = 1f;
    
    [SerializeField] private Transform sailMesh;
    [SerializeField] private float openSpeed = 1f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float maxRotationAngle = 60f;

    private float currentRotation = 0f;

    void Update()
    {
        HandleOpenClose();
        HandleRotation();
        ApplySailScale();
    }

    void HandleOpenClose()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            openAmount = Mathf.Clamp01(openAmount + openSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            openAmount = Mathf.Clamp01(openAmount - openSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
            currentRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            currentRotation -= rotationSpeed * Time.deltaTime;

        currentRotation = Mathf.Clamp(currentRotation, -maxRotationAngle, maxRotationAngle);

        transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
    }

    void ApplySailScale()
    {
        Vector3 scale = sailMesh.localScale;
        scale.y = openAmount;
        sailMesh.localScale = scale;
    }
}
