 using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using Random = UnityEngine.Random;
using NeuroRehabLibrary;


public class GameManager_Car2D : MonoBehaviour {

	public static GameManager_Car2D instance;
    float startGameSpeed = 1.0f;
    public float gameSpeed = 1.0f;

    public float scrollspeed;

	int prev_target = 0;
	int target_pos;
	int maxAttempts = 5;
    float Targetspawn;

	int highGameScore = 0;
    int lastHighGameScore = 0;
	int lastGameScore = 0;
	int bonusGameCount = 0;


    //public float spawnTime=30f;
    //public float spawnSpeed = 1.0f;
    public float spawnTime;
    public float spawnSpeed;
    
    public bool isGameOver = true;
	public bool isGamePaused = false;
    public bool isNextLevel = false;

    bool endValSet = false;

    int gameScore = 0;
    int win = 0;


    public bool isPlaying = false;
    private bool NightMode = false;
    private bool startLevel = true;

    public float timeleft;
    float halftime;
    float duration = 90;

    [Header("Spawn Time Management")]
    public float startSpawnSpeed = 2.0f;
    public float spawnStep = 0.05f;
    public float minSpawSpeed = 0.48f;
   

	[Header("Spawn Objects")]
	public RectTransform spawnLine;
    //public float[] spawnObjectsXPos = new float[4] {-2.25f, -0.75f, 0.75f, 2.25f};
	public float[] spawnObjectsXPos = new float[4] { 0f, 0f, 0f, 0f };
	public GameObject[] spawnCornerGameObjects;
    public GameObject[] spawnCornerGameObjectsBlue;
    public GameObject[] spawnCornerGameObjectsBrown;
    public GameObject[] spawnCornerGameObjectsSilver;
    private GameObject spawnCornerObject;


	public GameObject[] spawnCenterGameObjects;
    public GameObject[] SpawnCenterObjectsBlue;
    public GameObject[] SpawnCenterObjectsBrown;
    public GameObject[] SpawnCenterObjectsSilver;
    public GameObject[] SpawnCenterObjectsBlack;
    private GameObject spawnCenterObject;
    public GameObject targetlane;

    public GameObject Column;

    private GameSession currentGameSession;

    public GameObject[] SpawnPowerGameObjects;
    private GameObject SpawnPowerObject;

    
    [Header("Sprites and background")]

    public Sprite[] playersprites;
    public GameObject[] backgrounds;
    GameObject currentsprite;
    GameObject currentbackground;
    int startBackground = 0;

    [Header("Sounds")]
    public AudioClip buttonClick;

    [Header("Menus")]
    public GameObject menuCanvas;
    public GameObject gameCanvas;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public GameObject nextlevelCanvas;
    public GameObject LevelUpCanvas;

    [Header("StartSign")]
    public GameObject StartSign;

    GameObject newColumn;


    public Camera cam;
    void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	void Start () {
        spawnTime = PlayerPrefs.GetFloat("SpawnTime");
        spawnSpeed = PlayerPrefs.GetFloat("SpawnSpeed");
        isPlaying = false;
		Time.timeScale = startGameSpeed;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        startLevel = true;
        ShowGameMenu();
		target_pos = UnityEngine.Random.Range(0, 4);
		prev_target = target_pos;

        halftime = duration / 2;
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 UpperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = cam.ScreenToWorldPoint(UpperCorner);
        CAR_spawnTargets1.instance.playSize = targetWidth.x * 0.8f;
        //AppData.timeOnTrail = timeleft;
        //AppData.reps = 0;

      

    }

    void Update() {
        InputPressed();
        
        GameManager_Car2D.instance.scrollspeed = -2f;//-hyper1.instance.force_total;//forward movement using loadcell values.
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G is pressed.change the sprite");
            currentsprite = GameObject.FindWithTag("Player");
            currentsprite.GetComponent<SpriteRenderer>().sprite = playersprites[UnityEngine.Random.Range(0,playersprites.Length)];
        }

        if ((!isGameOver) && (!isGamePaused))
        {
               isPlaying = true;
                timeleft -= Time.deltaTime;
                //AppData.sessionDuration += Time.deltaTime;
        }
        if((int)timeleft==duration/2 && !NightMode)
        {
            //Debug.Log("Hiiiiii");
            ChangeNextLevel();
        }
        if (timeleft < 0 ) //&& !endfValSet)
        {
            //AppData.plutoData.desTorq = 0;
            //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
            //CAR_spawnTargets.instance.setZero();
            timeleft = 0;

           if (!isGameOver)
            {
                win = 1;
                Debug.Log("win+1");
            }
            else
            {
                Debug.Log("win-1");
                win = -1;
            }

            Debug.Log(win);
            //AppData.plutoData.desTorq = 0;
            //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
            //AppData.DifficultyManager(win);
            //AppData.writeGamePerformace();
            //AppData.WriteTrainingSummaryFile(AppData.reps, AppData.timeOnTrail);
            //AppData.timeOnTrail = 0;
            //AppData.reps = 0;
           
            isGameOver = true;
            // AppData.CreateSessionFile();
            // vdc.StopCapture();
            //AppData.StopGameLogging();

            // write for aan profle
            //string _fname = Path.Combine(SubjectData.Get_Subj_Assessment_Dir(AppData.subjHospNum), "aan_" + AppData.plutoData.mechs[AppData.plutoData.mechIndex] + ".csv");

            //if ((AppData.regime == "MINIMAL ASSIST"))
            //{
            //using (StreamWriter file = new StreamWriter(_fname, true))
            //{
            //AppData.dateTime = DateTime.Now.ToString("Dyyyy-MM-ddTHH-mm-ss");
            //string res = String.Join(",", CAR_spawnTargets1.assistanceTorque);
            //Debug.Log(res);
            //file.WriteLine(AppData.dateTime + "," + AppData.pROM()[0].ToString() + ", " + AppData.pROM()[1].ToString() + ", " + "10" + "," + res.ToString() + "," + AppData.isflalccidControl);
            //}
            //}
            
            
            PlayerDied();
        }
        
        //UpdateDifficulty();
        // UpdateGameData();
    }

    public void InputPressed()
    {
        //if ((Input.GetKeyDown(KeyCode.P) || AppData.inputPressed()))
        if ((Input.GetKeyDown(KeyCode.P)))
        {
            if (!isGameOver)
            {
                if (isGamePaused)
                {
                    GameResume();
                }
                else
                {
                    GamePause();
                }
            }
            else
            {
                if(isGamePaused && !isNextLevel)
                {
                 GameRestart();
                }
                else if(isGamePaused&&isNextLevel)
                {
                    GameNextLevel();
                }
                else
                {
                 GameStart();
                }
                
            }
        }
    }

 
    #region-------Player settings------

    //private void UpdateDifficulty()
    //{
    //scrollspeed = -2 - AppData.startGameLevelSpeed * .1f;
    //}

    public void PlayerDied()
    {
        //AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);

        endValSet = true;
        if (win == 1)
        {
            CleanUpScene();
            if (currentbackground.name == "Background_grey")
            {
                ShowLevelUpMenu();
            }
            else
            {
                ShowNextLevelMenu();
                isNextLevel = true;
            }

        }
        if (win == -1)
        {
            CleanUpScene();
            GameOver();
        }
        isGameOver = true;
        isPlaying = false;
        isGamePaused = true;
    }

    public void PlayerReached()
    {
        if (isGameOver)
        {
            return;
        }
        Debug.Log("player Reached");
       // gameData.gameScore++;
       //AppData.reps += 1;
        //CAR_spawnTargets1.instance.reached = true;
        SpawnNewObject();
        CAR_spawnTargets1.instance.stopClock = CAR_spawnTargets1.instance.trailDuration;
    }

    void CleanUpScene()
    {
        for (int i = 0; i < spawnLine.childCount; i++)
            Destroy(spawnLine.GetChild(i).gameObject);
    }

    #endregion



    #region ----------SPAWN FUNCTIONS--------------
    private void Changemode(bool mode)
    {
        if (mode)
        {
            currentsprite = GameObject.FindWithTag("Player");
            currentsprite.transform.GetChild(1).gameObject.SetActive(true);
            currentbackground = GameObject.FindWithTag("Background");
            currentbackground.transform.GetChild(0).gameObject.SetActive(false);
            currentbackground.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            currentsprite = GameObject.FindWithTag("Player");
            currentsprite.transform.GetChild(1).gameObject.SetActive(false);
            currentbackground = GameObject.FindWithTag("Background");
            currentbackground.transform.GetChild(1).gameObject.SetActive(false);
            currentbackground.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void ChangeBackground(int count)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }
        backgrounds[count].SetActive(true);
        if (count == 0)
        {
            spawnCenterGameObjects = SpawnCenterObjectsBlue;
            spawnCornerGameObjects = spawnCornerGameObjectsBlue;
        }
        else if (count == 1)
        {
            spawnCenterGameObjects = SpawnCenterObjectsBrown;
            spawnCornerGameObjects = spawnCornerGameObjectsBrown;
        }
        else if (count == 2)
        {
            spawnCenterGameObjects = SpawnCenterObjectsSilver;
            spawnCornerGameObjects = spawnCornerGameObjectsSilver;
        }
    }

    void SpawnNewObject()
    {

        // Vector2 targetPos = new Vector2(0, 6f);

        // int targetPos = Random.Range(0, 6);
        Targetspawn = CAR_spawnTargets1.instance.TargetSpawn().x;
        Debug.Log("targetwnspawn" + Targetspawn);
        if (IsBetween(Targetspawn, -6, -4))
        {
            target_pos = 0;
        }
        else if (IsBetween(Targetspawn, -4, -2))
        {
            target_pos = 1;
        }

        else if (IsBetween(Targetspawn, -2, 0))
        {
            target_pos = 2;
        }

        else if (IsBetween(Targetspawn, 0, 2))
        {
            target_pos = 3;
        }

        else if (IsBetween(Targetspawn, 2, 4))
        {
            target_pos = 4;
        }
        else
        {
            target_pos = 5;
        }

        //Debug.Log("Target:-" + target_pos);
        prev_target = target_pos;
        Debug.Log(Targetspawn + "," + target_pos);

        if (target_pos == 1 || target_pos == 2 || target_pos == 3 || target_pos == 4)
        {
            spawnColumn();
            Vector3 spawnCornerObjectPos = new Vector3(0f, spawnLine.position.y, 0);
            spawnCornerObject = spawnCornerGameObjects[UnityEngine.Random.Range(0, spawnCornerGameObjects.Length)];
            GameObject newEnemy = (GameObject)(Instantiate(spawnCornerObject, spawnCornerObjectPos, Quaternion.identity));
            newEnemy.transform.SetParent(spawnLine);
            newEnemy.transform.SetAsFirstSibling();

            if (target_pos == 1)
            {
                SpawnObjects(-1f);
                SpawnObjects(1f);
                SpawnObjects(3f);
                SpawnTarget(-3f);
            }
            else if (target_pos == 2)
            {
                SpawnObjects(-3f);
                SpawnObjects(1f);
                SpawnObjects(3f);
                SpawnTarget(-1f);
            }
            else if (target_pos == 3)
            {
                SpawnObjects(-1f);
                SpawnObjects(-3f);
                SpawnObjects(3f);
                SpawnTarget(1f);
            }
            else
            {
                SpawnObjects(-1f);
                SpawnObjects(-3f);
                SpawnObjects(1f);
                SpawnTarget(3f);
            }

        }
        else if (target_pos == 0)
        {
            spawnColumn();
            SpawnObjects(-1f);
            SpawnObjects(-3f);
            SpawnObjects(3f);
            SpawnObjects(4.8f);
            SpawnObjects(1f);
            SpawnTarget(-4.8f);

        }
        else if (target_pos == 5)
        {
            spawnColumn();
            SpawnObjects(-1f);
            SpawnObjects(-4.8f);
            SpawnObjects(-3f);
            SpawnObjects(1f);
            SpawnObjects(3f);
            SpawnTarget(4.8f);
        }
        //CAR_spawnTargets1.instance.trailDuration = (PlayerController_Car2D.rigBody2D.transform.position.y - (newColumn.transform.position.y + 2f)) / scrollspeed;
        CAR_spawnTargets1.instance.trailDuration =Mathf.Clamp((PlayerController_Car2D.rigBody2D.transform.position.y - (newColumn.transform.position.y+1f)) / GameManager_Car2D.instance.scrollspeed,2,7);
        //Debug.Log("Duration updated"+ (PlayerController_Car2D.rigBody2D.transform.position.y - (newColumn.transform.position.y + 2f)) / scrollspeed);
    }
   


    public bool IsBetween(float testValue, float bound1, float bound2)
    {
        return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
    }

    private void SpawnObjects(float xpos_center)
    {
        Vector3 spawnCenterObjectPos = new Vector3(xpos_center, spawnLine.position.y, 0);
        spawnCenterObject = spawnCenterGameObjects[UnityEngine.Random.Range(0, spawnCenterGameObjects.Length)];
        GameObject newEnemy1 = (GameObject)(Instantiate(spawnCenterObject, spawnCenterObjectPos, Quaternion.identity));
        newEnemy1.transform.SetParent(spawnLine);
        newEnemy1.transform.SetAsFirstSibling();
    }

    private void SpawnTarget(float targetx)
    {
        Vector3 spawntargetpos = new Vector3(targetx, spawnLine.position.y, 0);
        GameObject mytarget = (GameObject)(Instantiate(targetlane, spawntargetpos, Quaternion.identity));
        mytarget.transform.SetParent(spawnLine);
        mytarget.transform.SetAsFirstSibling();
    }


    private void SpawnFirstObject(float xpos_center)
    {
        Vector3 spawnPowerObjectPos = new Vector3(xpos_center, spawnLine.position.y - 8f, 0);
        SpawnPowerObject = SpawnPowerGameObjects[UnityEngine.Random.Range(0, SpawnPowerGameObjects.Length)];
        GameObject newEnemy1 = (GameObject)(Instantiate(SpawnPowerObject, spawnPowerObjectPos, Quaternion.identity));
        newEnemy1.transform.SetParent(spawnLine);
        newEnemy1.transform.SetAsFirstSibling();
    }


    private void spawnColumn()
    {
        Vector3 Spawnposition = new Vector3(0, spawnLine.position.y + 1f, 0);
        newColumn = (GameObject)(Instantiate(Column, Spawnposition, Quaternion.identity));
        newColumn.transform.SetParent(spawnLine);
    }

    private void spawnFirstColumn()
    {
        Vector3 Spawnposition = new Vector3(0, spawnLine.position.y - 6f, 0);
        newColumn = (GameObject)(Instantiate(Column, Spawnposition, Quaternion.identity));
        newColumn.transform.SetParent(spawnLine);

    }

    #endregion

    #region --------------- MENUS AND GAME CONTROL --------------- 
    public void ShowGameMenu()
    {
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        nextlevelCanvas.SetActive(false);
        LevelUpCanvas.SetActive(false);
    }

    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = "Highway Racer",
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
            string trialDataFileLocation = AppData.trialDataFileLocationTemp;
            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }


    public void ShowGamePlayMenu()
    {
        //Debug.Log("comes inside");
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        nextlevelCanvas.SetActive(false);
        LevelUpCanvas.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        nextlevelCanvas.SetActive(false);
        LevelUpCanvas.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        gameOverCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        nextlevelCanvas.SetActive(false);
        LevelUpCanvas.SetActive(false);
    }

    public void ShowNextLevelMenu()
    {
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        nextlevelCanvas.SetActive(true);
        LevelUpCanvas.SetActive(false);
    }

    public void ShowLevelUpMenu()
    {
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        nextlevelCanvas.SetActive(false);
        LevelUpCanvas.SetActive(true);
    }
    #endregion
    public void GameMenu()
    {
        Time.timeScale = 0;
        ButtonSound();
        ShowGameMenu();
    }

    public void GameStart()
    {
        StartCoroutine("GameStarted");
        ButtonSound();
        if (isGamePaused)
            GameResume();
        endValSet = false;
        isGameOver = false;
        //gameScore = 0;
        ResetTime();
        StartNewGameSession();
       // SoundManager_Car2D.instance.SetSoundOnOff(false);
        //RequestInterstial();
        //DisplayInterstial();


        CleanUpScene();
        //ChangeNextLevel();\
        if(startLevel)
        {
            Debug.Log("startLevel true");
            ChangeBackground(startBackground);
            startBackground++;
            startLevel = false;
        }

        //UpdateGameData();

        ShowGamePlayMenu();
        //PlayerReached();
        spawnFirstColumn();
        SpawnFirstObject(-5f);
        SpawnFirstObject(5f);
        PlayerController_Car2D.instance.currentLife = 0;
        Time.timeScale = startGameSpeed;
    }

    public void GameRestart()
    {
        ButtonSound();
            endValSet = false;
            isGameOver = false;
        gameData.isGameLogging = false;
        EndCurrentGameSession();
        gameData.StopLogging();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameStart();
       

        // GameStart();
    }

    public void GamePause()
    {
        ButtonSound();
        isGamePaused = true;
        Time.timeScale = 0;
        gameData.isGameLogging = false;
        ShowPauseMenu();
    }

    public void GameResume()
    {
        ButtonSound();
        isGamePaused = false;
        Time.timeScale = startGameSpeed;
        ShowGamePlayMenu();
        gameData.isGameLogging = true;
    }

    public void GameStop()
    {
        ButtonSound();
        GameOver();
       
    }

    public void GameOver()
    {
        //AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
        //SoundManager_Car2D.instance.SetSoundOnOff(true);
        CleanUpScene();
        EndCurrentGameSession();
        endValSet = true;
        isGameOver = true;
        isPlaying = false;
        ShowGameOverMenu();
        Time.timeScale = 0;
        hyper1.instance.Stop_data_log();
        gameData.isGameLogging = false;

    }

    public void GameQuit()
    {
        EndCurrentGameSession();
         ButtonSound();
        hyper1.instance.Stop_data_log();

    }

    public void GameQuitNow()
    {
        ButtonSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                Application.OpenURL(gameOverURL);
#else
                Application.Quit();
#endif
    }

    public void GameNextLevel()
    {
        //ButtonSound();
        ChangeNextLevel();
        isGamePaused = false;
        isNextLevel = false;
        //isPlaying = false;
       // CAR_spawnTargets.instance.setZero();
        GameStart();
        //ResetTime();
        //CleanUpScene();
        //StartCoroutine("GameStarted");
        //ShowGamePlayMenu();

    }

    public void ChangeNextLevel()
    {
        NightMode = !NightMode;
        Changemode(NightMode);
        if (!NightMode)
        {
            ChangeBackground(startBackground);
            startBackground++;
            startBackground %= 3;
        }
        //AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);

    }

    private void ResetTime()
    {
        spawnSpeed = startSpawnSpeed;
        spawnTime = spawnSpeed;
        timeleft = 90;
    }


    void ButtonSound()
    {
        if (buttonClick != null)
            SoundManager_Car2D.instance.PlaySound(buttonClick);
    }


    IEnumerator GameStarted()
    {
        for(int i=0;i<4;i++)
        {
            StartSign.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            StartSign.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

    }


}
