//using NeuroRehabLibrary;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using NeuroRehabLibrary;

//public class carsession : MonoBehaviour
//{
//    // Start is called before the first frame update



//    private GameSession currentGameSession;


//    void Start()
//    {
//        StartNewGameSession();
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    void StartNewGameSession()
//    {
//        currentGameSession = new GameSession
//        {
//            GameName = "SPACESHOOTER",
//            Assessment = 0 // Example assessment value, adjust as needed
//        };

//        SessionManager.Instance.StartGameSession(currentGameSession);
//        Debug.Log($"Started new game session with session number: {currentGameSession.SessionNumber}");

//        SetSessionDetails();
//    }


//    private void SetSessionDetails()
//    {
//        string device = "HYPER"; // Set the device name
//        string assistMode = "Null"; // Set the assist mode
//        string assistModeParameters = "Null"; // Set the assist mode parameters
//        string deviceSetupLocation = "Null"; // Set the device setup location
        

//        string gameParameter = "Null";

       

//        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);


//        SessionManager.Instance.SetDevice(device, currentGameSession);
//        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
//        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);



//    }








//}
