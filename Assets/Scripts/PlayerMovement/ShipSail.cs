using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ShipSail : MonoBehaviour
{


    private float traverse = 0;
    private float currentBearing = 180;
    private float newBearing;
    [SerializeField] private float rotationSpeed = 5.0f;


    // Update is called once per frame
    void Update()
    {


        Rotate();
        OpenCloseSail();
    }

    void SetCurrentBearing(float rot)
    {
        currentBearing = Mathf.Clamp(rot, 155f, 205f);
        this.transform.localRotation = Quaternion.Euler(0, 0, rot);
    }

    public void OpenCloseSail()
    {
        float result = 0;
        const float sailAlphaStep = .20f;
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            result += sailAlphaStep;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            result -= sailAlphaStep;
        }

        //cg.alpha += result;

    }

    public void Rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            traverse = 1 * rotationSpeed * Time.deltaTime;

            this.transform.Rotate(0, 0, traverse);
            newBearing = currentBearing + traverse;
            SetCurrentBearing(newBearing);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            traverse = -1 * rotationSpeed * Time.deltaTime;

            this.transform.Rotate(0, 0, traverse);
            newBearing = currentBearing + traverse;
            SetCurrentBearing(newBearing);


        }
    }

}


/* bubble ghost
 
     float radius = this.GetComponent<BoxCollider2D>().size.y;

     //the direction the bubble should travel...i think the direction is off on the inverse normal one
     Debug.DrawLine(windBlock.position, this.transform.position, Color.magenta);//todo magenta
     //dir to the sail from the block
     Vector3 dir = new Vector3(this.transform.position.x - windBlock.position.x, this.transform.position.y - windBlock.position.y, 0);

     //Debug.Log($"Dir: {dir}");

     //kite
     //center of ghost to center of bubble
     Debug.DrawLine(windBlock.position, this.transform.position, Color.yellow);
     //perpendicular line
     Vector3 mainVectorCopy = new Vector3(windBlock.transform.position.x - this.transform.position.x, windBlock.transform.position.y - this.transform.position.y, 0);
     //top point

     Vector3 pointThree = Vector3.Cross(mainVectorCopy, Vector3.forward);
     pointThree.Normalize();
     var newPoint = (radius / 2) * pointThree + this.transform.position;
     var newPoint2 = (-radius / 2) * pointThree + this.transform.position;

     Debug.DrawLine(newPoint, newPoint2, Color.blue);//todo blue

     //hypotonuse up
     Debug.DrawLine(newPoint, windBlock.transform.position, Color.green);//todo green
     //hypotonuse down
     Debug.DrawLine(newPoint2, windBlock.transform.position, Color.cyan);//todo cyan
     //Rotation = this.transform.rotation.eulerAngles;
     */