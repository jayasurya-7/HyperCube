using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //public GameManager gameManager;
    
    private void Start()
    {
        
        Destroy(gameObject, 2f);

        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }





    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            Score.Scorevalue += 1; 
            Destroy(col.gameObject);
            Destroy(gameObject);
            
            //gameManager.IncrementScore();
        }
        if (col.gameObject.tag.Equals("Player"))
        {
            Score.Scorevalue += 1;
            Destroy(gameObject);
            //gameManager.IncrementScore();
        }
    }
}
