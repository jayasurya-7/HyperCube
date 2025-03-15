using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mechSceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ROM values = new ROM();
        if (values.datetime != null)
        {
            AppData.handleAngleMax = values.handleMax;
            AppData.handleAngleMin = values.handleMin;
            AppData.handleGripForce = values.gripForce;
            AppData.grossKnobMin = values.grossKnobMin;
            AppData.grossKnobMax = values.grossKnobMax;
            AppData.fineKnobMin = values.fineKnobMin;
            AppData.fineKnobMax = values.fineKnobMax;
            AppData.keyKnobMin = values.keyKnobMin;
            AppData.keyKnobMax = values.keyKnobMax;
            AppData.graspMax = values.tripodMax;
            AppData.graspMin = values.tripodMin;
        }
        else SceneManager.LoadScene("ROM Assessment");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($" angle 1 :{JediSerialPayload.angle_1}");

        
    }
}
