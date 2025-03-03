using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShooterPlayerController : MonoBehaviour
{
	//public hyper1 hyperData;
	public float fireRate = 0.5F;
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;

	public Dropdown controllerMethod;
	private float myTime = 0.0F;
	private float nextFire = 0.5F;
	private Rigidbody rb;
	private AudioSource audioSource;
	float joyStickOffsetX;
	float joyStickOffsetY;
	float moveHorizontal;
	float moveVertical;
	public Text encVal;
    static float topbound = 10F;
    static float bottombound = 10F;
    public static float playSize;
    private Vector2 direction;



    void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		joyStickOffsetX = 700;
		joyStickOffsetY = 700;

	}

	void Update()
	{
		Time.timeScale = 1f;
		//Debug.Log(controllerMethod.value);
		myTime = myTime + Time.deltaTime;
		

		
		

	    if ((Input.GetKeyDown(KeyCode.P))||((hyper1.instance.buttonPin2State == 0) || hyper1.instance.force_total > 1 || hyper1.instance.buttonPin1State == 0 || hyper1.instance.buttonPin4State == 0) && myTime > nextFire) 
		{
				nextFire = myTime + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

				audioSource.Play();
				
				nextFire = nextFire - myTime;
				myTime = 0.0F;
		}

        PlayerControllerInput();










    }

	void FixedUpdate()
	{
        //if (controllerMethod.value == 0)
       // {
            //moveHorizontal = Mathf.Clamp((hyper1.instance.ang2) / 50, -3, 3);
            
            //moveHorizontal = Mathf.Clamp((hyperData.theta2) / 50, -3, 3);
            //moveVertical = -1;

           
       
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(
			0.0f,
			0.0f,
			rb.velocity.x * -tilt
		);
	}

    public void PlayerControllerInput()
    {
        float PcMech = PlayerPrefs.GetInt("Control Method");

        if (PcMech == 1)
        {
            transform.position = new Vector2(Angle2Screen(hyper1.instance.ang1), this.transform.position.y);
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }
        else 
        {
            PincerGraspControl();
            //code for limiting the play size.
            if (this.transform.position.x > topbound)
            {
                transform.position = new Vector2(topbound, this.transform.position.y);
            }
            else if (this.transform.position.x < bottombound)
            {
                transform.position = new Vector2(bottombound, this.transform.position.y);
            }
        }
             

    }

    public static float Angle2Screen(float angle)// Last used for handle surface testing
    {
        //angle1 = angle;
        float AngMin = PlayerPrefs.GetFloat("Handle Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Handle Ang Max");
        if (angle > 45f)
        {
            //return (-0.5f + (angle - 40) * (playSize) / (20 - (-90)));
            //return ((0.1636f * angle) - 10.8636f); // Last used for handle surface testing
            return (-2.3f + (angle - AngMin) * (playSize) / (AngMax - AngMin));
        }

        else
        {
            return (bottombound);
        }


    }

    private void PincerGraspControl()
    {
        if (hyper1.instance.buttonPin6State == 0)
        {
            //direction = Vector2.up;
            this.transform.Translate(0.05f, 0, 0);
        }
        else if (hyper1.instance.buttonPin7State == 0)
        {
            //direction = Vector2.down;
            this.transform.Translate(-0.05f, 0, 0);
        }
        else 
        {
            direction = Vector2.zero;
        }
    }
}
