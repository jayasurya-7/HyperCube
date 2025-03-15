using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime;
//using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using NeuroRehabLibrary;
using System.IO;

public class hyper1 : MonoBehaviour

{
    public static hyper1 instance;
    //JediSerialCom serReader;
    public float force_1;
    public float force_2;
    public float force_total;
    public float ang1;
    public float ang2;
    public float ang3;
    public float ang4;
    public int buttonPin1State;
    public int buttonPin2State;
    public int buttonPin3State;
    public int buttonPin4State;
    public int buttonPin5State;
    public int buttonPin6State;
    public int buttonPin7State;
    public float distance1;
    public float distance2;
    public float Btw_dist;
    public float Avg_Btw_dist;
    public float x;

    public static string Date;
    public static string name_sub_;
    public static string date_;
    // public static string Path;
    public static string filePath;
    public static string folderpath;
    public static float timer;
    public string[] portnames;
    public static string savepath;
    public InputField Hos_;

    public int MovingAverageLength = 5;
    private int count;
    private float movingAverage;
    private SerialPort serPort;
    public string[] Ports;

    public static float timer_;


    private GameSession currentGameSession;


    // Start is called before the first frame update
    void Start()
    {
        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        //serReader = new JediSerialCom("COM4");
        //serReader = new JediSerialCom(PlayerPrefs.GetString("COMPort"));
        //serReader.ConnectToArduino();
        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;
        //string[] ports = SerialPort.GetPortNames();
        //string[] Ports = SerialPort.GetPortNames();
        //foreach (string port in ports)
        //{
        //    Debug.Log(port);
        //}
        //Ports = ports;
    }

   
    // Update is called once per frame
    public void Update()
    {
        //Debug.Log(PlayerPrefs.GetString("Hospital Number"));
        timer_ += Time.deltaTime;
        //Debug.Log(timer_);
        //Debug.Log( "freqq = "+ (1 / Time.deltaTime));

        //if ((JediSerialPayload.data.Count == 15))
        //{
        //    //Debug.Log(JediSerialPayload.data[4].ToString());


        try
        {
            force_1 = (float.Parse(JediSerialPayload.data[0].ToString()));
           // Debug.Log($"force-1 :{force_1},data -{JediSerialPayload.force_1}");
            force_2 = (float.Parse(JediSerialPayload.data[1].ToString()));
            force_total = (force_1 + force_2);
            ang1 = float.Parse(JediSerialPayload.data[2].ToString());
            ang2 = float.Parse(JediSerialPayload.data[3].ToString());
            ang3 = float.Parse(JediSerialPayload.data[4].ToString());
            ang4 = float.Parse(JediSerialPayload.data[5].ToString());
            distance1 = float.Parse(JediSerialPayload.data[6].ToString());
            distance2 = float.Parse(JediSerialPayload.data[7].ToString());
            buttonPin1State = int.Parse(JediSerialPayload.data[8].ToString());
            buttonPin2State = int.Parse(JediSerialPayload.data[9].ToString());
            buttonPin3State = int.Parse(JediSerialPayload.data[10].ToString());
            buttonPin4State = int.Parse(JediSerialPayload.data[11].ToString());
            buttonPin5State = int.Parse(JediSerialPayload.data[12].ToString());
            buttonPin6State = int.Parse(JediSerialPayload.data[13].ToString());
            buttonPin7State = int.Parse(JediSerialPayload.data[14].ToString());
            //Btw_dist = Mathf.Round((11.2f - (distance1 + distance2)));
            //Btw_dist = Mathf.Round((11.2f - (distance1 + distance2))*10.0f)*0.1f;
            //Btw_dist = Average((11.2f - (distance1 + distance2)));
            //x = float.Parse(JediSerialPayload.data[15].ToString());
            Btw_dist = (11.2f - (distance1 + distance2));
            Avg_Btw_dist = MovingAverage(Btw_dist);
        }



        catch (System.Exception)
        {

        }
    }

    public static class sessionlocation
    {
        public static string datafile;
        public static string filename;
        public static string gameName;
    }
    public void start_data_log()
    {
        StartNewGameSession();
        Debug.Log("not created");
        //timerstart = true;
        //String name = name_subject.text;
        //string condition = condition_subject.text;
        //string trial_number = trial_no.text;
        //string Hosp_number = Hos_.text;
        string Hosp_number = AppData.hospno;
        string Game_name = PlayerPrefs.GetString("Game name");
        string Mech_name = PlayerPrefs.GetString("Mechanism");
        //string pth = "C:\\Users\\SRIRAMACHANDRAN\\Desktop\\HypercubeData\\";
        string pth = Path.Combine(AppData.rawDataPath, "RawData");

        if (!Directory.Exists(pth))
        {
            Directory.CreateDirectory(pth);
            Debug.Log(pth);
        }
        else
        {
            Debug.Log("not created");
        }
        string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy");

        string total_path = Game_name + '-' + Mech_name;
        //string pth = "D:\\HypercubeData\\";
        //string pth = "C:\\Users\\DELL\\Desktop\\Hypercube Data\\";
        //string pth = "C:\\Users\\Siva\\Desktop\\HypercubeGames23\\Data\\";
        //string pth = "C:\\Users\\aravi\\Desktop\\Hypercube data\\";
        //string total_path = pth + Hosp_number + "-" + Game_name + "-" + Mech_name;

        folderpath = total_path;
        //  Directory.CreateDirectory(total_path);

        sessionlocation.datafile = pth + Hosp_number;
        sessionlocation.filename = Game_name + "-" + Mech_name;
        sessionlocation.gameName = Game_name;
        //string baseDirectory = sessionlocation.datafile;
        //SessionManager.Initialize(baseDirectory);


        //change_color();

        string filename = Mech_name + "-" + Game_name + "-" + DateTime.UtcNow.ToLocalTime().ToString("yy-MM-dd-HH-mm-ss") +
             "-" + ".csv";
        string rawDataFile = Path.Combine(pth, filename);
        savepath = filename;
        PlayerPrefs.SetString("data", rawDataFile);
        //saver(total_path);
        AppData.dlogger = new DataLogger(rawDataFile, "");
        //  File.Create(rawDataFile).Dispose();

        if (!File.Exists(rawDataFile))
        {
            Debug.Log(rawDataFile);
            /*****header setting***/
            string clientHeader = $"\"TIME\",\"F1\",\"F2\",\"ang1\",\"ang2\",\"ang3\",\"ang4\",\"dist1\",\"dist2\",\"B1\",\"B2\",\"B3\",\"B4\",\"B5\",\"B6\",\"B7\",{Environment.NewLine}";



            File.WriteAllText(rawDataFile, clientHeader);


        }







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
            string trialDataFileLocation = savepath;
            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }





    public void Stop_data_log()
    {

        EndCurrentGameSession();
        AppData.dlogger.stopDataLog();
        Debug.Log("dataStopped");

    }

    public float Average(float[] arr)

    {
        float sum = 0;
        float average = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }
        average = sum / arr.Length;
        return average;

    }

    public float MovingAverage(float values)
    {
        count++;


        if (count > MovingAverageLength)
        {
            movingAverage = movingAverage + (values - movingAverage) / (MovingAverageLength + 1);
        }
        else
        {
            movingAverage += values;

            if (count == MovingAverageLength)
            {
                movingAverage = movingAverage / count;

            }
        }
        return movingAverage;
    }

    //public void ReconnectToArduino()
    //{
    //    //serReader.DisconnectArduino();
    //    //Debug.Log("Dis");
    //    JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
    //    //serReader = new JediSerialCom(PlayerPrefs.GetString("COMPort"));
    //    serReader = new JediSerialCom("COM11");
    //   serReader.ConnectToArduino();
    //    //Debug.Log("connected");
    //}
}









































