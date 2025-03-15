using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BoundaryDefault
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerControllerDefault : MonoBehaviour {
    public float speed;
    public float tilt;
    public BoundaryDefault boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public static float playSize;
    private Rigidbody rb;
    private float nextFire;
    static float topbound = 17F;
    static float bottombound = -17F;

    public int MovingAverageLength = 10;
    private int count;
    private float movingAverage;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        //playSize = Camera.main.orthographicSize;
        playSize = 30f;
        //rb.position = new Vector3(0f, 0f, 0f);
    }

    void Update ()
    { 
        
        if ((Input.GetKeyDown(KeyCode.F))|| (JediSerialPayload.totalForce >= AppData.handleGripForce) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
        PlayerControllerInput();
        //Debug.Log("freqq = " + (1 / Time.deltaTime));
    }

    //void FixedUpdate()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");
    //    //Debug.Log("move");
    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //    rb.velocity = movement * speed;

    //    rb.position = new Vector3(
    //            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
    //            0.0f,
    //            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
    //    );

    //    rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    //}
    public void PlayerControllerInput()
    {
        //rb.position = new Vector2(Angle2Screen(hyper1.instance.ang1), this.transform.position.y);
        rb.position = new Vector3 (MovingAverage(Angle2Screen(JediSerialPayload.angle_1)), this.transform.position.y, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        //rb.velocity = speed; //movement * speed;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
       // Debug.Log(Angle2Screen(hyper1.instance.ang1));
    }

    public static float Angle2Screen(float angle)
    {
        //float AngMin = PlayerPrefs.GetFloat("Handle Ang Min");
        //float AngMax = PlayerPrefs.GetFloat("Handle Ang Max");
        float AngMin = AppData.handleAngleMin;
        float AngMax = AppData.handleAngleMax;
        return Mathf.Clamp(- 15f + ((angle - AngMin) * (playSize) / (AngMax - AngMin)), bottombound, topbound);
    }

    public float MovingAverage(float values)
    {
        count++;


        if (count > MovingAverageLength)
        {
            movingAverage = movingAverage + (values - movingAverage) / (MovingAverageLength + 1);
        }
        else
        {
            movingAverage += values;

            if (count == MovingAverageLength)
            {
                movingAverage = movingAverage / count;

            }
        }
        return movingAverage;
    }
}
