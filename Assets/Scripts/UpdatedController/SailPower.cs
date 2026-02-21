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
        if (SailMovement.openAmount == 0f)
        {
            WindForce = null;
            return;
        }

        WindForce = windForce * SailMovement.openAmount;
    }
}