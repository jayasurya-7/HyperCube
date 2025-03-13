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

public class welcome : MonoBehaviour
{
    public InputField hospno;
    public Dropdown ComPortDropdown;
    public InputField Patientname;
    private SerialPort serialPort;
  //  JediSerialCom serReader;
    public static string p_hospno;

    public static string p_patientname;
    public static string newDirPath;
    public static string finalpath;
    public static string patientDir;
    public static string gamedata;
    public static string sessionfile;
    public Text messageText;


    //private const string SessionFilePath = "session_count.txt";

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
            ComPortDropdown.AddOptions(new System.Collections.Generic.List<string> { "No Ports Found" });
            Debug.Log("No ports found");
        }
        Debug.Log("No ports found 2");
    }

    void Update()
    {
    }

    public void signup()
    {
        SceneManager.LoadScene("Register");
    }

    public void onCLickQuit()
    {
        Application.Quit();
    }




    public void login()
    {

        p_hospno = hospno.text;
        bool hospno_check = string.IsNullOrEmpty(p_hospno);
        if (hospno_check == true)
        {
            Debug.Log("Empty hospno");
            messageText.text = "Empty Patient ID";
        }
        else
        {
            string path_to_data = Application.dataPath;
            Debug.Log("path "+ path_to_data);
            if (!Directory.Exists(path_to_data + "\\" + p_hospno))
            {
                string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno;
                circleclass.circlePath = patientDir;
                if (Directory.Exists(patientDir))
                {

                    string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                    string newDirPath = Path.Combine(patientDir, dateTimeNow);
                    Debug.Log(newDirPath + "xxx");
                    if (Directory.Exists(newDirPath))
                    {
                        staticclass.FolderPath = newDirPath;
                        AppData.rawDataPath = newDirPath;
                    }
                    else
                    {
                        Directory.CreateDirectory(newDirPath);
                        staticclass.FolderPath = newDirPath;
                        AppData.rawDataPath = newDirPath;
                        Debug.Log(newDirPath);
                    }
                    string baseDirectory = patientDir;


                    SessionManager.Initialize(baseDirectory);

                    SessionManager.Instance.Login();
                    AppData.hospno = p_hospno;
                    SceneManager.LoadScene("Home");
                }
                else
                {
                    Debug.Log("Hospital Number Does not exist");
                    messageText.text = " Hosptial Number doesn't exist";
                    SceneManager.LoadScene("Register");

                    //StartCoroutine(ShowMessageFor3Seconds("PLEASE ENTER SIGN UP AND REGISTER"));
                }
            }




        }


    }
}









public static class circleclass
{
    public static string circlePath;
    public static string sessionpath;

    // public static string CrossSceneInformation;
}


public static class staticclass
{
    public static string FolderPath;

}
