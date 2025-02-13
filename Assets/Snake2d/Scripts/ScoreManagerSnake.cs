using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerSnake : MonoBehaviour
{
    public static ScoreManagerSnake instance;
    public AudioSource ScoreSound;
    public Text ScoreText;
    public Text HighScoreText;
    int score = 0;
    int highscore = 0;

    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        ScoreText.text = "Score:" + score.ToString();
        HighScoreText.text = "Highscore:" + highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 10;
        ScoreText.text = "Score:" + score.ToString();
        ScoreSound.Play();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
       
    }
}
