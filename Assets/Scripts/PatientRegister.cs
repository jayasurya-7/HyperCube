using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.Linq;

public class Patient
{
    public string name { get; set; }
    public string age { get; set; }
    public string gender { get; set; }
    public string hospno { get; set; }
    public string hand { get; set; }
}

public class PatientRegister : MonoBehaviour
{
    public InputField nameInput;
    public InputField ageInput;
    public InputField hospnoInput;
    public ToggleGroup genderToggleGroup; // Toggle Group for gender selection
    public ToggleGroup handToggleGroup;   // Toggle Group for hand selection
    public Text msg;
    void Start() { }

    public void onclick_register()
    {
        string p_name = nameInput.text.Trim();
        string p_age = ageInput.text.Trim();
        string p_hospno = hospnoInput.text.Trim();

        // Get selected gender
        Toggle selectedGenderToggle = genderToggleGroup.ActiveToggles().FirstOrDefault();
        string p_gender = selectedGenderToggle != null ? selectedGenderToggle.name : "";

        // Get selected hand (if needed)
        Toggle selectedHandToggle = handToggleGroup.ActiveToggles().FirstOrDefault();
        string p_hand = selectedHandToggle != null ? selectedHandToggle.name : "";

        Debug.Log($"Name: {p_name}");
        Debug.Log($"Age: {p_age}");
        Debug.Log($"Gender: {p_gender}");
        Debug.Log($"Hospno: {p_hospno}");
        Debug.Log($"Hand: {p_hand}");

        if (string.IsNullOrEmpty(p_name) || string.IsNullOrEmpty(p_age) || string.IsNullOrEmpty(p_hospno) || string.IsNullOrEmpty(p_gender))
        {
            msg.text = "Error: Name, Age, Gender, and Hospno are required!";
            Debug.Log("Error: Name, Age, Gender, and Hospno are required!");
            return;
        }

        // Check if hospno already exists
        string patientDir = Path.Combine(Application.dataPath, "Patient_Data", p_hospno);
        if (Directory.Exists(patientDir))
        {
            msg.text = "Error: Hospno already exists! log in";
            Debug.Log("Error: Hospno already exists! log in");
            SceneManager.LoadScene("Start");
            //return;
        }

        // Create patient directory
        Directory.CreateDirectory(patientDir);

        // Save patient data
        var patientDetails = new Patient { name = p_name, age = p_age, gender = p_gender, hospno = p_hospno , hand = p_hand};
        string patientJson = JsonConvert.SerializeObject(patientDetails);
        File.WriteAllText(Path.Combine(patientDir, $"{p_hospno}.json"), patientJson);

        Debug.Log("Patient registered successfully!");
        Debug.Log(patientDir);
        // Load next scene
        SceneManager.LoadScene("Start");
    }

    public void onclick_existing()
    {
        SceneManager.LoadScene("Start");
    }
}
