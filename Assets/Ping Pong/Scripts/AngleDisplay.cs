using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleDisplay : MonoBehaviour
{
    public Text MaxAngle;
    public Text MinAngle;
    public Text CurrentAngle;
    public GameObject AngleDisplayCanvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Control Method") == 5) 
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = PlayerPrefs.GetFloat("Knob Ang Max").ToString();
            MinAngle.text = PlayerPrefs.GetFloat("Knob Ang Min").ToString();
           // CurrentAngle.text = hyper1.instance.ang2.ToString();
            CurrentAngle.text = JediSerialPayload.angle_2.ToString();
        }
        else if (PlayerPrefs.GetInt("Control Method") == 6)
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = PlayerPrefs.GetFloat("Knob Fine Ang Max").ToString();
            MinAngle.text = PlayerPrefs.GetFloat("Knob Fine Ang Min").ToString();
            //CurrentAngle.text = hyper1.instance.ang4.ToString();
            CurrentAngle.text = JediSerialPayload.angle_4.ToString();

        }
        else if (PlayerPrefs.GetInt("Control Method") == 7)
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = PlayerPrefs.GetFloat("Knob Key Ang Max").ToString();
            MinAngle.text = PlayerPrefs.GetFloat("Knob Key Ang Min").ToString();
            //  CurrentAngle.text = hyper1.instance.ang3.ToString();
            CurrentAngle.text = JediSerialPayload.angle_3.ToString();

        }
    }
}
