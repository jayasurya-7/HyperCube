using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceColumn : MonoBehaviour
{
    public AudioClip[] carsound;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = carsound[Random.Range(0,carsound.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger" + collision.gameObject.tag);
        if (collision.GetComponent<PlayerController_Car2D>()!=null)
        {
            //Debug.Log("collided");
            GameManager_Car2D.instance.PlayerReached();
            if(!collision.GetComponent<PlayerController_Car2D>().isAlive)
            {
                collision.GetComponent<PlayerController_Car2D>().isAlive = true;
            }
            else
            {
                GetComponent<AudioSource>().Play();
            }
            
        }

    }
}
