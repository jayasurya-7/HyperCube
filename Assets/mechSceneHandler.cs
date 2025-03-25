using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mechSceneHandler : MonoBehaviour
{
    private string assessmentScene = "ROM Assessment";
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
        else SceneManager.LoadScene(assessmentScene);
        string sessionNum = "Session_" + AppData.currentSessionNumber;
        string pth = Path.Combine(AppData.rawDataPath, sessionNum, "RawData");
        string gamepth = Path.Combine(AppData.rawDataPath, sessionNum, "GameData");
        AppData.rawData = pth;
        AppData.gameDataPath = gamepth;
        if (!Directory.Exists(pth) || !Directory.Exists(gamepth))
        {
            Directory.CreateDirectory(pth);
            Directory.CreateDirectory(gamepth);
            Debug.Log(pth);
        }
        else
        {
            Debug.Log("not created");
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log($" angle 1 :{JediSerialPayload.angle_1}");

        
    }
    public void assessmentButton()
    {
        SceneManager.LoadScene(assessmentScene);
    }
}
