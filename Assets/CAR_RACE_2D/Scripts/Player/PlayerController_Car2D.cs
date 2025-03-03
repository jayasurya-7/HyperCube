using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class PlayerController_Car2D : MonoBehaviour
{
    public static PlayerController_Car2D instance;
    public bool isAlive;
    public static float playerSpeed = 1.0f;
    public float playerSideSpeed = 5f;
    public bool movePlayerRot = true;
    public float playerRot = 5f;
    public float playerRotSpeed = 5f;
    public int controlValue;
    
    public float[] spawnPlayerXPos = new float[4] { -2.25f, -0.75f, 0.75f, 2.25f };
    public float playerMinX = -0.25f;
    public float playerMaxX = 0.25f;

    //public GameObject crashParticles;
    //public GameObject turboEffect;
    //public ParticleSystem turboFlareL;
    //public ParticleSystem turboFlareR;
    //public ParticleSystem turboCoreL;
    //public ParticleSystem turboCoreR;
    //public float rotationMultiplier = 2.1f;

    public AudioClip[] carCrash;

    private float playerPosY;
    private float playerVelX;
    private Vector2 direction;

    public static Rigidbody2D rigBody2D;
    //public Dropdown ControlMethod;
    public TMPro.TMP_Dropdown ControlMethod;

    static float topbound = 4F;
    static float bottombound = 4F;
    public static float playSize;
    
    public static float[] rom;

    float previousangle;

    Vector2 velocity;

    Vector2 previous;
    float[] angles = new float[30];
    float StartPosition = 0;
    float[] temp;


    //Life
    int totalLife = 5;
    public int currentLife = 0;

    //Blinking effect
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 2f;
    public bool startBlinking = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        currentLife = 0;
        isAlive = true;
        rigBody2D = GetComponent<Rigidbody2D>();
        playerPosY = rigBody2D.position.y;
        playerVelX = 0;
        //playSize = Camera.main.orthographicSize;
        playSize = 12f;
        Debug.Log("camerasize=" + playSize);
        //AppData.reps = 0;
        //AppData.timeOnTrail = 0;
        topbound = 5.5f;
        bottombound = -topbound;
        //calculateROM();
       for (int i = 0; i < 30; i++)
        {
            angles[i] = 0;
        }
        //Debug.Log("start");
        
    }

    void FixedUpdate()
    {
        if (startBlinking == true)
        {
            SpriteBlinkingEffect();

        }
        //Debug.Log(hyper1.instance.ang2);
        //Debug.Log(ControlMethod.value);
        //InputControl();
        //ConrolData();
        gameData.events = Array.IndexOf(gameData.tukEvents, "moving");
        PlayerControllerInput();
        //KeyboardControl();
        //Vector2 target_pos = new Vector3(Angle2Screen(Average(angles)), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
        //rigBody2D.MovePosition(target_pos);
        //if (movePlayerRot)
        //{
        //    float num = 0.0f;
        //    if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
        //    {
        //        Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
        //        num = velocity.x;
        //    }
        //    rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
        //}

        //TripodGraspControl();
        //PincerGraspControl();
        //code for limiting the play size.
        //if (this.transform.position.x > topbound)
        //    {
        //        transform.position = new Vector2(topbound, this.transform.position.y);
        //    }
        //else if (this.transform.position.x < bottombound)
        //    {
        //        transform.position = new Vector2(bottombound, this.transform.position.y);
        //    }




        //if (ControlMethod.value == 0)
        //{
        //    Vector2 target_pos = new Vector3(Angle2Screen(Average(angles)), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
        //    rigBody2D.MovePosition(target_pos);
        //    if (movePlayerRot)
        //    {
        //        float num = 0.0f;
        //        if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
        //        {
        //            Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
        //            num = velocity.x;
        //        }
        //        rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
        //    }

        //}

        //else if (ControlMethod.value == 1)
        //{
        //    Vector2 target_pos = new Vector3(Angle2Screen1(Average(angles)), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
        //    rigBody2D.MovePosition(target_pos);
        //    if (movePlayerRot)
        //    {
        //        float num = 0.0f;
        //        if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
        //        {
        //            Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
        //            num = velocity.x;
        //        }
        //        rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
        //    }
        //}
        //else if (ControlMethod.value == 2)
        //{
        //    PincerGraspControl();
        //    code for limiting the play size.
        //    if (this.transform.position.x > topbound)
        //    {
        //        transform.position = new Vector2(topbound, this.transform.position.y);
        //    }
        //    else if (this.transform.position.x < bottombound)
        //    {
        //        transform.position = new Vector2(bottombound, this.transform.position.y);
        //    }
        //}
        //else
        //{
        //    TripodGraspControl();
        //    code for limiting the play size.
        //    if (this.transform.position.x > topbound)
        //    {
        //        transform.position = new Vector2(topbound, this.transform.position.y);
        //    }
        //    else if (this.transform.position.x < bottombound)
        //    {
        //        transform.position = new Vector2(bottombound, this.transform.position.y);
        //    }
        //}
        Debug.Log(Camera.main.orthographicSize);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("Obsstacle");
            startBlinking = true;
            if (carCrash[0])
            {
                currentLife++;
                Debug.Log("Car hit");
                gameData.events = Array.IndexOf(gameData.tukEvents, "collided");
                CAR_spawnTargets1.instance.reached = false;
                isAlive = false;
                SoundManager_Car2D.instance.PlaySound(carCrash[UnityEngine.Random.Range(0,carCrash.Length)]);
                if (currentLife >= totalLife)
                {

                    GameManager_Car2D.instance.timeleft = -1;
                    //GameManager_Car2D.instance.PlayerDied();
                    GameManager_Car2D.instance.isGameOver = true;
                    gameData.isGameLogging = false;
                    //FlappyGameControl.instance.BirdDied();
                    
                }
               
               // GameManager_Car2D.instance.GameOver();
            }
        }
        if (other.tag == "Target")
        {
            Debug.Log("Reached");
            gameData.gameScore++;
            gameData.events = Array.IndexOf(gameData.tukEvents, "passed");
            CAR_spawnTargets1.instance.reached = true;
        }
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

    //public float[] calculateROM()
    //{

    //    rom = new float[] { AppData.pROM()[0],  AppData.pROM()[1] };
    //    Debug.Log(rom[0] + ", level" + rom[1]);
    //    return rom;


    //}


    public void InputControl() //Previously used.
    {
         if (controlValue == 0)
        {
            Vector2 target_pos = new Vector3(Angle2Screen(Average(angles)), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }

        }

        else if (controlValue == 1)
        {
            Vector2 target_pos = new Vector3(Angle2Screen1(Average(angles)), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }
        }
        else if (controlValue == 2)
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
        else
        {
            TripodGraspControl();
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

    public void PlayerControllerInput()
    {
        float PcMech = PlayerPrefs.GetInt("Control Method");
        
        if (PcMech == 1)
        {
            Vector2 target_pos = new Vector3(Angle2Screen(hyper1.instance.ang1), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }
        }
        else if (PcMech == 2)
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
        else if (PcMech == 4)
        {
            Vector2 target_pos = new Vector3(Angle2Screen2(hyper1.instance.Avg_Btw_dist), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }
            //TripodGraspControl();
            ////code for limiting the play size.
            //if (this.transform.position.x > topbound)
            //{
            //    transform.position = new Vector2(topbound, this.transform.position.y);
            //}
            //else if (this.transform.position.x < bottombound)
            //{
            //    transform.position = new Vector2(bottombound, this.transform.position.y);
            //}
            //Debug.Log(Angle2Screen2(hyper1.instance.Avg_Btw_dist));
        }
        else if (PcMech == 5)
        {
            Vector2 target_pos = new Vector3(Angle2screenGrossKnob(hyper1.instance.ang2), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }

        }
        else if (PcMech == 6)
        {
            Vector2 target_pos = new Vector3(Angle2screenFineKnob(hyper1.instance.ang4), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }

        }
        else
        {
            Vector2 target_pos = new Vector3(Angle2screenKeyKnob(hyper1.instance.ang3), rigBody2D.position.y, 0f); //Replace angles value with the encoder value
            rigBody2D.MovePosition(target_pos);
            if (movePlayerRot)
            {
                float num = 0.0f;
                if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
                {
                    Vector2 velocity = (rigBody2D.position - target_pos) / Time.deltaTime;
                    num = velocity.x;
                }
                rigBody2D.rotation = Mathf.SmoothStep(rigBody2D.rotation, num * playerRot, Time.deltaTime * playerRotSpeed);
            }
        }

    }




    public static float Angle2Screen(float angle)
    {
        // return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        float AngMin = PlayerPrefs.GetFloat("Handle Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Handle Ang Max");
        //return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        //return((angle- AppData.aROM()[0])/(playSize- AppData.aROM()[0])*(-playSize -AppData.aROM()[1])+AppData.aROM()[1]);

        //set rom[0],rom[1] to the maximum
        //
        //return Mathf.Clamp(-playSize + (angle - rom[0]) * (2 * playSize) / (rom[1] - rom[0]), bottombound, topbound);
        //return Mathf.Clamp(-playSize + (angle - AngMin) * (2 * playSize) / (AngMax - AngMin), bottombound, topbound);

        //return Mathf.Clamp((0.2f*hyper1.instance.ang1-18.5f), bottombound, topbound); //handle encoder value.
        //return Mathf.Clamp((0.11f * hyper1.instance.ang2 - 7.7f), bottombound, topbound);
        return Mathf.Clamp((-2.3f + (angle - AngMin) * (playSize) / (AngMax - AngMin)), bottombound, topbound); 
    }

    public static float Angle2Screen1(float angle)
    {
       
        return Mathf.Clamp((0.11f * hyper1.instance.ang2 - 7.7f), bottombound, topbound);
    }
    
    public static float Angle2Screen2(float angle)
    {
        float DistMin = PlayerPrefs.GetFloat("Dist Min");
        float DistMax = PlayerPrefs.GetFloat("Dist Max");
        return Mathf.Clamp((-6f + (angle - DistMin) * (playSize) / (DistMax - DistMin)), bottombound, topbound);
        //return Mathf.Clamp((-17.38f + (angle) * (playSize) / (DistMax - DistMin)), bottombound, topbound);
    }

    public static float Angle2screenGrossKnob(float angle)
    {
        float AngMin = PlayerPrefs.GetFloat("Knob Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Ang Max");
        return Mathf.Clamp((-6f + (angle - AngMin) * (playSize) / (AngMax - AngMin)), bottombound, topbound);
    }

    public static float Angle2screenFineKnob(float angle)
    {
        float AngMin = PlayerPrefs.GetFloat("Knob Fine Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Fine Ang Max");
        return Mathf.Clamp((-6f + (angle - AngMax) * (playSize) / (AngMax - AngMin)), bottombound, topbound);
    }

    public static float Angle2screenKeyKnob(float angle)
    {
        float AngMin = PlayerPrefs.GetFloat("Knob Key Ang Min");
        float AngMax = PlayerPrefs.GetFloat("Knob Key Ang Max");
        return Mathf.Clamp((-6f + (angle - AngMin) * (playSize) / (AngMax - AngMin)), bottombound, topbound);
    }


    public void dropodownfn()
    {
        controlValue = ControlMethod.value;
    }



    public void Shiftreplace(float currentangle)
    {
        //float[] temparray = new float[10];
        for (int i = 0; i < angles.Length; i++)
        {
            if (i < angles.Length - 1)
            {
                angles[i] = angles[i + 1];
            }
            else
            {
                angles[i] = currentangle;
            }
        }
    }
    public float Average(float[] arr)
    
    {
        float sum = 0;
        float average = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }
        average = sum / arr.Length;
        return average;

    }

    private void KeyboardControl()
    {
        playerVelX = Input.GetAxis("Horizontal");
       // Debug.Log(playerVelX);
        rigBody2D.velocity = new Vector3(playerVelX, 0, 0) * playerSideSpeed;
        rigBody2D.position = new Vector3(Mathf.Clamp(rigBody2D.position.x, playerMinX, playerMaxX), playerPosY, 0f);
        if (movePlayerRot)
        {
            float num = 0.0f;
            if (rigBody2D.position.x < playerMaxX && rigBody2D.position.x > playerMinX)
            {
                num = rigBody2D.velocity.x;
            }
            rigBody2D.rotation = Mathf.Lerp(rigBody2D.rotation, num * -playerRot, Time.deltaTime * playerRotSpeed);

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

    private void TripodGraspControl()
    {
        if (hyper1.instance.Btw_dist > 6.5f)
        {
            this.transform.Translate(0.05f, 0, 0);
        }
        else if (hyper1.instance.Btw_dist < 4.5f)
        {

            this.transform.Translate(-0.05f, 0, 0);
        }
        else
        {
            direction = Vector2.zero;
        }

    }



}
