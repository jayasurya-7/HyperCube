using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPref : MonoBehaviour
{
    public InputField HN;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("Hospital Number", HN.text);
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(HN.text);
        }
    }
   
}
