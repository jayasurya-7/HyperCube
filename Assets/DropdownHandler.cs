using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.IO.Ports;

public class DropdownHandler : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //string[] ComPorts = GetComponent<hyper1>().Ports;
        string[] ports = SerialPort.GetPortNames();
        //Debug.Log(ports);
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();

        foreach (string port in ports)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = port });
        }
        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

        
    }

   

        // Update is called once per frame
   
    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        Debug.Log(index);
        PlayerPrefs.SetString("COMPort", dropdown.options[index].text);
        Debug.Log(PlayerPrefs.GetString("COMPort"));
    }

}
