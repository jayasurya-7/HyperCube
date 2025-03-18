using UnityEngine;
using System;
using System.IO;

public class gamelogPM : MonoBehaviour
{
    //public static GameLog instance;
    GameObject Player, Target, Enemy;
    public static string dateTime;
    public static string date;
    public static string sessionNum;

    string fileName;
    float time;
    int x = 0;
    void Start()
    {
        ResetGameData();
        InitializeSessionDetails();
        CreateLogFile();
        gameData.StartDataLog(fileName);
    }

    private void ResetGameData()
    {
        if (gameData.playerScore != 0 || gameData.enemyScore != 0)
        {
            gameData.playerScore = 0;
            gameData.enemyScore = 0;
            gameData.events = 0;
        }
    }

    private void InitializeSessionDetails()
    {
        dateTime = DateTime.Now.ToString("Dyyyy-MM-ddTHH-mm-ss");

    }

    private void CreateLogFile()
    {

        string dir = AppData.gameDataPath;

        fileName = Path.Combine(dir, $"{AppData.selectedGame}_{dateTime}.csv");
        AppData.trialDataFileLocationTemp = fileName;

        File.Create(fileName).Dispose();
    }
    void Update()
    {
        Debug.Log($" logg : {gameData.isGameLogging}");
        if (gameData.isGameLogging)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
           
                if (Player != null)
                    gameData.playerPos = Player.transform.position.y.ToString();
                else
                    gameData.playerPos = x.ToString("F2");

    
            gameData.LogDataPM();
        }
        time += Time.deltaTime;
    }

    public void exitButton()
    {
        gameData.StopLogging();

    }

    public void OnDestroy()
    {
        gameData.StopLogging();
    }
}

