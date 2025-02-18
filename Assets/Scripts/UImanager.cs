using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using NeuroRehabLibrary;

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
    public GameObject FineKnobCancas;
    public GameObject KeyKnobCanvas;
    public GameObject TripodCanvas;



    private GameSession currentGameSession;




    // Start is called before the first frame update
    void Start()
    {



        StartNewGameSession();

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
        string device = "HyperCube"; // Set the device name
        string assistMode = "Null"; // Set the assist mode
        string assistModeParameters = "Null"; // Set the assist mode parameters
        string deviceSetupLocation = "Null"; // Set the device setup location
        

        string gameParameter = "Null";

      

        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);


        SessionManager.Instance.SetDevice(device, currentGameSession);
        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);



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


    public void HandleAngleROM()
    {
        if (HandleAngSet == false)
        {
            RadialBar1.fillAmount = 0.00403f * hyper1.instance.ang1;
        }
        HandleAngle.text = (Mathf.Round((hyper1.instance.ang1) * 10.0f) * 0.1f) + "Deg".ToString();

        if (HandleMax == false)
        {
            RadialBar2.fillAmount = 0.5f - (0.00403f * hyper1.instance.ang1);
        }
         
        if (HandleMax == false && Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetFloat("Handle Ang Max", hyper1.instance.ang1);
            HandleMax = true;
            Instruction3.SetActive(false);
            Instruction4.SetActive(true);
            HandleMaxAngle.text = "Max Angle: " + Mathf.Round(((PlayerPrefs.GetFloat("Handle Ang Max"))*10.0f)*0.1f).ToString();
        }

        if (HandleMin == false && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetFloat("Handle Ang Min", hyper1.instance.ang1);
            HandleMin = true;
            HandleAngSet = true;
            Instruction4.SetActive(false);
            HandleMinAngle.text = "Min Angle: " + Mathf.Round(((PlayerPrefs.GetFloat("Handle Ang Min"))*10.0f)*0.1f).ToString();
        }
    }
    
    public void HandleForce()
    {
        ForceBar.fillAmount = 0.05f * hyper1.instance.force_total;
        HandGripForce.text = (Mathf.Round((hyper1.instance.force_total) * 10.0f) * 0.1f) + "Kgs".ToString();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetFloat("Grip force", hyper1.instance.force_total);
            ForceAcknowledgement.text = "Threshold force set as  " + PlayerPrefs.GetFloat("Grip force") + "  Kgs".ToString();
                       
        }
        
    }

    public void GrossKnob()
    {

        GrossAngle.text = (Mathf.Round((hyper1.instance.ang2) * 10.0f) * 0.1f).ToString();
        
        if (GrossClockwise == false && GrossAnticlockwise == false)
        {
            GrossCircleClock.fillAmount = 0.00277f * hyper1.instance.ang2;
        }

        if (GrossClockwise == false && Input.GetKeyDown(KeyCode.Return))
        {
            if (GrossMaxAngSet == false)
            {
                PlayerPrefs.SetFloat("Knob Ang Max", hyper1.instance.ang2);
                GrossMaxAngle.text = "Max angle: " + PlayerPrefs.GetFloat("Knob Ang Max").ToString();
                GrossInstruction_1.SetActive(false);
                GrossInstruction_3.SetActive(false);
                GrossClockwise = true;
                GrossMaxAngSet = true;
            }
        }

        if (GrossClockwise == true )
        {
            GrossCircleAnticlock.fillAmount = -0.00277f * hyper1.instance.ang2;
            GrossInstruction_2.SetActive(true);
            GrossInstruction_4.SetActive(true);
            GrossAnticlockwise = true;          
        }

        if (GrossAnticlockwise == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (GrossMinAngSet == false)
            {
                PlayerPrefs.SetFloat("Knob Ang Min", hyper1.instance.ang2);
                GrossMinAngle.text = "Min angle: " + PlayerPrefs.GetFloat("Knob Ang Min").ToString();
                GrossClockwise = false;
                GrossMinAngSet = true;
                GrossInstruction_2.SetActive(false);
                GrossInstruction_4.SetActive(false);
                GrossInstruction_5.SetActive(true);
            }   
        }
    }

    public void FineKnob()
    {
        FineAngle.text = (Mathf.Round((hyper1.instance.ang4) * 10.0f) * 0.1f).ToString();

        if (FineClockwise == false && FineAnticlockwise == false)
        {
            FineCircleClock.fillAmount = 0.00277f * hyper1.instance.ang4;
        }

        if (FineClockwise == false && Input.GetKeyDown(KeyCode.Return))
        {
            if (FineMaxAngSet == false)
            {
                PlayerPrefs.SetFloat("Knob Fine Ang Max", hyper1.instance.ang4);
                FineMaxAngle.text = "Max angle: " + PlayerPrefs.GetFloat("Knob Fine Ang Max").ToString();
                FineInstruction_1.SetActive(false);
                FineInstruction_3.SetActive(false);
                FineClockwise = true;
                FineMaxAngSet = true;
            }
        }

        if (FineClockwise == true)
        {
            FineCircleAnticlock.fillAmount = -0.00277f * hyper1.instance.ang4;
            FineInstruction_2.SetActive(true);
            FineInstruction_4.SetActive(true);
            FineAnticlockwise = true;
        }

        if (FineAnticlockwise == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (FineMinAngSet == false)
            {
                PlayerPrefs.SetFloat("Knob Fine Ang Min", hyper1.instance.ang4);
                FineMinAngle.text = "Min angle: " + PlayerPrefs.GetFloat("Knob Fine Ang Min").ToString();
                FineClockwise = false;
                FineMinAngSet = true;
                FineInstruction_2.SetActive(false);
                FineInstruction_4.SetActive(false);
                FineInstruction_5.SetActive(true);
            }
        }
    }

    public void KeyKnob()
    {
        KeyAngle.text = (Mathf.Round((hyper1.instance.ang3) * 10.0f) * 0.1f).ToString();

        if (KeyClockwise == false && KeyAnticlockwise == false)
        {
            KeyCircleClock.fillAmount = 0.00277f * hyper1.instance.ang3;
        }

        if (KeyClockwise == false && Input.GetKeyDown(KeyCode.Return))
        {
            if (KeyMaxAngSet == false)
            {
                PlayerPrefs.SetFloat("Knob Key Ang Max", hyper1.instance.ang3);
                KeyMaxAngle.text = "Max angle: " + PlayerPrefs.GetFloat("Knob Key Ang Max").ToString();
                KeyInstruction_1.SetActive(false);
                KeyInstruction_3.SetActive(false);
                KeyClockwise = true;
                KeyMaxAngSet = true;
            }
        }

        if (KeyClockwise == true)
        {
            KeyCircleAnticlock.fillAmount = -0.00277f * hyper1.instance.ang3;
            KeyInstruction_2.SetActive(true);
            KeyInstruction_4.SetActive(true);
            KeyAnticlockwise = true;
        }

        if (KeyAnticlockwise == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (KeyMinAngSet == false)
            {
                PlayerPrefs.SetFloat("Knob Key Ang Min", hyper1.instance.ang3);
                KeyMinAngle.text = "Min angle: " + PlayerPrefs.GetFloat("Knob Key Ang Min").ToString();
                KeyClockwise = false;
                KeyMinAngSet = true;
                KeyInstruction_2.SetActive(false);
                KeyInstruction_4.SetActive(false);
                KeyInstruction_5.SetActive(true);
            }
        }
    }

    public void TripodGrasp()
    {
        DistanceValues.text = (Mathf.Round((hyper1.instance.Avg_Btw_dist) * 10.0f) * 0.1f).ToString();

        if (GraspIn == false && GraspOut == false)
        {
            HorizontalBar_1.fillAmount = 0.625f * ((hyper1.instance.Avg_Btw_dist) / 2.0f) - 1.0f;
            HorizontalBar_2.fillAmount = 0.625f * ((hyper1.instance.Avg_Btw_dist) / 2.0f) - 1.0f;
        }

        if (GraspIn == false && Input.GetKeyDown(KeyCode.Return))
        {
            if (GraspMaxSet == false)
            {
                PlayerPrefs.SetFloat("Dist Min", (Mathf.Round((hyper1.instance.Btw_dist) * 10.0f) * 0.1f));
                MaxDistance.text = "Min distance: " + PlayerPrefs.GetFloat("Dist Min").ToString();
                GraspInstruction_1.SetActive(false);
                GraspInstruction_3.SetActive(false);
                GraspIn = true;
                GraspMaxSet = true;
            }
        }

        if (GraspIn == true)
        {
            HorizontalBar_3.fillAmount = 0.625f * ((hyper1.instance.Avg_Btw_dist) / 2.0f) - 1.0f;
            HorizontalBar_4.fillAmount = 0.625f * ((hyper1.instance.Avg_Btw_dist) / 2.0f) - 1.0f;
            GraspInstruction_2.SetActive(true);
            GraspInstruction_4.SetActive(true);
            GraspOut = true;
        }

        if (GraspOut == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (GraspMinSet == false)
            {
                PlayerPrefs.SetFloat("Dist Max", (Mathf.Round((hyper1.instance.Btw_dist) * 10.0f) * 0.1f));
                MinDistance.text = "Max distance: " + PlayerPrefs.GetFloat("Dist Max").ToString();
                GraspInstruction_2.SetActive(false);
                GraspInstruction_4.SetActive(false);
                GraspInstruction_5.SetActive(true);
                GraspIn = false;
                GraspMinSet = true;
            }
        }

    }

    public void Reset()
    {   
        // Hand Grip ROM
        HandleMax = false;
        HandleMin = false;
        HandleAngSet = false;
        Instruction3.SetActive(true);
        
        //Gross Knob ROM
        GrossMinAngSet = false;
        GrossMaxAngSet = false;
        GrossAnticlockwise = false;
        GrossInstruction_1.SetActive(true);
        GrossInstruction_3.SetActive(true);
        GrossInstruction_5.SetActive(false);

        //Fine Knob ROM
        FineMinAngSet = false;
        FineMaxAngSet = false;
        FineAnticlockwise = false;
        FineInstruction_1.SetActive(true);
        FineInstruction_3.SetActive(true);
        FineInstruction_5.SetActive(false);

        //Key Knob ROM
        KeyMinAngSet = false;
        KeyMaxAngSet = false;
        KeyAnticlockwise = false;
        KeyInstruction_1.SetActive(true);
        KeyInstruction_3.SetActive(true);
        KeyInstruction_5.SetActive(false);

        //Grasp ROM
        GraspMaxSet = false;
        GraspMinSet = false;
        GraspOut = false;
        GraspInstruction_1.SetActive(true);
        GraspInstruction_3.SetActive(true);
        GraspInstruction_5.SetActive(false);
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
        FineKnobCancas.SetActive(true);
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
        EndCurrentGameSession();
        SceneManager.LoadScene("Home");
    }

    public void BackToBaseCanvas()
    {
        HandGripCanvas.SetActive(false);
        HandGripForceCanvas.SetActive(false);
        GrossKnobCanvas.SetActive(false);
        FineKnobCancas.SetActive(false);
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
        FineKnobCancas.SetActive(true);
    }

    public void Next4()
    {
        FineKnobCancas.SetActive(false);
        KeyKnobCanvas.SetActive(true);
    }

    public void Next5()
    {
        KeyKnobCanvas.SetActive(false);
        TripodCanvas.SetActive(true);
    }

    
}
