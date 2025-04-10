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
    public GameObject Instruction5;
    public GameObject Instruction6;
    public TMPro.TMP_Text HandleMaxAngle;
    public TMPro.TMP_Text HandleMinAngle;
    public GameObject Grip;
    public GameObject Force;
    public GameObject menuButton1;
    public GameObject menuButton2;
    public Image Banner;
    public Image ForceBanner;
    public GameObject forceIns1;
    public GameObject forceIns2;
    public GameObject forceIns3;
    public GameObject forceIns4;
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
    public GameObject GrossInstruction_6;
    public GameObject GrossInstruction_7;
    bool GrossClockwise = false;
    bool GrossAnticlockwise = false;
    bool GrossMaxAngSet = false;
    bool GrossMinAngSet = false;
    public GameObject Gross;
    public GameObject menuButton3;
    public Image grossBanner;

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
    public GameObject FineInstruction_6;
    public GameObject FineInstruction_7;
    bool FineClockwise = false;
    bool FineAnticlockwise = false;
    bool FineMaxAngSet = false;
    bool FineMinAngSet = false;
    public GameObject Fine;
    public GameObject menuButton4;
    public Image fineBanner;

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
    public GameObject KeyInstruction_6;
    public GameObject KeyInstruction_7;
    bool KeyClockwise = false;
    bool KeyAnticlockwise = false;
    bool KeyMaxAngSet = false;
    bool KeyMinAngSet = false;
    public GameObject Key;
    public GameObject menuButton5;
    public Image keyBanner;

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
    public GameObject GraspInstruction_6;
    public GameObject GraspInstruction_7;
    bool GraspMaxSet = false;
    bool GraspMinSet = false;
    bool GraspIn = false;
    bool GraspOut = false;
    public GameObject Grasp;
    public GameObject menuButton6;
    public Image tripodBanner;


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
    float handleAngleMin = 0f;
    float handleAngleMax = 0f;
    float handleGripForce = 0f;
    float grossKnobMin = 0f;
    float grossKnobMax = 0f;
    float fineKnobMax = 0f;
    float fineKnobMin =     0f;
    float keyKnobMin = 0f;
    float keyKnobMax = 0f;
    float graspMax = 0f;
    float graspMin = 0f;

    // Start is called before the first frame update
    void Start()
    {
        handleGrip = false;
        grossKnob = false;
        fineKnob = false;
        keyKnob = false;
        tripodgrasp = false;
        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        //ConnectToHypercube.Connect("COM10");
        AppData.rom = new ROM();

        //StartNewGameSession();
        //Debug.Log($"handle :{AppData.handleAngleMax}, handle  :{AppData.handleAngleMin}, grip :{AppData.handleGripForce},gross :{AppData.grossKnobMax},gross min:{AppData.grossKnobMin}, fine max:{AppData.fineKnobMax}, fine min :{AppData.fineKnobMin}," +
        //    $",key max : {AppData.keyKnobMax},key Min :{AppData.keyKnobMin}, grasp max :{AppData.graspMax},grasp Min :{AppData.graspMin}");

        Debug.Log($"handle :{AppData.rom.handleMax}, handle  :{AppData.rom.handleMin}, grip :{AppData.rom.gripForce},gross :{AppData.rom.grossKnobMax},gross min:{AppData.rom.grossKnobMin}, fine max:" +
            $"{AppData.rom.fineKnobMax}, fine min :{AppData.rom.fineKnobMin}," +
            $",key max : {AppData.rom.keyKnobMax},key Min :{AppData.rom.keyKnobMin}, grasp max :{AppData.rom.tripodMax},grasp Min :{AppData.rom.tripodMin}");


    }



    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = "ASSESSMENT",
            Assessment = 1 // Example assessment value, adjust as needed
        };

        SessionManager.Instance.StartGameSession(currentGameSession);
        Debug.Log($"Started new game session with session number: {currentGameSession.SessionNumber}");

        SetSessionDetails();
    }


    private void SetSessionDetails()
    {

        string device = "HYPERCUBE"; // Set the device name
        string assistMode = "Null"; // Set the assist mode
        string assistModeParameters = "Null"; // Set the assist mode parameters
        string deviceSetupLocation = "Null"; // Set the device setup location
        string gameParameter = "Null";
        string mech = AppData.selectedMechanism;
        SessionManager.Instance.SetDevice(device, currentGameSession);
        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);
        SessionManager.Instance.mechanism(mech, currentGameSession);

    }


    void EndCurrentGameSession()
    {
        if (currentGameSession != null)
        {
            string trialDataFileLocation = "Null";
            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (AppData.rom.datetime == null)
        {
            Grip.SetActive(false);
            menuButton1.SetActive(false);
        }

        if (BaseCanvas.gameObject.activeSelf)
        {
            Debug.Log(" Base Canvas");
        }
        else if (HandGripForceCanvas.gameObject.activeSelf)
        {
            HandleForce();
        }
        else if (HandGripCanvas.gameObject.activeSelf)
        {
            HandleAngleROM();
        }
        else if (GrossKnobCanvas.gameObject.activeSelf)
        {
            GrossKnob();
        }
        else if (KeyKnobCanvas.gameObject.activeSelf)
        {
            KeyKnob();
        }
        else if (FineKnobCanvas.gameObject.activeSelf)
        {
            FineKnob();
        }
        else if (TripodCanvas.gameObject.activeSelf)
        {
            TripodGrasp();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            Debug.Log($"outer enter : {enterCounter}+{enterCounter == 2}");

        }

    }


    public void HandleAngleROM()
    {
        HandleAngle.text = (Mathf.Round((JediSerialPayload.angle_1 - AppData.offset) * 10.0f) * 0.1f) + "Deg".ToString();

        if (!handleRom && HandGripCanvas.gameObject.activeSelf)
        {
            if (HandleAngSet == false)
            {
                RadialBar1.fillAmount = 0.00403f * (JediSerialPayload.angle_1 - AppData.offset);
            }

            if (HandleMax == false)
            {
                RadialBar2.fillAmount = 0.5f - (0.00403f * (JediSerialPayload.angle_1 - AppData.offset));
                //Banner.color = Color.yellow;
            }

            if (HandleMax == false && Input.GetKeyDown(KeyCode.Return))
            {
                PlayerPrefs.SetFloat("Handle Ang Max", (JediSerialPayload.angle_1 - AppData.offset));
                AppData.handleAngleMax = (JediSerialPayload.angle_1 - AppData.offset);
                HandleMax = true;
                Instruction3.SetActive(false);
                Instruction4.SetActive(true);
                Instruction5.SetActive(true);
                Banner.color = Color.yellow;
                HandleMaxAngle.text = "Max Angle: " + Mathf.Round(((PlayerPrefs.GetFloat("Handle Ang Max")) * 10.0f) * 0.1f).ToString();
            }

            if (HandleMin == false && Input.GetKeyDown(KeyCode.Space))
            {
                PlayerPrefs.SetFloat("Handle Ang Min", (JediSerialPayload.angle_1 - AppData.offset));
                AppData.handleAngleMin = (JediSerialPayload.angle_1 - AppData.offset);

                HandleMin = true;
                HandleAngSet = true;

                Instruction4.SetActive(false);

                HandleMinAngle.text = "Min Angle: " + Mathf.Round(((PlayerPrefs.GetFloat("Handle Ang Min")) * 10.0f) * 0.1f).ToString();
                handleRom = true;
                Banner.color = Color.green;
                Instruction6.SetActive(true);
                AppData.offsetRunOnce = false;
            }
        }
        
    }
    public void offsetHandle() 
    { 
        AppData.offset = float.Parse(JediSerialPayload.data[2].ToString());
    }
    public void HandleForce()
    {
        if (!handleGrip)
        {
            ForceBar.fillAmount = 0.05f * JediSerialPayload.totalForce;
            forceIns1.SetActive(false);
            forceIns2.SetActive(true);
            HandGripForce.text = (Mathf.Round((JediSerialPayload.totalForce) * 10.0f) * 0.1f) + "Kgs".ToString();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerPrefs.SetFloat("Grip force", JediSerialPayload.totalForce);
                AppData.handleGripForce = JediSerialPayload.totalForce;
                ForceAcknowledgement.text = "Threshold force set as  " + PlayerPrefs.GetFloat("Grip force") + "  Kgs".ToString();
                forceIns2.SetActive(false);
                forceIns3.SetActive(true);
                forceIns4.SetActive(true);
                ForceBanner.color = Color.green;
                handleGrip = true;
            }

        }
    }

    public void GrossKnob()
    {

        if (!grossKnob)
        {
            GrossAngle.text = (Mathf.Round((JediSerialPayload.angle_2) * 10.0f) * 0.1f).ToString();

            if (GrossClockwise == false && GrossAnticlockwise == false)
            {
                GrossCircleClock.fillAmount = 0.00277f * JediSerialPayload.angle_2;
            }

            if (GrossClockwise == false && Input.GetKeyDown(KeyCode.Return))
            {
                if (GrossMaxAngSet == false)
                {
                    PlayerPrefs.SetFloat("Knob Ang Max", JediSerialPayload.angle_2);
                    AppData.grossKnobMax = JediSerialPayload.angle_2;

                    GrossMaxAngle.text = "Max angle: " + PlayerPrefs.GetFloat("Knob Ang Max").ToString();
                    GrossInstruction_1.SetActive(false);
                    GrossInstruction_3.SetActive(false);
                    GrossInstruction_6.SetActive(true);
                    grossBanner.color = Color.yellow;
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
                    PlayerPrefs.SetFloat("Knob Ang Min", JediSerialPayload.angle_2);
                    AppData.grossKnobMin = JediSerialPayload.angle_2;

                    GrossMinAngle.text = "Min angle: " + PlayerPrefs.GetFloat("Knob Ang Min").ToString();
                    GrossClockwise = false;
                    GrossMinAngSet = true;
                    GrossInstruction_2.SetActive(false);
                    GrossInstruction_4.SetActive(false);
                    GrossInstruction_6.SetActive(true);
                   grossBanner.color = Color.green;
                    grossKnob = true;
                }
            }
        }
        if(GrossMaxAngSet && GrossMinAngSet) GrossInstruction_7.SetActive(true);
    }

    public void FineKnob()
    {
        if (!fineKnob)
        {
            FineAngle.text = (Mathf.Round((JediSerialPayload.angle_4) * 10.0f) * 0.1f).ToString();

            if (FineClockwise == false && FineAnticlockwise == false)
            {
                FineCircleClock.fillAmount = 0.00277f * JediSerialPayload.angle_4;
            }

            if (FineClockwise == false && Input.GetKeyDown(KeyCode.Return))
            {
                if (FineMaxAngSet == false)
                {
                    PlayerPrefs.SetFloat("Knob Fine Ang Max", JediSerialPayload.angle_4);
                    AppData.fineKnobMax = JediSerialPayload.angle_4;

                    FineMaxAngle.text = "Max angle: " + PlayerPrefs.GetFloat("Knob Fine Ang Max").ToString();
                    FineInstruction_1.SetActive(false);
                    FineInstruction_3.SetActive(false);
                    FineInstruction_6.SetActive(true);
                    fineBanner.color = Color.yellow;
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
                    PlayerPrefs.SetFloat("Knob Fine Ang Min", JediSerialPayload.angle_4);
                    AppData.fineKnobMin = JediSerialPayload.angle_4;
                    FineMinAngle.text = "Min angle: " + PlayerPrefs.GetFloat("Knob Fine Ang Min").ToString();
                    FineClockwise = false;
                    FineMinAngSet = true;
                    FineInstruction_2.SetActive(false);
                    FineInstruction_4.SetActive(false);
                    FineInstruction_6.SetActive(true);
                    fineBanner.color = Color.green;
                    fineKnob = false;
                }
            }
        }
        FineInstruction_7.SetActive(FineMaxAngSet && FineMinAngSet);
    }

    public void KeyKnob()
    {
        KeyAngle.text = (Mathf.Round((JediSerialPayload.angle_3) * 10.0f) * 0.1f).ToString();

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
                    PlayerPrefs.SetFloat("Knob Key Ang Max", JediSerialPayload.angle_3);
                    AppData.keyKnobMax = JediSerialPayload.angle_3;

                    KeyMaxAngle.text = "Max angle: " + PlayerPrefs.GetFloat("Knob Key Ang Max").ToString();
                    KeyInstruction_1.SetActive(false);
                    KeyInstruction_3.SetActive(false);
                    KeyInstruction_6.SetActive(true);
                    KeyClockwise = true;
                    KeyMaxAngSet = true;
                    keyBanner.color = Color.yellow;
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
                    PlayerPrefs.SetFloat("Knob Key Ang Min", JediSerialPayload.angle_3);
                    AppData.keyKnobMin = JediSerialPayload.angle_3;

                    KeyMinAngle.text = "Min angle: " + PlayerPrefs.GetFloat("Knob Key Ang Min").ToString();
                    KeyClockwise = false;
                    KeyMinAngSet = true;
                    KeyInstruction_2.SetActive(false);
                    KeyInstruction_4.SetActive(false);
                    KeyInstruction_6.SetActive(true);
                    keyBanner.color = Color.green;
                    keyKnob = true;
                }
            }
        }
        KeyInstruction_7.SetActive(KeyMaxAngSet && KeyMinAngSet);
    }

    public void TripodGrasp()
    {
        DistanceValues.text = (Mathf.Round((JediSerialPayload.avgBtwDistance) * 10.0f) * 0.1f).ToString();

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
                    PlayerPrefs.SetFloat("Dist Min", (Mathf.Round((JediSerialPayload.btwDistance) * 10.0f) * 0.1f));
                    AppData.graspMin = (Mathf.Round((JediSerialPayload.btwDistance) * 10.0f) * 0.1f);

                    MaxDistance.text = "Min distance: " + PlayerPrefs.GetFloat("Dist Min").ToString();
                    GraspInstruction_1.SetActive(false);
                    GraspInstruction_3.SetActive(false);
                    GraspInstruction_6.SetActive(true);
                    GraspIn = true;
                    GraspMaxSet = true;
                    tripodBanner.color = Color.yellow;
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
                    PlayerPrefs.SetFloat("Dist Max", (Mathf.Round((JediSerialPayload.btwDistance) * 10.0f) * 0.1f));
                    AppData.graspMax = (Mathf.Round((JediSerialPayload.btwDistance) * 10.0f) * 0.1f);

                    MinDistance.text = "Max distance: " + PlayerPrefs.GetFloat("Dist Max").ToString();
                    GraspInstruction_2.SetActive(false);
                    GraspInstruction_4.SetActive(false);
                    GraspInstruction_6.SetActive(true);
                    GraspIn = false;
                    GraspMinSet = true;

                    tripodgrasp = true;
                }
            }
        }
        GraspInstruction_7.SetActive(GraspMaxSet && GraspMinSet);
        if (GraspMaxSet && GraspMinSet) { 
            tripodBanner.color = Color.green;
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
            //forceIns1.SetActive(true);
            forceIns3.SetActive(false);
            forceIns4.SetActive(false);
            ForceBanner.color = Color.red;
            Debug.Log(" grip Canvas 2 is active");
        }
        else if (HandGripCanvas.gameObject.activeSelf)
        {
            // Hand Grip ROM
            HandleMax = false;
            HandleMin = false;
            HandleAngSet = false;
            Instruction3.SetActive(true);
            Instruction4.SetActive(false);
            Instruction5.SetActive(false);
            Instruction6.SetActive(false);
            Banner.color = Color.red;
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
            GrossInstruction_7.SetActive(false);
            GrossInstruction_7.SetActive(false);
            grossBanner.color = Color.red;
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
            KeyInstruction_7.SetActive(false);
            KeyInstruction_6.SetActive(false);
            keyBanner.color = Color.red;
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
            FineInstruction_7.SetActive(false);
            FineInstruction_6.SetActive(false);
            fineBanner.color = Color.red;    
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
            GraspInstruction_6.SetActive(false);
            GraspInstruction_7.SetActive(false);
            tripodBanner.color = Color.red;
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
      
        handleAngleMin = (AppData.handleAngleMin == 0) ? AppData.rom.handleMin : AppData.handleAngleMin;
        handleAngleMax = (AppData.handleAngleMax == 0) ? AppData.rom.handleMax : AppData.handleAngleMax;
        handleGripForce = (AppData.handleGripForce < 0.7) ? AppData.rom.gripForce : AppData.handleGripForce;
        grossKnobMin = (AppData.grossKnobMin >=AppData.rom.grossKnobMin) ? AppData.rom.grossKnobMin : AppData.grossKnobMin;
        grossKnobMax = (AppData.grossKnobMax <= AppData.rom.grossKnobMax) ? AppData.rom.grossKnobMax : AppData.grossKnobMax;
        fineKnobMax = (AppData.fineKnobMax <= AppData.rom.fineKnobMax) ? AppData.rom.fineKnobMax : AppData.fineKnobMax;
        fineKnobMin = (AppData.fineKnobMin >= AppData.rom.fineKnobMin) ? AppData.rom.fineKnobMin : AppData.fineKnobMin;
        keyKnobMin = (AppData.keyKnobMin >= AppData.rom.keyKnobMin) ? AppData.rom.keyKnobMin : AppData.keyKnobMin;
        keyKnobMax = (AppData.keyKnobMax <= AppData.rom.keyKnobMax) ? AppData.rom.keyKnobMax : AppData.keyKnobMax;
        graspMax =  AppData.graspMax;
        graspMin =  AppData.graspMin;
       
        string dir = Path.Combine(AppData.idPath, "rom.csv");
        if (!File.Exists(dir))
        {
            using (var writer = new StreamWriter(dir, false, Encoding.UTF8))
            {
                writer.WriteLine("datetime,handleMin,handleMax,gripForce,grossKnobMin,grossKnobMax,fineKnobMin,fineKnobMax,keyKnobMin,KeyKnobMax,tripodMin,tripodMax");
            }
        }
        ROM values = new ROM(handleAngleMin, handleAngleMax, handleGripForce,grossKnobMin,grossKnobMax,fineKnobMin,fineKnobMax,keyKnobMin,
            keyKnobMax,graspMin,graspMax,true);
        Debug.Log($"before handle :{AppData.rom.handleMax}, handle  :{AppData.rom.handleMin}, grip :{AppData.rom.gripForce},gross :{AppData.rom.grossKnobMax},gross min:{AppData.rom.grossKnobMin}, fine max:" +
           $"{AppData.rom.fineKnobMax}, fine min :{AppData.rom.fineKnobMin}," +
           $",key max : {AppData.rom.keyKnobMax},key Min :{AppData.rom.keyKnobMin}, grasp max :{AppData.rom.tripodMax},grasp Min :{AppData.rom.tripodMin}");
        //End the sessionfile, not sure whether it will be working or not
        //EndCurrentGameSession();

        Debug.Log($"after handle max:{handleAngleMax}, handle  :{handleAngleMin}, grip :{handleGripForce},gross max :{grossKnobMax},gross min:{grossKnobMin}, fine max:" +
           $"{fineKnobMax}, fine min :{fineKnobMin}," +
           $",key max : {keyKnobMax},key Min :{keyKnobMin}, grasp max :{graspMax},grasp Min :{graspMin}");
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
        if (AppData.rom.datetime == null)
        {
            Force.SetActive(false);
            menuButton2.SetActive(false);
        }
        HandGripForceCanvas.SetActive(true);
    }

    public void Next2()
    {
      HandGripForceCanvas.SetActive(false);
        if (AppData.rom.datetime == null)
        {
            Gross.SetActive(false);
            menuButton3.SetActive(false);
        }
        GrossKnobCanvas.SetActive(true);

    }

    public void Next3()
    {
        GrossKnobCanvas.SetActive(false);
        if (AppData.rom.datetime == null)
        {
            Fine.SetActive(false);
            menuButton4.SetActive(false);
        }
        FineKnobCanvas.SetActive(true);
    }

    public void Next4()
    {
        FineKnobCanvas.SetActive(false);
        if (AppData.rom.datetime == null)
        {
            Key.SetActive(false);
            menuButton5.SetActive(false);
        }
        KeyKnobCanvas.SetActive(true);
    }

    public void Next5()
    {
        KeyKnobCanvas.SetActive(false);
        if (AppData.rom.datetime == null)
        {
            menuButton6.SetActive(false);
        }
        TripodCanvas.SetActive(true);
    }

    
}
