using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using NeuroRehabLibrary;
using System;

public class UIManagerPP : MonoBehaviour
{
	//public RockVR.Video.VideoCapture vdc;
	GameObject[] pauseObjects, finishObjects;
	public BoundController rightBound;
	public BoundController leftBound;
	public bool isFinished;
	public bool playerWon, enemyWon;
	public AudioClip[] audioClips; // winlevel loose
	public int winScore = 7;
	public int win;

	public GameObject dropdown;
	// Use this for initialization

	private float lastTimestamp = 0f, gameMoveTime =0f;

    private GameSession currentGameSession;



    void Start()
	{
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
		hideFinished();
        //vdc.customPath = false;
        //vdc.customPathFolder = "";
        //vdc.filePath = AppData.GameVideoLog(AppData.subjHospNum, AppData.plutoData.mechs[AppData.plutoData.mechIndex], AppData.game, AppData.regime);
        ////   Debug.Log(vdc.filePath);
        //vdc.StartCapture();
        StartNewGameSession();

    }




    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
           GameName = "Ping Pong",
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
            string trialDataFileLocation = AppData.trialDataFileLocationTemp;
            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }



    // Update is called once per frame
    void Update()
	{
        if (Time.timeScale > 0 && !isFinished)
        {
            float currentTime = Time.unscaledTime;
            gameMoveTime += currentTime - lastTimestamp;
            lastTimestamp = currentTime;
        }
        else
        {
            lastTimestamp = Time.unscaledTime; // Update timestamp even if paused or finished
        }

        //gameData.events = Array.IndexOf(gameData.pongEvents, "playerFail");
        if (rightBound.enemyScore >= winScore && !isFinished)
		{
			isFinished = true;
			enemyWon = true;
			Camera.main.GetComponent<AudioSource>().Stop();
			win = -1;
			isFinished = true;
			//Debug.Log(HatGameController.instance.score + "," + 0.8 * HT_spawnTargets1.instance.count);
/*			AppData.plutoData.desTorq = 0;
			AppData.DifficultyManager(-1);
			AppData.writeGamePerformace();

			AppData.StopGameLogging();
			AppData.WriteTrainingSummaryFile(AppData.reps, AppData.timeOnTrail);*/
			playAudio(1);
			playerWon = false;
/*			AppData.timeOnTrail = 0;
			AppData.reps = 0;*/
		}
		else if (leftBound.playerScore >= winScore && !isFinished
			)
		{
			Camera.main.GetComponent<AudioSource>().Stop();
/*			AppData.DifficultyManager(1);
			AppData.writeGamePerformace();*/

			//string data = AppData.startGamePerformace.ToString() + "," + AppData.startGameLevel.ToString() + "," + AppData.endGamePerformace.ToString() + "," + AppData.endGameLevel.ToString() + "," + AppData.gameLogFileName + "\n";
			// AppData.CreateGamePerformacelog(data);
/*
			AppData.StopGameLogging();
			AppData.WriteTrainingSummaryFile(AppData.reps, AppData.timeOnTrail);*/

			//playAudio(1);
			playAudio(0);
			isFinished = true;
			enemyWon = false;
			win = 1;
			playerWon = true;
/*			AppData.timeOnTrail = 0;
			AppData.reps = 0;*/
		}

		if (isFinished)
		{
			//vdc.StopCapture();
			showFinished();
	
			EndCurrentGameSession();
			gameData.isGameLogging = false;

            LoadScene("pong_menu");
			//h.Stop_data_log();
		}


		//uses the p button to pause and unpause the game
		if (( Input.GetKeyDown(KeyCode.P)) && !isFinished)
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
				gameData.isGameLogging = false;
				//ShowDropdown();
			}
			else if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
				hidePaused();
                gameData.isGameLogging = true;

                //hideDropdown();
            }
        }


		if (Time.timeScale == 0 && !isFinished)
		{
			//searches through pauseObjects for PauseText
			foreach (GameObject g in pauseObjects)
			{

				if (g.name == "PauseText")
					//makes PauseText to Active
					g.SetActive(true);
			}
		}
		else
		{
			//searches through pauseObjects for PauseText
			foreach (GameObject g in pauseObjects)
			{
				if (g.name == "PauseText")
					//makes PauseText to Inactive
					g.SetActive(false);
			}
		}
	}
	//Reloads the Level
	public void LoadScene(string sceneName)
	{
		//Application.LoadLevel(sceneName);
		EndCurrentGameSession();
		gameData.StopLogging();
		SceneManager.LoadScene(sceneName);
	}

	//Reloads the Level
	public void Reload()
	{
		//Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	void playAudio(int clipNumber)
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = audioClips[clipNumber];
		audio.Play();

	}
	//controls the pausing of the scene
	public void pauseControl()
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

	//shows objects with ShowOnPause tag
	public void showPaused()
	{
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

	//shows objects with ShowOnFinish tag
	public void showFinished()
	{


		foreach (GameObject g in finishObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnFinish tag
	public void hideFinished()
	{
		foreach (GameObject g in finishObjects)
		{
			g.SetActive(false);
		}
	}

    public void hideDropdown()
    {
		dropdown.SetActive(false);
    }

	public void ShowDropdown()
	{
		dropdown.SetActive(true);
	}


}
