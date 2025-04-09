using UnityEngine;
using System;
using System.IO;

public class gamelogHR : MonoBehaviour
{
    //public static GameLog instance;
    GameObject Player, Target, Enemy;
    public static string dateTime;
    public static string date;
    public static string sessionNum;

    string fileName;
    float time;
    int x = 0;
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

            Player = GameObject.FindGameObjectWithTag("Player");
            Target = GameObject.FindGameObjectWithTag("Target");
            Enemy = GameObject.FindGameObjectWithTag("Enemy");

           if (Player != null)
                    gameData.playerPos = Player.transform.position.y.ToString("F3");
                else
                    gameData.playerPos = x.ToString("F2"); ;

                if (Target != null)
                    gameData.TargetPos= Target.transform.position.x.ToString("F3");
                else
                    gameData.TargetPos = x.ToString("F2") ;
                if (Enemy != null)
                    gameData.enemyPos = Enemy.transform.position.y.ToString("F3");
                else
                    gameData.enemyPos = x.ToString("F2");
            

            gameData.LogDataAR();
        }
        time += Time.deltaTime;
    }

    public void OnDestroy()
    {
        gameData.StopLogging();
    }
}

