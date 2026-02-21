using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float thrustSpeed = 1f;
    private bool thrusting;
    public bool IsThrusting => thrusting;
    public float rotationSpeed = 0.1f;
    private float turnDirection;
    public Rigidbody2D rb;
    public float MaxSpeed = 10;
    public float MaxSpeedRot = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

        //Debug.Log(this.transform.rotation.z);
        thrusting = Input.GetKey(KeyCode.W); //|| Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A))// || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D))// || Input.GetKey(KeyCode.RightArrow))
        {
            
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Paddle();
        }

        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = (Vector2)Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
        }
        //if(rb.angularVelocity > MaxSpeedRot)
        //{
        //    rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, 0, MaxSpeed);
        //}
        
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0f)
        {
            //Debug.Log(rb.angularVelocity);
            //Debug.Log(rb.inertia);
            //Debug.Log(rb.totalTorque);

            //if more than max speed
            if (rb.angularVelocity < MaxSpeedRot && rb.angularVelocity > (MaxSpeedRot*-1))
            {
                rb.AddTorque(rotationSpeed * turnDirection);
            }
        }

    }

    void Paddle()
    {
        rb.AddForce(transform.up * thrustSpeed * 50);
    }

}
