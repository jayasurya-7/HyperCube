using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleDisplayPingPong : MonoBehaviour
{
    public Text MaxAngle;
    public Text MinAngle;
    public Text CurrentAngle;
    public static hyper1 instance;
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
            CurrentAngle.text = (Mathf.Round((hyper1.instance.ang2)*10.0f)*0.1f).ToString();
        }
        else if (PlayerPrefs.GetInt("Control Method") == 6)
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = PlayerPrefs.GetFloat("Knob Fine Ang Max").ToString();
            MinAngle.text = PlayerPrefs.GetFloat("Knob Fine Ang Min").ToString();
            CurrentAngle.text = (Mathf.Round((hyper1.instance.ang4)*10.0f)*0.1f).ToString();
        }
        else if (PlayerPrefs.GetInt("Control Method") == 7)
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = PlayerPrefs.GetFloat("Knob Key Ang Max").ToString();
            MinAngle.text = PlayerPrefs.GetFloat("Knob Key Ang Min").ToString();
            CurrentAngle.text = (Mathf.Round((hyper1.instance.ang3)*10.0f)*0.1f).ToString();
        }
    }
}
