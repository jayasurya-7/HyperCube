using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceShooterGameManager : MonoBehaviour
{
    
    public GameObject target;
    public GameObject friendly;
    // Start is called before the first frame update

    //int Score = 0;
    
    void Start()
    {
       
       InvokeRepeating("SpawnTarget", 1f, 2f);
       InvokeRepeating("SpawnFriendly", 3f, 2f);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //if(gameObject.tag=="bullet")
        //{
           // Spawn();
        //}
    //}
    void SpawnTarget()
    {
        float randomX = Random.Range(-9f, 9f);
        float randomY = Random.Range(0f, 4f);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        Instantiate(target, randomPosition, Quaternion.identity);
        

    }

    void SpawnFriendly()
    {
        float randomX = Random.Range(-9f, 9f);
        float randomY = Random.Range(0f, 4f);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        Instantiate(friendly, randomPosition, Quaternion.identity);

    }

   


}
