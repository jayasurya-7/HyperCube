 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
//using PlutoDataStructures;

public class BirdControl : MonoBehaviour
{
    public static hyper1 instance;
    private bool isDead = false;
    public static Rigidbody2D rb2d;
    Animator anime;
    // player controls

    public int controlValue;
    public static float playSize;
    public static int FlipAngle = 1;
    public static float tempRobot,tempBird;
    public bool set= false;
    public TMPro.TMP_Dropdown ControlMethod;
    public float angle1;

    int totalLife = 5;
    int currentLife = 0;
    bool columnHit;
    public Image life;
    public int collision_count;

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 2f;
    public bool startBlinking = false;

    public float speed = 0.001f;
    //public float down_speed = 3f;
    //public float up_speed = -3f;
    public float Player_translate_UpSpeed = 0.03f;
    public float Player_translate_DownSpeed = -0.03f;

    static float topbound = 5.5F;
    static float bottombound = -3.5F;
    //public static float playSize;
    public static float spawntime = 3f;
    private Vector2 direction;

    float startTime;
    float endTime;
    float loadcell;
    // Start is called before the first frame update

    float targetAngle;

    public FlappyGameControl FGC;

    //flappybird style for hand grip
    private Vector3 Direction;
    public float gravity = -9.8f;
    public float strength;

    void Start()
    {
        startTime = 0;
        endTime = 0;
        currentLife = 0;
        collision_count = 0;
        anime = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //anime.SetTrigger("Flap");
        //playSize = Camera.main.orthographicSize;
        Debug.Log("start");
        playSize = 2.3f + 5.5f;

    }

    // Update is called once per frame

    private void Update()
    {


        gameData.events = Array.IndexOf(gameData.tukEvents, "moving");

        //loadcell = -hyper1.instance.force_total + 2f;
        //KeyboardControl();
        //Debug.Log(hyper1.instance.ang1);

        // Used for handle surafce testing
        //transform.position = new Vector2(this.transform.position.x, Angle2Screen(hyper1.instance.ang1));
        //if (this.transform.position.y > topbound)
        //{
        //    transform.position = new Vector2(this.transform.position.x, topbound);
        //}
        //else if (this.transform.position.y < bottombound)
        //{
        //    transform.position = new Vector2(this.transform.position.x, bottombound);
        //}
        //if (hyper1.instance.force_total < 1)
        //{
        //    FlappyGameControl.instance.scrollSpeed = -3f;
        //}
        //else
        //{
        //    FlappyGameControl.instance.scrollSpeed = -hyper1.instance.force_total * 2f;
        //}

        //ControlInput();

        PlayerControllerInput();
        //strength = hyper1.instance.force_total;
        //Direction = Vector3.up * strength;
        //Direction.y += gravity * Time.deltaTime;
        //transform.position += Direction * Time.deltaTime;

        //Debug.Log("freqq = " + (1 / Time.deltaTime));
        //Debug.Log(Time.deltaTime);
        if (startBlinking == true)
        {
            if (collision_count <= totalLife)
            {
                SpriteBlinkingEffect();
            }
        }
    }

    void FixedUpdate()
    {
        

        //KeyboardControl();
        if (startTime < 2)
        {
            startTime += Time.deltaTime;
        }
        if (startBlinking == true)
        {
            SpriteBlinkingEffect();
            
        }

        //Debug.Log(AppData.plutoData.angle);
        if (!isDead)
        {//
         // 
            if (columnHit)
            {
                anime.SetTrigger("Idle");
                columnHit = false;
            }
            //targetAngle = approxRollingAverage(targetAngle, this.transform.position.y);
            //transform.position = new Vector2(Mathf.SmoothStep(-13,-7,startTime/2), Mathf.Clamp(targetAngle,-2.5f,7));
            //rb2d./*MovePosition*/(new Vector2(-7, Angle2Screen(AppData.plutoData.angle)));
        }
        //else if (FGC.gameOver)
        //{
        //    endTime += Time.deltaTime;

        //    float y = Mathf.Abs(2 * Mathf.Sin(2 * endTime));
        //    transform.localPosition = new Vector3(transform.position.x+3*Time.deltaTime, transform.position.y, 0);
        //}

        anime.updateMode = AnimatorUpdateMode.UnscaledTime;

    }
    float approxRollingAverage(float avg, float new_sample)
    {
        
       avg = avg*0.9f + 0.1f* new_sample;

        return avg;
    }
    public void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 
                                                                             //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("yippie");
        Debug.Log("Collion " + collision.gameObject.tag);
        startBlinking = true;
        collision_count++;
        if (collision.gameObject.tag == "TopCollider" || collision.gameObject.tag == "BottomCollider") {

            gameData.events = Array.IndexOf(gameData.tukEvents, "collided");

            startBlinking = true;
            currentLife++;
            life.fillAmount = ((float)currentLife / totalLife);
            // anime.SetTrigger("Die");
            columnHit = true;
            if (currentLife >= totalLife)
            {

                FlappyGameControl.instance.gameduration = -1;
                FlappyGameControl.instance.gameOver = true;
                //FlappyGameControl.instance.BirdDied();
                anime.SetTrigger("Die");
                isDead = true;
                anime.SetTrigger("Die");
            }
           
        }
        
    }
    //public static float Angle2Screen(float angle)// Last used for handle surface testing
    //{
    //    //angle1 = angle;

    //    if (angle > 45f)
    //    {
    //        //return (-0.5f + (angle - 40) * (playSize) / (20 - (-90)));
    //        return ((0.1636f * angle) - 10.8636f); // Last used for handle surface testing
    //    }

    //    else
    //    {
    //        return (bottombound);
    //    }


    //}

    public void dropdownfn()
    {
        controlValue = ControlMethod.value;
    }

    public void ControlInput()//Previously used.
    {
        if (controlValue == 1)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2Screen(hyper1.instance.ang1));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
            if (hyper1.instance.force_total < 1)
            {
                FlappyGameControl.instance.scrollSpeed = -3f;
            }
            else
            {
                FlappyGameControl.instance.scrollSpeed = -hyper1.instance.force_total * 2f;
            }
        }

        else if (controlValue == 0)
        {
            PincerGraspControl();
            //code for limiting the play size.
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }
        else if (controlValue == 2)
        {
            TripodGraspControl();
            //code for limiting the play size.
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }

        }
        else if (controlValue == 3)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenFineKnob(hyper1.instance.ang4));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }

        //else if (controlValue == 4)
        //{
        //    transform.position = new Vector2(this.transform.position.x, Angle2ScreenGrossKnob(hyper1.instance.ang2));
        //    if (this.transform.position.y > topbound)
        //    {
        //        transform.position = new Vector2(this.transform.position.x, topbound);
        //    }
        //    else if (this.transform.position.y < bottombound)
        //    {
        //        transform.position = new Vector2(this.transform.position.x, bottombound);
        //    }
        //}
        else if (controlValue == 4)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenGrossKnob(JediSerialPayload.angle_2));
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
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenKeyHold(hyper1.instance.ang3));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }

    }

    public void PlayerControllerInput()
    {
        float PcMech = PlayerPrefs.GetInt("Control Method");
        
        if (PcMech == 1)
        {
            //transform.position = new Vector2(this.transform.position.x, Angle2Screen(hyper1.instance.ang1));
            //if (this.transform.position.y > topbound)
            //{
            //    transform.position = new Vector2(this.transform.position.x, topbound);
            //}
            //else if (this.transform.position.y < bottombound)
            //{
            //    transform.position = new Vector2(this.transform.position.x, bottombound);
            //}
            //if (hyper1.instance.force_total < 1)
            //{
            //    FlappyGameControl.instance.scrollSpeed = -3f;
            //}
            //else
            //{
            //    FlappyGameControl.instance.scrollSpeed = -hyper1.instance.force_total * 2f;
            //}
            transform.position = new Vector2(this.transform.position.x, Force2Screen(hyper1.instance.force_total));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
            Debug.Log(hyper1.instance.force_total);

        }
        else if (PcMech == 2)
        {
            PincerGraspControl();
            //code for limiting the play size.
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }
        else if (PcMech == 3)
        {
            ButtonControl();
            //code for limiting the play size.
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }

        else if (PcMech == 4)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenTripodGrasp(hyper1.instance.Avg_Btw_dist));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {    
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }

        }
        else if (PcMech == 5)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenGrossKnob(hyper1.instance.ang2));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }

        }
        else if (PcMech == 6)
        {
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenFineKnob(hyper1.instance.ang4));
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
            transform.position = new Vector2(this.transform.position.x, Angle2ScreenKeyHold(hyper1.instance.ang3));
            if (this.transform.position.y > topbound)
            {
                transform.position = new Vector2(this.transform.position.x, topbound);
            }
            else if (this.transform.position.y < bottombound)
            {
                transform.position = new Vector2(this.transform.position.x, bottombound);
            }
        }

    }
    public static float Force2Screen(float force)
    {   float GripForce = PlayerPrefs.GetFloat("Grip force");
        return (-2.3f + (force - 2.3f ) * (playSize) / (GripForce - 2.3f));
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
    public static float Angle2ScreenTripodGrasp(float angle)// Last used for handle surface testing
    {
        //angle1 = angle;
        float DistMin = PlayerPrefs.GetFloat("Dist Min");
        float DistMax = PlayerPrefs.GetFloat("Dist Max");
        
        
            //return (-0.5f + (angle - 40) * (playSize) / (20 - (-90)));
            //return ((0.1636f * angle) - 10.8636f); // Last used for handle surface testing
            return (-2.3f + (angle - DistMin) * (playSize) / (DistMax - DistMin));
        

       


    }

    public static float Angle2ScreenFineKnob(float angle)
    {

        //130 and -120 degrees.
        // return Mathf.Clamp((0.036f * angle + 0.82f), bottombound, topbound);
        float AngMin = PlayerPrefs.GetFloat("Knob Fine Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Fine Ang Max");
        return (-2.3f + (angle - AngMin) * (playSize) / (AngMax - AngMin));

    }

    public static float Angle2ScreenGrossKnob(float angle)
    {

        //45 and -60 degrees.
        //return Mathf.Clamp((-0.085f * angle + 0.325f), bottombound, topbound);
        float AngMin = PlayerPrefs.GetFloat("Knob Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Ang Max");
        return (-2.3f + (angle - AngMin) * (playSize) / (AngMax - AngMin));

    }

    public static float Angle2ScreenKeyHold(float angle)
    {

        //95 and -40 degrees.
        //return Mathf.Clamp((-0.067f * angle + 2.865f), bottombound, topbound);
        float AngMin = PlayerPrefs.GetFloat("Knob Key Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Key Ang Max");
        return (-2.3f + (angle - AngMin) * (playSize) / (AngMax - AngMin));

    }

    private void KeyboardControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
            this.transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
            this.transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    //private void PincerGraspControl()
    //{
    //    if (hyper1.instance.buttonPin6State == 0)
    //    {
    //        direction = Vector2.up;
    //        this.transform.Translate(direction * speed * Time.deltaTime);
    //    }
    //    else if (hyper1.instance.buttonPin7State == 0)
    //    {
    //        direction = Vector2.down;
    //        this.transform.Translate(direction * speed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        direction = Vector2.zero;
    //    }
    //}


    private void PincerGraspControl()
    {
        if (hyper1.instance.buttonPin7State == 0)
        {
            //direction = Vector2.up;
            this.transform.Translate(0, Player_translate_UpSpeed, 0);
        }
        else if (hyper1.instance.buttonPin6State == 0)
        {
            //direction = Vector2.down;
            this.transform.Translate(0, Player_translate_DownSpeed, 0);
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void ButtonControl()
    {
        if (hyper1.instance.buttonPin3State == 0)
        {
            //direction = Vector2.up;
            this.transform.Translate(0, Player_translate_UpSpeed, 0);
        }
        else if (hyper1.instance.buttonPin1State == 0)
        {
            //direction = Vector2.down;
            this.transform.Translate(0, Player_translate_DownSpeed, 0);
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
            this.transform.Translate(0, Player_translate_UpSpeed, 0);
        }
        else if (hyper1.instance.Btw_dist < 4.5f)
        {

            this.transform.Translate(0, Player_translate_DownSpeed, 0);
        }
        else
        {
            direction = Vector2.zero;
        }
    }
}
