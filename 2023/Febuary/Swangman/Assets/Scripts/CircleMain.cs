using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleMain : MonoBehaviour
{
    public static CircleMain mainCircle;
    public float rotateZ;
    public float speed;

    void Awake()
    {
        speed = 290;
        mainCircle = this;
        rotateZ = Time.deltaTime * speed * (1 / transform.localScale.x);

    }

    void Update()
    {
        if (GameManager.gameManager.isLose)
        {
            RotateCircle();
        }
    }

    void RotateCircle()
    {
        if (transform.localRotation.z >= 0.7 || transform.localRotation.z <= -0.7)
        {
            rotateZ = -rotateZ;
        }
        transform.Rotate(new Vector3(0, 0, rotateZ));
    }
}
