using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.IO.Ports;
using NeuroRehabLibrary;
using System.Linq;
using UnityEditor.Rendering;

public class ButtonClick : MonoBehaviour
{
    public TMPro.TMP_InputField HN;
    public InputField GripForce;
    public InputField HandleAngMin;
    public InputField HandleAngMax;
    public InputField ThumbDistMin;
    public InputField ThumbDistMax;
    public InputField IndexDistMin;
    public InputField IndexDistMax;
    public InputField KnobAngMin;
    public InputField KnobAngMax;
    public InputField FineKnobAngMin;
    public InputField FineKnobAngMax;
    public InputField KeyKnobAngMin;
    public InputField KeyKnobAngMax;
    public InputField DistMin;
    public InputField DistMax;
    public InputField Path;
    public InputField Space;
    public InputField Time;
    public InputField Auto;
    public InputField car1;
    public InputField car2;
    public InputField Pac1;
    public InputField Pac2;
    public InputField PP;
    public GameObject Canvas;
    public GameObject HandGripCanvas;
    public GameObject PinchGraspCanvas;
    public GameObject ButtonsCanvas;
    public GameObject TripodGraspCanvas;
    public GameObject GrossKnobCanvas;
    public GameObject FineKnobCanvas;
    public GameObject KeyKnobCanvas;
   
    //public TMPro.TMP_Dropdown ComPort;
    public GameObject settingCanvas;
    public GameObject SpeedCanvas;
    public GameObject Timer;

    public Text msg;

    public float Grip_Force;
    public float Thumb_dist_Min;
    public float Thumb_dist_Max;
    public float Index_dist_Min;
    public float Index_dist_Max;

    public int ComPortValue;
   // public Dropdown ComPortDropdown;
    private SerialPort serialPort;
    //JediSerialCom serReader;

    //Scenes

    private string flappyGame = "FlappyGame";
    private string spaceShooter = "Space";
    private string highwayRacer = "HIGHWAYRACER";
    private string pacMan = "menu";
    private string pongGame = "pong_menu";
    private string assessmentScene = "ROM Assessment";
    private string homeScene = "Home";
    private string snakeGame = "Snake Game";
    private string settings = "Settings";
    private string startScene = "Start";

    void Start()
    {
       // Timer.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }

    public void AutogameLoad()
    {
        SceneManager.LoadScene(flappyGame);
        PlayerPrefs.SetString("Game name", "Auto Ride");
        AppData.selectedGame = "autoRider";

    }

    public void HighwayRacerLoad()
    {
        SceneManager.LoadScene(highwayRacer);
        PlayerPrefs.SetString("Game name", "Highway Racer");
        AppData.selectedGame = "highwayRacer";
    }

    public void SpaceShooterLoad()
    {
        SceneManager.LoadScene(spaceShooter);
        PlayerPrefs.SetString("Game name", "Space Shooter");
        AppData.selectedGame = "spaceShooter";

    }

    public void PingPongLoad()
    {
        SceneManager.LoadScene(pongGame);
        PlayerPrefs.SetString("Game name", "Ping Pong");
        AppData.selectedGame = "pingPong";
    }

    public void HomeScreenLoad()
    {
        SceneManager.LoadScene("Main scene");
    }

    public void PacmanLoad()
    {
        SceneManager.LoadScene(pacMan);
        PlayerPrefs.SetString("Game name", "pacman");
        AppData.selectedGame = "pacMan";
    }

    public void SnakeLoad()
    {
        SceneManager.LoadScene(snakeGame);
        PlayerPrefs.SetString("Game name", "Snake2d");
        AppData.selectedGame = "snakeGame";
    }

    public void AssessmentSceneLoad()
    {
        SceneManager.LoadScene("Assessment");
        PlayerPrefs.SetString("Game name", "Assessment");
    }

    public void MechanismSceneLoad()
    {
        PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene(homeScene);


        string pth = PlayerPrefs.GetString("Address");
        string Hosp_number = PlayerPrefs.GetString("Hospital Number");


        string path_to_data = Application.dataPath;

       
        string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + Hosp_number;
        PlayerPrefs.SetFloat("Time", float.Parse(Time.text));
        
    }

    public void summaryScene()
    {
        SceneManager.LoadScene("summaryScene");
    }
    public void Set()
    {
        //float Grip_Force = float.Parse(GripForce.text);
        //float Thumb_dist_Min = float.Parse(ThumbDistMin.text);
        //float Thumb_dist_Max = float.Parse(ThumbDistMax.text);
        //float Index_dist_Min = float.Parse(IndexDistMin.text);
        //float Index_dist_Max = float.Parse(IndexDistMax.text);
        //PlayerPrefs.SetFloat("Grip force", Grip_Force);
        //PlayerPrefs.SetFloat("Thumb Min", Thumb_dist_Min);
        //PlayerPrefs.SetFloat("Thumb Max", Thumb_dist_Max);
        //PlayerPrefs.SetFloat("Index Min", Index_dist_Min);
        //PlayerPrefs.SetFloat("Index Max", Index_dist_Max);
        PlayerPrefs.SetFloat("Grip force", float.Parse(GripForce.text));
        PlayerPrefs.SetFloat("Handle Ang Min", float.Parse(HandleAngMin.text));
        PlayerPrefs.SetFloat("Handle Ang Max", float.Parse(HandleAngMax.text));
        PlayerPrefs.SetFloat("Knob Ang Min", float.Parse(KnobAngMin.text));
        PlayerPrefs.SetFloat("Knob Ang Max", float.Parse(KnobAngMax.text));
        PlayerPrefs.SetFloat("Knob FIne Ang Min", float.Parse(FineKnobAngMin.text));
        PlayerPrefs.SetFloat("Knob Fine Ang Max", float.Parse(FineKnobAngMax.text));
        PlayerPrefs.SetFloat("Knob Key Ang Min", float.Parse(KeyKnobAngMin.text));
        PlayerPrefs.SetFloat("Knob Key Ang Max", float.Parse(KeyKnobAngMax.text));
        //PlayerPrefs.SetFloat("Thumb Min", float.Parse(ThumbDistMin.text));
        // PlayerPrefs.SetFloat("Thumb Max", float.Parse(ThumbDistMax.text));
        //PlayerPrefs.SetFloat("Index Min", float.Parse(IndexDistMin.text));
        //PlayerPrefs.SetFloat("Index Max", float.Parse(IndexDistMax.text));
        PlayerPrefs.SetFloat("Dist Min", float.Parse(DistMin.text));
        PlayerPrefs.SetFloat("Dist Max", float.Parse(DistMax.text));
        SceneManager.LoadScene("Home");
    }

    public void HandGripGamesLoad()
    {
        Canvas.SetActive(false);
        HandGripCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[1];
        PlayerPrefs.SetInt("Control Method", 1);
        PlayerPrefs.SetString("Mechanism", "Hand Grip");
    }
    public void HandGripForceLoad()
    {
        Canvas.SetActive(false);
        HandGripCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[1];
        PlayerPrefs.SetInt("Control Method", 1);
        PlayerPrefs.SetString("Mechanism", "Hand Grip");
    }
    public void PinchGraspGamesLoad()
    {
        Canvas.SetActive(false);
        PinchGraspCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[6];

        PlayerPrefs.SetInt("Control Method", 2);
        PlayerPrefs.SetString("Mechanism", "Pinch Grasp");
    }

    public void ButtonGamesLoad()
    {
        Canvas.SetActive(false);
        ButtonsCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[7];

        PlayerPrefs.SetInt("Control Method", 3);
        PlayerPrefs.SetString("Mechanism", "Buttons");
    }

    public void TripodGraspGamesLoad()
    {
        Canvas.SetActive(false);
        TripodGraspCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[5];
        PlayerPrefs.SetInt("Control Method", 4);
        PlayerPrefs.SetString("Mechanism", "Tripod Grasp");
    }

    public void GrossKnobGamesLoad()
    {
        Canvas.SetActive(false);
        GrossKnobCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[2];
        PlayerPrefs.SetInt("Control Method", 5);
        PlayerPrefs.SetString("Mechanism", "Gross Knob");
        //PingPongLoad();
    }

    public void FineKnobGamesLoad()
    {
        Canvas.SetActive(false);
        FineKnobCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[3];
        PlayerPrefs.SetInt("Control Method", 6);
        PlayerPrefs.SetString("Mechanism", "Fine Knob");
        //PingPongLoad();
    }
    public void KeyKnobGamesLoad()
    {
        Canvas.SetActive(false);
        KeyKnobCanvas.SetActive(true);
        AppData.selectedMechanism = JediSerialPayload.MECHANISMS[4];
        PlayerPrefs.SetInt("Control Method", 7);
        PlayerPrefs.SetString("Mechanism", "Key Knob");
        //PingPongLoad();
    }

    public void LoadUIscene()
    {
        SceneManager.LoadScene(assessmentScene);
    }

    public void Return()
    {
        SceneManager.LoadScene(homeScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Settings()
    {
        SceneManager.LoadScene(settings);
    }

    public void ApplySettings()
    {
        //PlayerPrefs.SetString("Address", Path.text.ToString());
       msg.text = " [ ! ] Settings Applied ";
    }
   

    public void LoadGameSpeedControl()
    {
        settingCanvas.SetActive(false);
        SpeedCanvas.SetActive(true);
    }
    public void ReturnGamesettingCanvas()
    {
        settingCanvas.SetActive(true);
        SpeedCanvas.SetActive(false);
    }
    public void SetSpeedSpace()
    {
        PlayerPrefs.SetFloat("SpawnWait", float.Parse(Space.text));
    }
    public void SetSpeedAuto()
    {
        PlayerPrefs.SetFloat("ScrollSpeed", float.Parse(Auto.text));
    }
    public void SetSpeedPong()
    {
        PlayerPrefs.SetFloat("ScrollSpeed", float.Parse(Auto.text));
    }
    public void SetSpeedCar()
    {
        PlayerPrefs.SetFloat("SpawnTime", float.Parse(car1.text));
        PlayerPrefs.SetFloat("SpawnSpeed", float.Parse(car2.text));
    }
    public void SetSpeedPac()
    {
        PlayerPrefs.SetFloat("PlayerSpeed", float.Parse(Pac1.text));
        PlayerPrefs.SetFloat("GhostSpeed", float.Parse(Pac2.text));
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScene);
       // PlayerPrefs.SetFloat("Time", float.Parse(Time.text));
        
        Destroy(Timer.gameObject);
    }
}



   
