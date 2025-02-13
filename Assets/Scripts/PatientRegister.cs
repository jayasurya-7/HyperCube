using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime;
using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;

public class patient
{
    public string name { get; set; }
    //public string lastname { get; set; }
    public string age { get; set; }
    public string gender { get; set; }
    public string hospno { get; set; }
    //public string use_hand { get; set; }
}

public static class StaticClass
{
    public static string CrossSceneInformation { get; set; }
}
public class PatientRegister : MonoBehaviour
{

    public AudioClip[] sounds;
    private AudioSource source;

    public new InputField name;
    //public InputField lastname;
    public InputField age;
    public InputField sex;
    public InputField hospno;
    // public InputField path;
    //public int hand = 1;


    //public object JsonConvert { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        // Debug.Log(hand+" :hand");
        //onclick_register();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void onclick_register()
    {
        string p_name = name.text;
        //string p_lastname = lastname.text;
        string p_age = age.text;
        string p_sex = sex.text;
        string p_hospno = hospno.text;
        Debug.Log("Name: " + name);
        //Debug.Log("Lastname: " + lastname);
        Debug.Log("Age: " + age);
        Debug.Log("Sex: " + sex);
        Debug.Log("Hospno: " + hospno);

        //p_path = path.text;

        bool name_check = string.IsNullOrEmpty(p_name);
        bool hospno_check = string.IsNullOrEmpty(p_hospno);
        if (name_check == true | hospno_check == true)
        {
            Debug.Log("Empty name or hospno or hand");
        }
        else
        {
            //source.clip = sounds[Random.Range(0,sounds.Length)];
            //source.Play();

            //if (hand == 1)
            //{
            //    hand_ = "1";

            //}
            //else if (hand == 2)
            //{
            //    hand_ = "2";

            //}

            var patient_details = new patient { name = p_name, age = p_age, gender = p_sex, hospno = p_hospno };
            //var patient_details = new patient { name = p_name, lastname = p_lastname,age = p_age,gender = p_sex,hospno = p_hospno, use_hand = p_path};
            string patient_json = JsonConvert.SerializeObject(patient_details);

            //string path_to_data = System.AppDomain.CurrentDomain.BaseDirectory;

            string path_to_data = Application.dataPath;
            //Debug.Log(path_to_data);
            //string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + Hosp_number;

            Directory.CreateDirectory(path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno);
            Debug.Log(path_to_data);
            File.WriteAllText(path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno + "\\" + p_hospno+".json", patient_json);
            //string data = "Date" + "," + "Start Level" + "," + "End Level" + "," + "Start Time" + "," + "End Time" + "," + "Duration" + "," + "Hits" + "\n";
            //string filePath = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno + "\\" + "hits.csv";
            //File.WriteAllText(filePath, data);
            //string filePath_flappy = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno + "\\" + "flappy_hits.csv";
            //File.WriteAllText(filePath_flappy, data);

            // SceneManager.LoadScene("DrawPath");
            SceneManager.LoadScene("Start");
        }

    }

    public void onclick_existing()
    {
        // SceneManager.LoadScene("History");
        SceneManager.LoadScene("Start");
    }
}