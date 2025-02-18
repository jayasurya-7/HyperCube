//using PlutoDataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using NeuroRehabLibrary;
using static hyper1;
public class FlappyGameControl : MonoBehaviour
{
    public hyper1 h;
    public AudioClip[] winClip;
    public AudioClip[] hitClip;
    public Text ScoreText;
    public ProgressBar timerObject;
    public static FlappyGameControl instance;
    //public RockVR.Video.VideoCapture vdc;
    public GameObject GameOverText;
    public bool gameOver = false;
    //public float scrollSpeed = -1.5f;
    public float scrollSpeed;
    private int score;
    public GameObject[] pauseObjects;
    public float gameduration = 90;
    //public float gameduration = PlayerPrefs.GetFloat("");
    public GameObject start;
    int win = 0;
    bool endValSet = false;

    public int startGameLevelSpeed=1;
    public int startGameLevelRom = 1;
    public float ypos;

    public GameObject menuCanvas;
    public GameObject Canvas;

    public BirdControl bc;
    private GameSession currentGameSession;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        //AppData.game = "FLAPPY BIRD";

    }


    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = -(PlayerPrefs.GetFloat("ScrollSpeed"));
        Time.timeScale = 1;
        timerObject.isOn = true;
        timerObject.enableSpecified = true;
        // AppData.timeOnTrail = 0;
        //AppData.reps = 0;
        //vdc.customPath = false;
        //vdc.customPathFolder = "";
        //vdc.filePath = AppData.GameVideoLog(AppData.subjHospNum, AppData.plutoData.mechs[AppData.plutoData.mechIndex], AppData.game, AppData.regime);
        //// Debug.Log(vdc.filePath);
        //vdc.StartCapture();
        // Time.timeScale = 0;
        ShowGameMenu();
        StartNewGameSession();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameDurationUI();
       
        //uses the p button to pause and unpause the game
        if ((Input.GetKeyDown(KeyCode.P)))
        {
            if (!gameOver)
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    showPaused();
                }
                else if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    hidePaused();
                }
            }
            else if (gameOver)
            {
                hidePaused();
                playAgain();
                h.Stop_data_log();
            }
        }
        

        if (!gameOver && Time.timeScale == 1)
        {
            //AppData.sessionDuration += Time.deltaTime;
            //AppData.timeOnTrail += Time.deltaTime;
            gameduration -= Time.deltaTime;
        }




    }

  
    void UpdateGameDurationUI()
    {
        timerObject.specifiedValue = Mathf.Clamp(100 * (90 - gameduration) / 90f, 0, 100); ;

    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
       // AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {

        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
    public void BirdDied()
    {
        //AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
        
        endValSet = true;
        if (win == 1)
        {
            GameOverText.GetComponent<Text>().text = "Great Work! \n You WON! :)";
            //switch (AppData.startGamePerformace)
            //{
               
            //    case 2:
            //        GameOverText.GetComponent<Text>().text = "Great Work! Level Increased! :) \n"
            //            + "Current Level :" + AppData.endGameLevelSpeed;

            //        break;

            //}
        }
        if (win == -1)
            GameOverText.GetComponent<Text>().text = "Try Again";
        GameOverText.SetActive(true);
        gameOver = true;
        h.Stop_data_log();
        EndCurrentGameSession();

    }
    public void BirdScored()
    {


        if (gameduration < 0 && !endValSet)
        {
            
            //AppData.plutoData.desTorq = 0;
            //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
            //FB_spawnTargets.instance.setZero();
            gameduration = 0;


            if (!gameOver)
            {
                win = 1;
            }
            else
            {
                win = -1;
            }
            gameOver = true;
            Debug.Log(win);
            //AppData.plutoData.desTorq = 0;
            //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
            //AppData.DifficultyManager(win);
            //AppData.writeGamePerformace();

         
            //AppData.WriteTrainingSummaryFile(AppData.reps, AppData.timeOnTrail);
            //// vdc.StopCapture();
            //AppData.StopGameLogging();

            //// write for aan profle
            //string _fname = Path.Combine(SubjectData.Get_Subj_Assessment_Dir(AppData.subjHospNum), "aan_" + AppData.plutoData.mechs[AppData.plutoData.mechIndex] + ".csv");

            //if ((AppData.regime == "MINIMAL ASSIST"))
            //{
            //    using (StreamWriter file = new StreamWriter(_fname, true))
            //    {
            //        //AppData.dateTime = DateTime.Now.ToString("Dyyyy-MM-ddTHH-mm-ss");
            //        string res = String.Join(",", FB_spawnTargets.assistanceTorque);
            //        Debug.Log(res);
            //        file.WriteLine(AppData.dateTime + ", " + AppData.pROM()[0].ToString() + ", " + AppData.pROM()[1].ToString() + ", " + "10" + "," + res.ToString() + "," + "");
            //    }
            //}


            // SendToRobot.ControlParam("wfe", ControlType.TORQUE, true, false);
            // SceneManager.LoadScene("GameMenu");
            score = 0;
            BirdDied();
            //AppData.timeOnTrail = 0;
            //AppData.reps = 0;
            //  HT_spawnTargets1.instance.count = 0;
            // GameOverObject.SetActive(true);

        }
        else
        {
            if (!bc.startBlinking )
            {
                int index = UnityEngine.Random.Range(0, winClip.Length);
                GetComponent<AudioSource>().clip = winClip[index];
                if (score != 0)
                {
                    GetComponent<AudioSource>().Play();
                }
                score += 1;
                //AppData.gameScore ++;
                //Debug.Log("Score");
                //AppData.reps += 1;
            }
            else
            {
                int index = UnityEngine.Random.Range(0, hitClip.Length);
                GetComponent<AudioSource>().clip = hitClip[index];
                GetComponent<AudioSource>().Play();

            }
            //FB_spawnTargets.instance.reached = true;

            ScoreText.text = "Score: " + score.ToString();
            //RandomAngle();
            FlappyColumnPool.instance.spawnColumn();

        }
    }

    public void RandomAngle()
    {
        ypos = UnityEngine.Random.Range(-3f, 5.5f);
    }

    public void playAgain()
    {
        if (gameOver == true)
        {
            //endValSet = false;
            //gameOver = false;
            //Time.timeScale = 1;
            //hidePaused();
           // gameduration = 20;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (!gameOver)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }

        }

    }
    public void PlayStart()
    {
        endValSet = false;
        start.SetActive(false);
        Time.timeScale = 1;
    }

    public void continueButton() {
          if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();

        }
    }

    public void ShowGameMenu()
    {
        
        menuCanvas.SetActive(true);
        Canvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Canvas.SetActive(true);
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = sessionlocation.filename,
            Assessment = 0 // Example assessment value, adjust as needed
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



        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);


        SessionManager.Instance.SetDevice(device, currentGameSession);
        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);



    }

    void EndCurrentGameSession()
    {
        if (currentGameSession != null)
        {
            string trialDataFileLocation = savepath;
            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }

}
