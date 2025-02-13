using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PongPlayerController : MonoBehaviour {

	//speed of player
	public float speed = 30;

	//bounds of player
	 static float topBound = 4.5F;
	static float bottomBound = -4.5F;
    // player controls
    public static hyper1 instance;
    private Vector2 direction;
    public static float playSize;
    //public float playSize;
    public static float[] rom;
    //  public static int FlipAngle = 1;
    //public static float tempRobot, tempBird;

    public float ballTrajetoryPrediction;
    public int controlValue;
    public TMPro.TMP_Dropdown ControlMethod;

    public static int reps;
    public int MovingAverageLength = 25;
    private int count;
    private float movingAverage;


    private void Awake()
    {
        //AppData.game = "PING PONG";
        //if(AppData.subjd.side == "LEFT")
        //{
        //    this.transform.position = new Vector2(-6,0);
        //}
        
    }
    // Use this for initialization
    void Start () {
        //playSize = Camera.main.orthographicSize;
        //AppData.reps = 0;
        playSize = topBound - bottomBound;
        //AppData.timeOnTrail = 0;
        Time.timeScale = 0;
        //topBound = playSize - this.transform.localScale.y / 4;
        //bottomBound = -topBound;
        //calculateROM();
        //AppData.WriteTrainingSummaryFile(5, 10);
    }

    
	
	// Update is called once per frame
	void Update ()
    {
        PlayerControllerInput();
        //InputControl();
        //Debug.Log(Angle2Screen(AppData.plutoData.angle));
        //if (Time.time != 0)
        //{
        //    AppData.timeOnTrail += Time.deltaTime;

        //    AppData.sessionDuration += Time.deltaTime;
        //}
        // Debug.Log(Angle2Screen(AppData.aROM()[0]));
        // Debug.Log(AppData.timeOnTrail);

        //if(GameObject.FindGameObjectsWithTag("Target") != null)
        //{

        //    ballTrajetoryPrediction = GameObject.FindGameObjectWithTag("Target").GetComponent<BaallTrajectoryPlotter>().targetPosition;

        //    //Debug.Log(ballTrajetoryPrediction);
        //}

        //this.transform.position = new Vector2(this.transform.position.x, ballTrajetoryPrediction);

        //this.transform.position = new Vector2(this.transform.position.x, Angle2Screen(AppData.plutoData.angle));  //useful.


        //      //get player input and set speed
        //      float movementSpeedY = speed * Input.GetAxis("Vertical") * Time.deltaTime;
        //transform.Translate(0, movementSpeedY, 0);
        //ButtonManipulation();
        //TripodGraspControl();
        //PincerGraspControl();
        //set bounds of player
        //if (transform.position.y > topBound)
        //{
        //    transform.position = new Vector3(transform.position.x, topBound, 0);
        //}
        //else if (transform.position.y < bottomBound)
        //{
        //    transform.position = new Vector3(transform.position.x, bottomBound, 0);
        //}

        Debug.Log(Angle2ScreenTripodGrasp(Mathf.Round((MovingAveragePingPong(hyper1.instance.Btw_dist))*10.0f)*0.1f));
    }
    public static float Angle2Screen(float angle)
    {

    //    //return Mathf.Clamp(-   +(angle -AppData.pROM()[0]) * (2 * playSize) / (AppData.pROM()[1] - AppData.pROM()[0]), bottomBound, topBound);
    //130 and -120 degrees.
            return Mathf.Clamp((0.036f * angle - 0.18f), bottomBound, topBound);


    }

    public float Angle2ScreenTripodGrasp(float angle)
    {
        float DistMin = PlayerPrefs.GetFloat("Dist Min");
        float DistMax = PlayerPrefs.GetFloat("Dist Max");
        return (-4.5f + (angle - DistMin) * (playSize) / (DistMax - DistMin));
        //return (-17.38f + (angle) * (playSize) / (DistMax - DistMin));
    }

    public float Angle2Screenknob(float angle)
    {   float AngMin = PlayerPrefs.GetFloat("Knob Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Ang Max");
        return (-4.5f + (angle - AngMin) * (playSize) / (AngMax - AngMin));
    }

    public float Angle2ScreenknobFine(float angle)
    {
        float AngMin = PlayerPrefs.GetFloat("Knob Fine Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Fine Ang Max");
        return (-4.5f + (angle - AngMin) * (playSize) / (AngMax - AngMin));
    }

    public float Angle2ScreenknobKey(float angle)
    {
        float AngMin = PlayerPrefs.GetFloat("Knob Key Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Key Ang Max");
        return (-4.5f + (angle - AngMin) * (playSize) / (AngMax - AngMin));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            //AppData.reps += 1;
            //Debug.Log(AppData.reps);
            
        }
    }
    private void OntriggerEnter2D(Collision2D collision)
    {
        Debug.Log("hello");
    }

    public void dropDown()
    {
        controlValue = ControlMethod.value;
    }

    //private void InputControl()
    //{
    //    if (controlValue == 0)
    //    {
    //        PincerGraspControl();
    //        //code for limiting the play size.
    //        if (transform.position.y > topBound)
    //        {
    //            transform.position = new Vector3(transform.position.x, topBound, 0);
    //        }
    //        else if (transform.position.y < bottomBound)
    //        {
    //            transform.position = new Vector3(transform.position.x, bottomBound, 0);
    //        }
    //    }
    //    else if (controlValue == 1)
    //    {
    //        TripodGraspControl();
    //        //code for limiting the play size.
    //        if (transform.position.y > topBound)
    //        {
    //            transform.position = new Vector3(transform.position.x, topBound, 0);
    //        }
    //        else if (transform.position.y < bottomBound)
    //        {
    //            transform.position = new Vector3(transform.position.x, bottomBound, 0);
    //        }
    //    }
    //    else
    //    {
    //        this.transform.position = new Vector2(this.transform.position.x, Angle2Screen(hyper1.instance.ang4));
    //    }

    //}

    public void PlayerControllerInput()
    {
        float PcMech = PlayerPrefs.GetInt("Control Method");

        
        if (PcMech == 2)
        {
            PincerGraspControl();
            //code for limiting the play size.
            if (transform.position.y > topBound)
            {
                transform.position = new Vector3(transform.position.x, topBound, 0);
            }
            else if (transform.position.y < bottomBound)
            {
                transform.position = new Vector3(transform.position.x, bottomBound, 0);
            }
        }
        else if (PcMech == 4)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenTripodGrasp(Mathf.Round((MovingAveragePingPong(hyper1.instance.Btw_dist))*10.0f)*0.1f));
            if (this.transform.position.y > topBound)
            {
                transform.position = new Vector2(this.transform.position.x, topBound);
            }
            else if (this.transform.position.y < bottomBound)
            {
                transform.position = new Vector2(this.transform.position.x, bottomBound);
            }

        }
        else if (PcMech == 5)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2Screenknob(hyper1.instance.ang2));
            if (this.transform.position.y > topBound)
            {
                transform.position = new Vector2(this.transform.position.x, topBound);
            }
            else if (this.transform.position.y < bottomBound)
            {
                transform.position = new Vector2(this.transform.position.x, bottomBound);
            }
        }
        else if (PcMech == 6 )
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenknobFine(hyper1.instance.ang4));
            if (this.transform.position.y > topBound)
            {
                transform.position = new Vector2(this.transform.position.x, topBound);
            }
            else if (this.transform.position.y < bottomBound)
            {
                transform.position = new Vector2(this.transform.position.x, bottomBound);
            }
        }
        else
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenknobKey(hyper1.instance.ang3));
            if (this.transform.position.y > topBound)
            {
                transform.position = new Vector2(this.transform.position.x, topBound);
            }
            else if (this.transform.position.y < bottomBound)
            {
                transform.position = new Vector2(this.transform.position.x, bottomBound);
            }
        }
    }

    private void PincerGraspControl()
    {
        if (hyper1.instance.buttonPin7State == 0)
        {
            //direction = Vector2.up;
            this.transform.Translate(0, 0.05f, 0);
        }
        else if (hyper1.instance.buttonPin6State == 0)
        {
            //direction = Vector2.down;
            this.transform.Translate(0, -0.05f, 0);
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void TripodGraspControl()
    {
        if (hyper1.instance.Btw_dist > 6.5f)
        {
            this.transform.Translate(0, 0.05f, 0);
        }
        else if (hyper1.instance.Btw_dist < 4.5f)
        {

            this.transform.Translate(0, -0.05f, 0);
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void ButtonManipulation()
    {
        if (hyper1.instance.buttonPin4State == 0)
        {
            //direction = Vector2.up;
            this.transform.Translate(0, 0.05f, 0);
        }
        else if (hyper1.instance.buttonPin2State == 0)
        {
            //direction = Vector2.down;
            this.transform.Translate(0, -0.05f, 0);
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    public float MovingAveragePingPong(float values)
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
