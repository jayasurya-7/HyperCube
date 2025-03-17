using UnityEngine;
using System;
using System.IO;

public class gamelogSS : MonoBehaviour
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
        if (gameData.isGameLogging)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            //Target = GameObject.FindGameObjectWithTag("Target");
            //Enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (gameData.game == "COMPENSATION")
            {
                gameData.playerPos = Player.transform.eulerAngles.z.ToString();
                //gameData.TargetPos = Target.transform.eulerAngles.z.ToString();
                //gameData.enemyPos = Enemy.transform.eulerAngles.z.ToString();

            }
            else
            {
                if (Player != null)
                    gameData.playerPos = Player.transform.position.x.ToString();

                else
                    gameData.playerPos = x.ToString("F2");
                
            }
            gameData.LogDataHT();
        }
        time += Time.deltaTime;
    }

    public void OnDestroy()
    {
        gameData.StopLogging();
    }
}

