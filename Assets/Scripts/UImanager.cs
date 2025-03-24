using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using NeuroRehabLibrary;
using System.IO;
using System.Text;

public class UImanager : MonoBehaviour
{
    //HandGrip 
    bool HandleMax = false;
    bool HandleMin = false;
    bool HandleAngSet = false;
    public Image RadialBar1;
    public Image RadialBar2;
    public Image ForceBar;
    public TMPro.TMP_Text HandleAngle;
    public TMPro.TMP_Text HandGripForce;
    public TMPro.TMP_Text ForceAcknowledgement;
    public GameObject Instruction3;
    public GameObject Instruction4;
    public TMPro.TMP_Text HandleMaxAngle;
    public TMPro.TMP_Text HandleMinAngle;


    //Gross knob ROM Canvas
    public Image GrossCircleClock;
    public Image GrossCircleAnticlock;
    public TMPro.TMP_Text GrossAngle;
    public TMPro.TMP_Text GrossMaxAngle;
    public TMPro.TMP_Text GrossMinAngle;
    public GameObject GrossInstruction_1;
    public GameObject GrossInstruction_2;
    public GameObject GrossInstruction_3;
    public GameObject GrossInstruction_4;
    public GameObject GrossInstruction_5;
    bool GrossClockwise = false;
    bool GrossAnticlockwise = false;
    bool GrossMaxAngSet = false;
    bool GrossMinAngSet = false;

    //Fine knob ROM Canvas
    public Image FineCircleClock;
    public Image FineCircleAnticlock;
    public TMPro.TMP_Text FineAngle;
    public TMPro.TMP_Text FineMaxAngle;
    public TMPro.TMP_Text FineMinAngle;
    public GameObject FineInstruction_1;
    public GameObject FineInstruction_2;
    public GameObject FineInstruction_3;
    public GameObject FineInstruction_4;
    public GameObject FineInstruction_5;
    bool FineClockwise = false;
    bool FineAnticlockwise = false;
    bool FineMaxAngSet = false;
    bool FineMinAngSet = false;

    //Key Knob ROM Canvas
    public Image KeyCircleClock;
    public Image KeyCircleAnticlock;
    public TMPro.TMP_Text KeyAngle;
    public TMPro.TMP_Text KeyMaxAngle;
    public TMPro.TMP_Text KeyMinAngle;
    public GameObject KeyInstruction_1;
    public GameObject KeyInstruction_2;
    public GameObject KeyInstruction_3;
    public GameObject KeyInstruction_4;
    public GameObject KeyInstruction_5;
    bool KeyClockwise = false;
    bool KeyAnticlockwise = false;
    bool KeyMaxAngSet = false;
    bool KeyMinAngSet = false;

    //Tripod grasp ROM Canvas
    public Image HorizontalBar_1;
    public Image HorizontalBar_2;
    public Image HorizontalBar_3;
    public Image HorizontalBar_4;
    public TMPro.TMP_Text DistanceValues;
    public TMPro.TMP_Text MaxDistance;
    public TMPro.TMP_Text MinDistance;
    public GameObject GraspInstruction_1;
    public GameObject GraspInstruction_2;
    public GameObject GraspInstruction_3;
    public GameObject GraspInstruction_4;
    public GameObject GraspInstruction_5;
    bool GraspMaxSet = false;
    bool GraspMinSet = false;
    bool GraspIn = false;
    bool GraspOut = false;

    // Canvas
    public GameObject BaseCanvas;
    public GameObject HandGripCanvas;
    public GameObject HandGripForceCanvas;
    public GameObject GrossKnobCanvas;
    public GameObject FineKnobCanvas;
    public GameObject KeyKnobCanvas;
    public GameObject TripodCanvas;

    private string homeScene = "home";

    //array gameCanvas

    public GameObject[] assessmentScenes;


    private GameSession currentGameSession;


    // Mech Reset Bool;
    private bool handleRom = false;
    private bool handleGrip = false;
    private bool grossKnob = false;
    private bool fineKnob = false;
    private bool keyKnob = false;
    private bool tripodgrasp = false;

    private int enterCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        //ConnectToHypercube.Connect("COM10");


        //StartNewGameSession();
        Debug.Log($"handle :{AppData.handleAngleMax}, handle  :{AppData.handleAngleMin}, grip :{AppData.handleGripForce},gross :{AppData.grossKnobMax},gross min:{AppData.grossKnobMin}, fine max:{AppData.fineKnobMax}, fine min :{AppData.fineKnobMin}," +
            $",key max : {AppData.keyKnobMax},key Min :{AppData.keyKnobMin}, grasp max :{AppData.graspMax},grasp Min :{AppData.graspMin}");

    }



    //void StartNewGameSession()
    //{
    //    currentGameSession = new GameSession
    //    {
    //        GameName = "ASSESSMENT",
    //        Assessment = 1 // Example assessment value, adjust as needed
    //    };

    //    SessionManager.Instance.StartGameSession(currentGameSession);
    //    Debug.Log($"Started new game session with session number: {currentGameSession.SessionNumber}");

    //    SetSessionDetails();
    //}


    //private void SetSessionDetails()
    //{

    //    string device = "HYPERCUBE"; // Set the device name
    //    string assistMode = "Null"; // Set the assist mode
    //    string assistModeParameters = "Null"; // Set the assist mode parameters
    //    string deviceSetupLocation = "Null"; // Set the device setup location
    //    string gameParameter = "Null";
    //    string mech = AppData.selectedMechanism;
    //    SessionManager.Instance.SetDevice(device, currentGameSession);
    //    SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
    //    SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
    //    SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);
    //    SessionManager.Instance.mechanism(mech, currentGameSession);

    //}


    //void EndCurrentGameSession()
    //{
    //    if (currentGameSession != null)
    //    {
    //        string trialDataFileLocation = "Null";
    //        SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

    //        SessionManager.Instance.EndGameSession(currentGameSession);
    //    }
    //}

    

    void Update()
    {
     
        HandleAngleROM();
        HandleForce();
        GrossKnob();
        FineKnob();
        KeyKnob();
        TripodGrasp();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

    }


    private float angleValue( float angle)
    {
        return (Mathf.Round(angle * 10.0f) * 0.1f);
    }

    public void HandleAngleROM()
    {
        HandleAngle.text = angleValue(JediSerialPayload.angle_1) + "Deg".ToString();

        if (!handleRom && HandGripCanvas.gameObject.activeSelf)
        {
            if (HandleAngSet == false)
            {
                RadialBar1.fillAmount = 0.00403f * JediSerialPayload.angle_1;
            }

            if (HandleMax == false)
            {
                RadialBar2.fillAmount = 0.5f - (0.00403f * JediSerialPayload.angle_1);
            }

            if (HandleMax == false && Input.GetKeyDown(KeyCode.Return))
            {
                AppData.handleAngleMax = JediSerialPayload.angle_1;
                HandleMax = true;
                Instruction3.SetActive(false);
                Instruction4.SetActive(true);
                HandleMaxAngle.text = "Max Angle: " + angleValue(AppData.handleAngleMax).ToString();
            }

            if (HandleMin == false && Input.GetKeyDown(KeyCode.Space))
            {
                AppData.handleAngleMin = JediSerialPayload.angle_1;

                HandleMin = true;
                HandleAngSet = true;

                Instruction4.SetActive(false);
                HandleMinAngle.text = "Min Angle: " + angleValue(AppData.handleAngleMin).ToString();
                handleRom = true;
            }
        }
    }

    public void HandleForce()
    {
        if (!handleGrip)
        {
            ForceBar.fillAmount = 0.05f * JediSerialPayload.totalForce;
            HandGripForce.text = angleValue(JediSerialPayload.totalForce) + "Kgs".ToString();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                AppData.handleGripForce = JediSerialPayload.totalForce;
                ForceAcknowledgement.text = "Threshold force set as  " + AppData.handleGripForce+ "  Kgs".ToString();
                handleGrip = true;
            }

        }
    }

    public void GrossKnob()
    {

        if (!grossKnob)
        {
            GrossAngle.text = angleValue(JediSerialPayload.angle_2).ToString();

            if (GrossClockwise == false && GrossAnticlockwise == false)
            {
                GrossCircleClock.fillAmount = 0.00277f * JediSerialPayload.angle_2;
            }

            if (GrossClockwise == false && Input.GetKeyDown(KeyCode.Return))
            {
                if (GrossMaxAngSet == false)
                {
                    AppData.grossKnobMax = JediSerialPayload.angle_2;

                    GrossMaxAngle.text = "Max angle: " + AppData.grossKnobMax.ToString();
                    GrossInstruction_1.SetActive(false);
                    GrossInstruction_3.SetActive(false);
                    GrossClockwise = true;
                    GrossMaxAngSet = true;
                }
            }

            if (GrossClockwise == true)
            {
                GrossCircleAnticlock.fillAmount = -0.00277f * JediSerialPayload.angle_2;
                GrossInstruction_2.SetActive(true);
                GrossInstruction_4.SetActive(true);
                GrossAnticlockwise = true;
            }

            if (GrossAnticlockwise == true && Input.GetKeyDown(KeyCode.Space))
            {
                if (GrossMinAngSet == false)
                {
                    AppData.grossKnobMin = JediSerialPayload.angle_2;

                    GrossMinAngle.text = "Min angle: " + AppData.grossKnobMin.ToString();
                    GrossClockwise = false;
                    GrossMinAngSet = true;
                    GrossInstruction_2.SetActive(false);
                    GrossInstruction_4.SetActive(false);
                    GrossInstruction_5.SetActive(true);

                    grossKnob = true;
                }
            }
        }
    }

    public void FineKnob()
    {
        if (!fineKnob)
        {
            FineAngle.text = angleValue(JediSerialPayload.angle_4).ToString();

            if (FineClockwise == false && FineAnticlockwise == false)
            {
                FineCircleClock.fillAmount = 0.00277f * JediSerialPayload.angle_4;
            }

            if (FineClockwise == false && Input.GetKeyDown(KeyCode.Return))
            {
                if (FineMaxAngSet == false)
                {
                    AppData.fineKnobMax = JediSerialPayload.angle_4;

                    FineMaxAngle.text = "Max angle: " + AppData.fineKnobMax.ToString();
                    FineInstruction_1.SetActive(false);
                    FineInstruction_3.SetActive(false);
                    FineClockwise = true;
                    FineMaxAngSet = true;
                }
            }

            if (FineClockwise == true)
            {
                FineCircleAnticlock.fillAmount = -0.00277f * JediSerialPayload.angle_4;
                FineInstruction_2.SetActive(true);
                FineInstruction_4.SetActive(true);
                FineAnticlockwise = true;
            }

            if (FineAnticlockwise == true && Input.GetKeyDown(KeyCode.Space))
            {
                if (FineMinAngSet == false)
                {
                    AppData.fineKnobMin = JediSerialPayload.angle_4;
                    FineMinAngle.text = "Min angle: " + AppData.fineKnobMin.ToString();
                    FineClockwise = false;
                    FineMinAngSet = true;
                    FineInstruction_2.SetActive(false);
                    FineInstruction_4.SetActive(false);
                    FineInstruction_5.SetActive(true);

                    fineKnob = false;
                }
            }
        }
    }

    public void KeyKnob()
    {
        KeyAngle.text = angleValue(JediSerialPayload.angle_3).ToString();

        if (!keyKnob)
        {
            if (KeyClockwise == false && KeyAnticlockwise == false)
            {
                KeyCircleClock.fillAmount = 0.00277f * JediSerialPayload.angle_3;
            }

            if (KeyClockwise == false && Input.GetKeyDown(KeyCode.Return))
            {
                if (KeyMaxAngSet == false)
                {
                    AppData.keyKnobMax = JediSerialPayload.angle_3;
                    KeyMaxAngle.text = "Max angle: " + AppData.keyKnobMax.ToString();
                    KeyInstruction_1.SetActive(false);
                    KeyInstruction_3.SetActive(false);
                    KeyClockwise = true;
                    KeyMaxAngSet = true;
                }
            }

            if (KeyClockwise == true)
            {
                KeyCircleAnticlock.fillAmount = -0.00277f * JediSerialPayload.angle_3;
                KeyInstruction_2.SetActive(true);
                KeyInstruction_4.SetActive(true);
                KeyAnticlockwise = true;
            }

            if (KeyAnticlockwise == true && Input.GetKeyDown(KeyCode.Space))
            {
                if (KeyMinAngSet == false)
                {
                    AppData.keyKnobMin = JediSerialPayload.angle_3;

                    KeyMinAngle.text = "Min angle: " + AppData.keyKnobMin.ToString();
                    KeyClockwise = false;
                    KeyMinAngSet = true;
                    KeyInstruction_2.SetActive(false);
                    KeyInstruction_4.SetActive(false);
                    KeyInstruction_5.SetActive(true);

                    keyKnob = true;
                }


            }
        }
    }

    public void TripodGrasp()
    {
        DistanceValues.text = angleValue(JediSerialPayload.avgBtwDistance).ToString();

        if (!tripodgrasp)
        {
            if (GraspIn == false && GraspOut == false)
            {
                HorizontalBar_1.fillAmount = 0.625f * ((JediSerialPayload.avgBtwDistance) / 2.0f) - 1.0f;
                HorizontalBar_2.fillAmount = 0.625f * ((JediSerialPayload.avgBtwDistance) / 2.0f) - 1.0f;
            }

            if (GraspIn == false && Input.GetKeyDown(KeyCode.Return))
            {
                if (GraspMaxSet == false)
                {
                    AppData.graspMin = angleValue(JediSerialPayload.btwDistance);
                    MaxDistance.text = "Min distance: " + AppData.graspMin.ToString();
                    GraspInstruction_1.SetActive(false);
                    GraspInstruction_3.SetActive(false);
                    GraspIn = true;
                    GraspMaxSet = true;
                }
            }

            if (GraspIn == true)
            {
                HorizontalBar_3.fillAmount = 0.625f * ((JediSerialPayload.avgBtwDistance) / 2.0f) - 1.0f;
                HorizontalBar_4.fillAmount = 0.625f * ((JediSerialPayload.avgBtwDistance) / 2.0f) - 1.0f;
                GraspInstruction_2.SetActive(true);
                GraspInstruction_4.SetActive(true);
                GraspOut = true;
            }

            if (GraspOut == true && Input.GetKeyDown(KeyCode.Space))
            {
                if (GraspMinSet == false)
                {
                    AppData.graspMax = angleValue(JediSerialPayload.btwDistance);
                    MinDistance.text = "Max distance: " + AppData.graspMax.ToString();
                    GraspInstruction_2.SetActive(false);
                    GraspInstruction_4.SetActive(false);
                    GraspInstruction_5.SetActive(true);
                    GraspIn = false;
                    GraspMinSet = true;

                    tripodgrasp = true;
                }
            }
        }

    }

    public void Reset()
    {
        if (BaseCanvas.gameObject.activeSelf)
        {
            Debug.Log("Canvas 1 is active");
        }
        else if (HandGripForceCanvas.gameObject.activeSelf)
        {
            handleGrip = false;
            Debug.Log(" grip Canvas 2 is active");
        }
        else if (HandGripCanvas.gameObject.activeSelf)
        {
            // Hand Grip ROM
            HandleMax = false;
            HandleMin = false;
            HandleAngSet = false;
            Instruction3.SetActive(true);
            handleRom = false;
            Debug.Log(" handle rom Canvas 2 is active");
        }
        else if (GrossKnobCanvas.gameObject.activeSelf)
        {

            //Gross Knob ROM
            GrossMinAngSet = false;
            GrossMaxAngSet = false;
            GrossAnticlockwise = false;
            GrossInstruction_1.SetActive(true);
            GrossInstruction_3.SetActive(true);
            GrossInstruction_5.SetActive(false);

            grossKnob=false;
            Debug.Log(" gross Canvas 2 is active");
        }
        else if (KeyKnobCanvas.gameObject.activeSelf)
        {
            //Key Knob ROM
            KeyMinAngSet = false;
            KeyMaxAngSet = false;
            KeyAnticlockwise = false;
            KeyInstruction_1.SetActive(true);
            KeyInstruction_3.SetActive(true);
            KeyInstruction_5.SetActive(false);
            keyKnob = false;
            Debug.Log("key Canvas 2 is active");
        }
        else if (FineKnobCanvas.gameObject.activeSelf)
        {


            //Fine Knob ROM
            FineMinAngSet = false;
            FineMaxAngSet = false;
            FineAnticlockwise = false;
            FineInstruction_1.SetActive(true);
            FineInstruction_3.SetActive(true);
            FineInstruction_5.SetActive(false);

            fineKnob = false;

            Debug.Log(" fine Canvas 2 is active");
        }
        else if (TripodCanvas.gameObject.activeSelf)
        {

            //Grasp ROM
            GraspMaxSet = false;
            GraspMinSet = false;
            GraspOut = false;
            GraspInstruction_1.SetActive(true);
            GraspInstruction_3.SetActive(true);
            GraspInstruction_5.SetActive(false);

            tripodgrasp = false;
            Debug.Log("tripod Canvas 2 is active");
        }

        
       



    }

    public void HandgripCanvasLoad()
    {
        HandGripCanvas.SetActive(true);
        BaseCanvas.SetActive(false);
    }

    public void HandgripForceCanvasLoad()
    {
        HandGripForceCanvas.SetActive(true);
        BaseCanvas.SetActive(false);
    }

    public void GrossKnobCanvasLoad()
    {
        GrossKnobCanvas.SetActive(true);
        BaseCanvas.SetActive(false);
    }

    public void FineKnobCanvasLoad()
    {
        FineKnobCanvas.SetActive(true);
        BaseCanvas.SetActive(false);
    }

    public void KeyKnobCanvasLoad()
    {
        KeyKnobCanvas.SetActive(true);
        BaseCanvas.SetActive(false);
    }

    public void TripodGraspCanvasLoad()
    {
        TripodCanvas.SetActive(true);
        BaseCanvas.SetActive(false);
    }

    public void ReturnToHome()
    {

        string dir = Path.Combine(AppData.idPath, "rom.csv");
        if (!File.Exists(dir))
        {
            using (var writer = new StreamWriter(dir, false, Encoding.UTF8))
            {
                writer.WriteLine("datetime,handleMin,handleMax,gripForce,grossKnobMin,grossKnobMax,fineKnobMin,fineKnobMax,keyKnobMin,KeyKnobMax,tripodMin,tripodMax");
            }
        }
        ROM values = new ROM(AppData.handleAngleMin, AppData.handleAngleMax,AppData.handleGripForce,AppData.grossKnobMin,AppData.grossKnobMax,AppData.fineKnobMin,AppData.fineKnobMax,AppData.keyKnobMin,
            AppData.keyKnobMax,AppData.graspMin,AppData.graspMax,true);

        //End the sessionfile, not sure whether it will be working or not
        //EndCurrentGameSession();
        SceneManager.LoadScene(homeScene);
    }

    public void BackToBaseCanvas()
    {
        HandGripCanvas.SetActive(false);
        HandGripForceCanvas.SetActive(false);
        GrossKnobCanvas.SetActive(false);
        FineKnobCanvas.SetActive(false);
        KeyKnobCanvas.SetActive(false);
        TripodCanvas.SetActive(false);
        BaseCanvas.SetActive(true);
    }

    public void Next1()
    {
        HandGripCanvas.SetActive(false);
        HandGripForceCanvas.SetActive(true);
    }

    public void Next2()
    {
      HandGripForceCanvas.SetActive(false);
      GrossKnobCanvas.SetActive(true);
    }

    public void Next3()
    {
        GrossKnobCanvas.SetActive(false);
        FineKnobCanvas.SetActive(true);
    }

    public void Next4()
    {
        FineKnobCanvas.SetActive(false);
        KeyKnobCanvas.SetActive(true);
    }

    public void Next5()
    {
        KeyKnobCanvas.SetActive(false);
        TripodCanvas.SetActive(true);
    }

    
}
