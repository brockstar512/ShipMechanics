using UnityEngine;

public class SailPower : MonoBehaviour
{
    public SailMovement SailMovement;
    public Vector2? WindForce { get; private set; }

    private void Awake()
    {
        SailMovement = GetComponent<SailMovement>();
    }

    public void ReceiveWind(Vector2 windForce)
    {
        float effectiveOpen = Mathf.Max(SailMovement.openAmount, 0.1f);
        WindForce = windForce * effectiveOpen;
    }
}