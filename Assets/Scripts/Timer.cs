using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{   public float TimeLeft;
    public bool TimerOn = false;
    public TMPro.TMP_Text TimerText;
    public static Timer instance;
    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = (PlayerPrefs.GetFloat("Time")) * 60f;
        if (instance != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                TimerUpdate(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;    

            }
        }
        
    }

    void TimerUpdate(float CurrentTime)
    {
        CurrentTime += 1;

        float minutes = Mathf.FloorToInt(CurrentTime / 60);
        float seconds = Mathf.FloorToInt(CurrentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
