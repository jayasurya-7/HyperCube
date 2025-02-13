using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    public static int Scorevalue = 0;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score:" + Scorevalue;
        if (Scorevalue == 5)
        {
            SceneManager.LoadScene("Finish");
        }
    }

    public void Gamefinish()
    {
        
    }
}
