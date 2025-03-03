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
    JediSerialCom serReader;
    // Start is called before the first frame update
    void Start()
    {
        PopulateComPorts();
        ComPortDropdown.onValueChanged.AddListener(delegate { ConnectToHypercube(); });


    }
    void ConnectToHypercube()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }

        string selectedPort = ComPortDropdown.options[ComPortDropdown.value].text;


        if (selectedPort != "No Ports Found")
        {
            serReader = new JediSerialCom(selectedPort);
            serReader.ConnectToArduino();
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
            ConnectToHypercube(); // Automatically connect to the first port
        }
        else
        {
            ComPortDropdown.AddOptions(new System.Collections.Generic.List<string> { "No Ports Found" });
            Debug.Log("No ports found");
        }
        Debug.Log("No ports found 2");
    }
}
