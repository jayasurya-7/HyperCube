using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using NeuroRehabLibrary;
using System;
public class Snake : MonoBehaviour
{
    Vector2 dir = Vector2.right;
    List<Transform> Tail = new List<Transform>();
    bool ate = false;
    bool gameOver = false;
    bool gamePause = false;
    public GameObject tailPrefab;
    public GameObject GameOverCanvas;
    public GameObject PauseCanvas;
    public AudioSource ScoreSound;
    public AudioSource GameOverSound;
    public AudioSource PauseSound;
    public AudioSource UnpauseSound;
    private GameSession currentGameSession;
    // Start is called before the first frame update
    void Start()
    {
        gameData.isGameLogging = true;
        InvokeRepeating("Move", 0.3f, 0.3f);
        Time.timeScale = 1;
        StartNewGameSession();
        //hyper1.instance.start_data_log();
       

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log($" isGameLogging :{ gameData.isGameLogging } ");
        MovementControl();
        if (gameOver == true)
        {
            Restart();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PauseCanvas.SetActive(true);
                PauseSound.Play();
                gameData.isGameLogging=false;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                PauseCanvas.SetActive(false);
                gameData.isGameLogging=true;
                gameData.events = Array.IndexOf(gameData.tukEvents, "moving");
                UnpauseSound.Play();
            }
        }
        
    }


    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = "Snake game",
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


    void Move ()
    {
        Vector2 v = transform.position;
        
        
        transform.Translate(dir);
        
        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            Tail.Insert(0, g.transform);
            ate = false;
        }
        
        
        else if (Tail.Count > 0)
        {
            Tail.Last().position = v;
            Tail.Insert(0, Tail.Last());
            Tail.RemoveAt(Tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "FoodPrefab")
        {
            ate = true;
            Destroy(coll.gameObject);
            //ScoreSound.Play();
            gameData.events = Array.IndexOf(gameData.tukEvents, "passed");

            ScoreManagerSnake.instance.AddScore();
        }

        else if (coll.gameObject.tag == "Obstacle")
        {
            Debug.Log("Game Over");
            gameOver = true;
            GameOver();
            gameData.events = Array.IndexOf(gameData.tukEvents, "collided");


        }

    }

    private void MovementControl()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //    dir = Vector2.right;
        //else if (Input.GetKey(KeyCode.DownArrow))
        //    dir = Vector2.down;
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //    dir = Vector2.left;
        //else if (Input.GetKey(KeyCode.UpArrow))
        //    dir = Vector2.up;

        if (JediSerialPayload.button_2 == 0)
        {
            dir = Vector2.right;
            Debug.Log("Right");
        }
        else if (JediSerialPayload.button_1 == 0)
        {
            dir = Vector2.down;
            Debug.Log("Down");
        }
        else if (JediSerialPayload.button_4 == 0)
            dir = Vector2.left;
        else if (JediSerialPayload.button_3 == 0)
            dir = Vector2.up;   
    }

    private void GameOver()
    {
        EndCurrentGameSession();
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        GameOverSound.Play();
        
    }
    public void exitButton()
    {
        EndCurrentGameSession();
        gameData.isGameLogging = false;
        SceneManager.LoadScene("Home");
    }

    public void Restart()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
