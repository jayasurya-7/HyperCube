using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class Friendly : MonoBehaviour
{
    public GameObject Background;
    //public Text gameover;
    // Start is called before the first frame update
    void Start()
    {
        //Background.SetActive(true);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            Score.Scorevalue -= 1;
            //Background.SetActive(true);
            Destroy(col.gameObject);
            Destroy(gameObject);
            //gameover.text = "GAMEOVER";
            //gameManager.IncrementScore();
        }

        if (col.gameObject.tag.Equals("Player"))
        {
            //Background.SetActive(true);
            SceneManager.LoadScene("GameOver");
            Destroy(col.gameObject);
            Destroy(gameObject);

            //gameManager.IncrementScore();
        }
        //if (col.gameObject.tag.Equals("Player"))
        //{
        //    Background.SetActive(true);
        //}


    }
}
