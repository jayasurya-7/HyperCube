using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class connectHypercube : MonoBehaviour
{
    public Dropdown ComPortDropdown;
    private SerialPort serialPort;
    // Start is called before the first frame update
    void Start()
    {
        PopulateComPorts();
        ComPortDropdown.onValueChanged.AddListener(delegate { ConnectToHypercubex(); });


    }
    void ConnectToHypercubex()
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
}
