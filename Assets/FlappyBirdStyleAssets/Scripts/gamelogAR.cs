using UnityEngine;
using System;
using System.IO;

public class gamelogAR : MonoBehaviour
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
        sessionNum = "Session_" + AppData.currentSessionNumber;
    }

    private void CreateLogFile()
    {
        //string dir = Path.Combine(AppData.rawDataPath, sessionNum);
        //Directory.CreateDirectory(dir);

        string dir = AppData.gameDataPath;

        fileName = Path.Combine(dir, $"{AppData.selectedGame}_{dateTime}.csv");
        AppData.trialDataFileLocationTemp = fileName;

        File.Create(fileName).Dispose();
    }
    void Update()
    {
        Debug.Log($" logging {gameData.isGameLogging}");
        if (gameData.isGameLogging)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Target = GameObject.FindGameObjectWithTag("Target");
            //Enemy = GameObject.FindGameObjectWithTag("Enemy");
                if (Player != null)
                    gameData.playerPos = Player.transform.position.y.ToString();
                else
                    gameData.playerPos = x.ToString("F2");

                if (Target != null)
                    gameData.TargetPos = "\"" + Target.transform.position.x.ToString() + "|" + Target.transform.position.y.ToString() + "\"";
                else
                    gameData.TargetPos = x.ToString("F2");
                if (Enemy != null)
                    gameData.enemyPos = Enemy.transform.position.y.ToString();
                else
                    gameData.enemyPos = x.ToString("F2");
            

            gameData.LogDataAR();
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

