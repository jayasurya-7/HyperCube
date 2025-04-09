using UnityEngine;
using System;
using System.IO;

public class gamelogSG : MonoBehaviour
{
    //public static GameLog instance;
    GameObject Player, Target, Enemy;
    public static string dateTime;
    public static string date;
    public static string sessionNum;

    string fileName;
    float time;
    bool runOnce = false;
    void Start()
    {
        ResetGameData();
        InitializeSessionDetails();
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
        if (gameData.isGameLogging)
        {
            if (!runOnce)
            {

                CreateLogFile();
                gameData.StartDataLog(fileName);
                runOnce = true;
            }
            Player = GameObject.FindGameObjectWithTag("Snake Head");
        
                if (Player != null)
                    gameData.playerPos = Player.transform.position.y.ToString();
                else
                    gameData.playerPos = "\"" + "XXX" ;


            gameData.LogDataPM();
            

        }
        time += Time.deltaTime;
    }

    public void OnDestroy()
    {
        gameData.StopLogging();
    }
}

