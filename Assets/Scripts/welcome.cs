using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System;

using System.Collections;
using NeuroRehabLibrary;
using System.IO.Ports;
using System.Linq;
using UnityEditor.Rendering;
using static UnityEngine.Networking.UnityWebRequest;

public class welcome : MonoBehaviour
{
    public InputField hospno;
    public Dropdown ComPortDropdown;
    private SerialPort serialPort;
    private string patientID;
    public GameObject connectionPanel, loginPanel;
    public Text messageText;

    // scene names
    private string registerScene = "Register";
    private string mechScene = "Home";
    private string assessmentScene = "ROM Assessment";

    void Start()
    {
        messageText.text = " ";
        PopulateComPorts();
    }
    public void ConnectToHypercubex()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }

        string selectedPort = ComPortDropdown.options[ComPortDropdown.value].text;

        if (selectedPort != "No Ports Found")
        {
            JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
            ConnectToHypercube.Connect(selectedPort);
        }
    }
    void PopulateComPorts()
    {
        string[] ports = SerialPort.GetPortNames();
        ComPortDropdown.ClearOptions();

        if (ports.Length > 0)
        {
            ComPortDropdown.AddOptions(ports.ToList());
            ComPortDropdown.value = 0;
            ComPortDropdown.RefreshShownValue();
        }
        else
        {
            ComPortDropdown.AddOptions(new List<string> { "No Ports Found" });
            Debug.Log("No ports found");
        }
        Debug.Log("No ports found 2");
    }

    void Update()
    {
        if (AppData.HyperCubeConnected) { 
            connectionPanel.SetActive(false);
            loginPanel.SetActive(true);
        }
    }

    public void signup()
    {
        SceneManager.LoadScene(registerScene);
    }

    public void onCLickQuit()
    {
        Application.Quit();
    }


    private void assessment()
    {
        string date = AppData.rom.datetime;

        if (!string.IsNullOrEmpty(date))
        {
            DateTime oldDate;
            if (DateTime.TryParseExact(date, "dd-MM-yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out oldDate))
            {
                DateTime currentDate = DateTime.Now;
                TimeSpan timeDifference = currentDate - oldDate;

                if (timeDifference.TotalDays >= 7)
                {
                    SceneManager.LoadScene(assessmentScene);
                }
                else
                {
                    SceneManager.LoadScene(mechScene);
                }
            }
            else
            {
                Debug.LogError($"Invalid date format: {date}. Expected format: 'dd-MM-yyyy HH:mm:ss'.");
            }
        }
        else
        {
            Debug.LogError("Date is null or empty.");
        }
    }

    public void login()
    {
        patientID = hospno.text;
        bool hospno_check = string.IsNullOrEmpty(patientID);
        AppData.hospno = patientID;

        if (AppData.rom != null) AppData.rom = null;

        if (hospno_check == true)
        {
            messageText.text = "Empty Patient ID";
        }
        else
        {
            string patientDir = Path.Combine(Application.dataPath, "Patient_Data",patientID);
            AppData.idPath= patientDir;
            if (Directory.Exists(patientDir))
            {

                string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                string newDirPath = Path.Combine(patientDir, dateTimeNow);
                if (Directory.Exists(newDirPath))
                {
                    AppData.rawDataPath = newDirPath;
                }
                else
                {
                    Directory.CreateDirectory(newDirPath);
                    AppData.rawDataPath = newDirPath;
                    Debug.Log(newDirPath);
                }

                string baseDirectory = patientDir;
                SessionManager.Initialize(baseDirectory);
                SessionManager.Instance.Login();
                Debug.Log($"Base Directory :{baseDirectory}");
                AppData.rom = new ROM();

                if (AppData.rom.datetime == null) SceneManager.LoadScene(assessmentScene);
                assessment();

            }
            else
            {
                Debug.Log("Hospital Number Does not exist");
                messageText.text = " Hosptial Number doesn't exist";
                SceneManager.LoadScene(registerScene);
            }
        }
    }
}









