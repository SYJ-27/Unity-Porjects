using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingLine : MonoBehaviour
{
    public float xPosition, yPosition, zRotation;

    public void InitPosition(){
        transform.position = new Vector3(xPosition, yPosition, 0);
        transform.Rotate(0, 0, zRotation);
    }

}
