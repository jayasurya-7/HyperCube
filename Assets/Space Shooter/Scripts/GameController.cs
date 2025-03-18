using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NeuroRehabLibrary;
public class GameController : MonoBehaviour
{
	public GameObject[] asteroids;
	public hyper1 hyper;
	public Vector3 spawnValues;
	public int asteroidCount;
	public float startWait;
	public float spawnWait;
	
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text highScoreText;

	private int score;
	private bool gameOver;
	private bool restart;
	private bool Paused;
	public GameObject player;
	public GameObject GameOverCanvas;

	public GameObject MenuCanvas;
	public GameObject Canvas;
	public GameObject PauseCanvas;

    private GameSession currentGameSession;

    float countDownTimer;
	void Start()
	{
		spawnWait = PlayerPrefs.GetFloat("SpawnWait");
		//ShowGameMenu();
		hyper.start_data_log();
		//();
		score = 0;
		UpdateScore();
		
		gameOver = false;
		gameOverText.text = "";
		Paused = false;

		restart = false;
		restartText.text = "";
		if (!PlayerPrefs.HasKey("highScore")) PlayerPrefs.SetInt("highScore", 100);

		highScoreText.text = "High Score:" + PlayerPrefs.GetInt("highScore").ToString();
		StartCoroutine(SpawnWaves());
		StartNewGameSession();
		gameData.isGameLogging= true;
		
	}


	public void restartGame()
    {
		score = 0;
		UpdateScore();

		gameOver = false;
		gameOverText.text = "";

		restart = false;
		restartText.text = "";
		if (!PlayerPrefs.HasKey("highScore")) PlayerPrefs.SetInt("highScore", 100);
        gameData.isGameLogging = true;
        highScoreText.text = "High Score:" + PlayerPrefs.GetInt("highScore").ToString();
		StartCoroutine(SpawnWaves());
	}
	
	void Update()
	{
		PlayerPrefs.SetInt("highScore", 1750);
		gameData.events = Array.IndexOf(gameData.spaceEvents, "moving");
		if (gameOver)
		{
			gameData.isGameLogging = false;
            restart = true;
			restartText.text = "Press 'R' to Restart";
			//h.Stop_data_log();
			GameOverCanvas.SetActive(true);
		}
		countDownTimer += Time.deltaTime;
		asteroidCount += Mathf.RoundToInt(countDownTimer / 10); 
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				StartNewGameSession();
                //player.SetActive(true);
                //restartGame();
            }
		}

		if (Input.GetKeyDown(KeyCode.P))
        {
			if (!Paused)
			{
				PauseGame();
			}
            else
            {
				ContinueGame();
                gameData.events = Array.IndexOf(gameData.tukEvents, "moving");
            }
        }
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		while(!gameOver) {
			for (int i = 0; i < asteroidCount; i++) {
				GameObject asteroid = asteroids[UnityEngine.Random.Range(0, asteroids.Length)];

				Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate(asteroid, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);

			if (gameOver) {
				restart = true;
				restartText.text = "Press 'R' to Restart";
				break;
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString();
		gameData.gameScore= score;	
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		if(score> PlayerPrefs.GetInt("highScore"))
        {
			Debug.Log("New High Score");
			PlayerPrefs.SetInt("highScore", score);

		}
		gameOver = true;
		EndCurrentGameSession();
		Debug.Log(gameOver);
		gameOverText.text = "GAME OVER";
		hyper.Stop_data_log();
		//GameOverCanvas.SetActive(true);
	}

	public void ShowGameMenu()
	{

		MenuCanvas.SetActive(true);
		Canvas.SetActive(false);
		Time.timeScale = 0;
	}
    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = PlayerPrefs.GetString("Game name"),
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


    public void StartGame()
	{
		Canvas.SetActive(true);
		MenuCanvas.SetActive(false);
		Time.timeScale = 1;
	}

	public void PauseGame()
    {
		Paused = true;
		PauseCanvas.SetActive(true);
		Time.timeScale = 0;
		gameData.isGameLogging = false;
    }
	public void ContinueGame()
    {
		PauseCanvas.SetActive(false);
		Time.timeScale = 1;
		Paused = false;
		gameData.isGameLogging = true;

    }
	public void ExitSpaceShooter()
    {
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
		gameData.isGameLogging = false;
		EndCurrentGameSession();
		gameData.StopLogging();

    }
}
