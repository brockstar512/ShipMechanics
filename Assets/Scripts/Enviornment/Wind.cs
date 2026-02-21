using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Transform other;

    void Update()
    {
        if (other)
        {

            Vector2 forward = transform.TransformDirection(Vector2.up);
            Vector3 toOther = Vector3.Normalize(other.position - transform.position);

           // Debug.Log(Vector3.Dot(forward, toOther));
            if (Vector3.Dot(forward, toOther) < 0)
            {
                //print("The other transform is behind me!");
                //Debug.Log(Vector3.Dot(forward, toOther));
                //todo draw raycast
            }
        }
    }
}

//distance
//field of view
//point Plus contact ple direction of sail

//later... percentage of sail shown