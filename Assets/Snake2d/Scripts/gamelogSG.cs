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
            Player = GameObject.FindGameObjectWithTag("Snake Head");
            //Target = GameObject.FindGameObjectWithTag("Target");
            //Enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (gameData.game == "COMPENSATION")
            {
                gameData.playerPos = Player.transform.eulerAngles.z.ToString();
                gameData.TargetPos = Target.transform.eulerAngles.z.ToString();
                gameData.enemyPos = Enemy.transform.eulerAngles.z.ToString();
                Debug.Log($" x Player Position :{gameData.playerPos}");

            }
            else
            {
                if (Player != null)
                    gameData.playerPos = Player.transform.position.y.ToString();
                else
                    gameData.playerPos = "\"" + "XXX" ;


                Debug.Log($" Player Position :{gameData.playerPos}");

                //if (Target != null)
                //    gameData.TargetPos = "\"" + Target.transform.position.x.ToString() + "," + Target.transform.position.y.ToString() + "\"";
                //else
                //    gameData.TargetPos = "\"" + "XXX" + "," + "XXX" + "\"";
                //if (Enemy != null)
                //    gameData.enemyPos = Enemy.transform.position.y.ToString();
                //else
                //    gameData.enemyPos = "\"" + "XXX" + "," + "XXX" + "\"";
            }
            gameData.LogDataHT();
            Debug.Log($" y Player Position :{gameData.playerPos}");

        }
        time += Time.deltaTime;
        Debug.Log($"Position :{gameData.isGameLogging}");
    }

    public void OnDestroy()
    {
        gameData.StopLogging();
    }
}

