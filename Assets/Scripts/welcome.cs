using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System;

using System.Collections;

public class welcome : MonoBehaviour
{
    public InputField hospno;

    public InputField Patientname;
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
        p_patientname = Patientname.text;
        bool Patientname_check = string.IsNullOrEmpty(p_patientname);
        if (Patientname_check == true)
        {
            Debug.Log("Empty name");

        }
        else
        {
            string path_to_data = Application.dataPath;

            if (!Directory.Exists(path_to_data + "\\" + p_hospno))
            {
                string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno;
                circleclass.circlePath = patientDir;
            }
        }

        p_hospno = hospno.text;
        bool hospno_check = string.IsNullOrEmpty(p_hospno);
        if (hospno_check == true)
        {
            Debug.Log("Empty hospno");

        }
        else
        {
            string path_to_data = Application.dataPath;

            if (!Directory.Exists(path_to_data + "\\" + p_hospno))
            {
                string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno;
                circleclass.circlePath = patientDir;
                if (Directory.Exists(patientDir))
                {










                    string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                    string newDirPath = Path.Combine(patientDir, dateTimeNow);

                    if (Directory.Exists(newDirPath))
                    {
                        staticclass.FolderPath = newDirPath;
                    }
                    else
                    {
                        Directory.CreateDirectory(newDirPath);
                        staticclass.FolderPath = newDirPath;
                    }






                    SceneManager.LoadScene("newscene");










                }
                else
                {
                    Debug.Log("Hospital Number Does not exist");

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
