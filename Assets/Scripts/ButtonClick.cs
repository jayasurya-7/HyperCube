using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.IO.Ports;
using NeuroRehabLibrary;
using static hyper1;

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
    //public Dropdown ComPort;
    //public TMPro.TMP_Dropdown ComPort;
    public GameObject settingCanvas;
    public GameObject SpeedCanvas;
    public GameObject Timer;


    public float Grip_Force;
    public float Thumb_dist_Min;
    public float Thumb_dist_Max;
    public float Index_dist_Min;
    public float Index_dist_Max;

    public int ComPortValue;
    //public GameObject hypercube;
    //public float GF;

    // Start is called before the first frame update
    void start()
    {
        Timer.SetActive(false);
                
        //string[] ComPorts = GetComponent<hyper1>().Ports;
        //var dropdown = transform.GetComponent<Dropdown>();
        //dropdown.options.Clear();

        //foreach (string ComPort in ComPorts)
        //{
        //    dropdown.options.Add(new Dropdown.OptionData() { text = ComPort });
        //}


    }
    public void AutogameLoad()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("FlappyGame");
        PlayerPrefs.SetString("Game name", "Auto Ride");
        //DontDestroyOnLoad(hypercube);

    }

    public void HighwayRacerLoad()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("HIGHWAYRACER");
        PlayerPrefs.SetString("Game name", "Highway Racer");
    }

    public void SpaceShooterLoad()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("Space");
        PlayerPrefs.SetString("Game name", "Space Shooter");
   
    }

    public void PingPongLoad()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("pong_menu");
        PlayerPrefs.SetString("Game name", "Ping Pong");
    }

    public void HomeScreenLoad()
    {
        SceneManager.LoadScene("Main scene");
    }

    public void PacmanLoad()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("menu");
        PlayerPrefs.SetString("Game name", "pacman");
    }

    public void SnakeLoad()
    {
        SceneManager.LoadScene("Snake Game");
        PlayerPrefs.SetString("Game name", "Snake2d");
    }

    public void AssessmentSceneLoad()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("Assessment");
    }

    public void MechanismSceneLoad()
    {
        PlayerPrefs.SetString("Hospital Number", HN.text);
        SceneManager.LoadScene("Home");


        string pth = PlayerPrefs.GetString("Address");
        string Hosp_number = PlayerPrefs.GetString("Hospital Number");


        string path_to_data = Application.dataPath;

       
        string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + Hosp_number;
        //string baseDirectory = pth + Hosp_number;
        string baseDirectory = patientDir;


  SessionManager.Initialize(baseDirectory);

        SessionManager.Instance.Login();
        PlayerPrefs.SetFloat("Time", float.Parse(Time.text));

       

        //SessionManager.Instance.Login();
        
        //Timer.SetActive(true);
        
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
        PlayerPrefs.SetInt("Control Method", 1);
        PlayerPrefs.SetString("Mechanism", "Hand Grip");
    }

    public void PinchGraspGamesLoad()
    {
        Canvas.SetActive(false);
        PinchGraspCanvas.SetActive(true);
        PlayerPrefs.SetInt("Control Method", 2);
        PlayerPrefs.SetString("Mechanism", "Pinch Grasp");
    }

    public void ButtonGamesLoad()
    {
        Canvas.SetActive(false);
        ButtonsCanvas.SetActive(true);
        PlayerPrefs.SetInt("Control Method", 3);
        PlayerPrefs.SetString("Mechanism", "Buttons");
    }

    public void TripodGraspGamesLoad()
    {
        Canvas.SetActive(false);
        TripodGraspCanvas.SetActive(true);
        PlayerPrefs.SetInt("Control Method", 4);
        PlayerPrefs.SetString("Mechanism", "Tripod Grasp");
    }

    public void GrossKnobGamesLoad()
    {
        Canvas.SetActive(false);
        GrossKnobCanvas.SetActive(true);
        PlayerPrefs.SetInt("Control Method", 5);
        PlayerPrefs.SetString("Mechanism", "Gross Knob");
        //PingPongLoad();
    }

    public void FineKnobGamesLoad()
    {
        Canvas.SetActive(false);
        FineKnobCanvas.SetActive(true);
        PlayerPrefs.SetInt("Control Method", 6);
        PlayerPrefs.SetString("Mechanism", "Fine Knob");
        //PingPongLoad();
    }
    public void KeyKnobGamesLoad()
    {
        Canvas.SetActive(false);
        KeyKnobCanvas.SetActive(true);
        PlayerPrefs.SetInt("Control Method", 7);
        PlayerPrefs.SetString("Mechanism", "Key Knob");
        //PingPongLoad();
    }

    public void LoadUIscene()
    {
        SceneManager.LoadScene("ROM Assessment");
    }

    public void Return()
    {
        SceneManager.LoadScene("Home");
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
        SceneManager.LoadScene("Settings");
    }

    //public void ComPortDropdown()
    //{
    //    ComPortValue = ComPort.value;
        
    //}

    public void ApplySettings()
    {
        PlayerPrefs.SetString("Address", Path.text.ToString());
        //PlayerPrefs.SetFloat("Time", float.Parse(Time.text));
        //Debug.Log(ComPortValue);
        //if (ComPortValue == 0)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM3");
        //}
        //else if (ComPortValue == 1)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM4");
        //}
        //else if (ComPortValue == 2)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM5");
        //}
        //else if (ComPortValue == 3)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM6");
        //}
        //else if (ComPortValue == 4)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM7");
        //}
        //else if (ComPortValue == 5)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM8");
        //}
        //else if (ComPortValue == 6)
        //{
        //    PlayerPrefs.SetString("COMPort", "COM9");
        //}

    }
    
    //public void ApplyComPort()
    //{
    //    //PlayerPrefs.SetString("Address", Path.text);
    //    //Debug.Log(ComPortValue);
    //    if (ComPortValue == 1)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM3");
    //    }
    //    else if (ComPortValue == 2)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM4");
    //    }
    //    else if (ComPortValue == 3)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM5");
    //    }
    //    else if (ComPortValue == 4)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM6");
    //    }
    //    else if (ComPortValue == 5)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM7");
    //    }
    //    else if (ComPortValue == 6)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM8");
    //    }
    //    else if (ComPortValue == 7)
    //    {
    //        PlayerPrefs.SetString("COMPort", "COM9");
    //    }
    //    Debug.Log(PlayerPrefs.GetString("COMPort"));
    //}


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
        SceneManager.LoadScene("Start");
        //PlayerPrefs.SetFloat("Time", float.Parse(Time.text));
        
        Destroy(Timer.gameObject);
    }
}



   
