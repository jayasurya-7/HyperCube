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
            MaxAngle.text = AppData.grossKnobMax.ToString();
            MinAngle.text = AppData.grossKnobMin.ToString();
           // CurrentAngle.text = hyper1.instance.ang2.ToString();
            CurrentAngle.text = JediSerialPayload.angle_2.ToString();
        }
        else if (PlayerPrefs.GetInt("Control Method") == 6)
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = AppData.fineKnobMax.ToString();
            MinAngle.text = AppData.fineKnobMin.ToString();
            //CurrentAngle.text = hyper1.instance.ang4.ToString();
            CurrentAngle.text = JediSerialPayload.angle_4.ToString();

        }
        else if (PlayerPrefs.GetInt("Control Method") == 7)
        {
            AngleDisplayCanvas.SetActive(true);
            MaxAngle.text = AppData.graspMax.ToString();
            MinAngle.text = AppData.graspMin.ToString();
            //  CurrentAngle.text = hyper1.instance.ang3.ToString();
            CurrentAngle.text = JediSerialPayload.angle_3.ToString();

        }
    }
}
