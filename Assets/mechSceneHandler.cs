using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mechSceneHandler : MonoBehaviour
{
    void Start()
    {
        AppData.rom = new ROM();
        if (AppData.rom.datetime != null)
        {
            AppData.handleAngleMax = AppData.rom.handleMax;
            AppData.handleAngleMin = AppData.rom.handleMin;
            AppData.handleGripForce = AppData.rom.gripForce;
            AppData.grossKnobMin = AppData.rom.grossKnobMin;
            AppData.grossKnobMax = AppData.rom.grossKnobMax;
            AppData.fineKnobMin = AppData.rom.fineKnobMin;
            AppData.fineKnobMax = AppData.rom.fineKnobMax;
            AppData.keyKnobMin = AppData.rom.keyKnobMin;
            AppData.keyKnobMax = AppData.rom.keyKnobMax;
            AppData.graspMax = AppData.rom.tripodMax;
            AppData.graspMin = AppData.rom.tripodMin;
        }
        else SceneManager.LoadScene("ROM Assessment");
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

    void Update()
    {

        
    }
}
