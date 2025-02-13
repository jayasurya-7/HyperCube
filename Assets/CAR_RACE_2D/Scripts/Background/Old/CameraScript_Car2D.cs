using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript_Car2D : MonoBehaviour
{
    private float speed = 1f;
    private float accelaration = 0.2f;
    private float maxSpeed = 3.2f;
    //[HideInInspector]
    public bool moveCamera;



    // Start is called before the first frame update
    void Start()
    {
        moveCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        Vector3 temp = transform.position;

        float oldY = temp.y;

        float newY = temp.y + (speed * Time.deltaTime);

        temp.y = Mathf.Clamp(temp.y, newY, oldY);

        transform.position = temp;

        speed += accelaration * Time.deltaTime;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

}
