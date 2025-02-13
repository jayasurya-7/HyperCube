using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    //public GameObject Background;
    //public GameObject Friendly;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject player;
    public hyper1 h;
    public int i = 1;
    public float a;
   //public float timer = 0f;
   //public float speed = 0.0000001f;
    //public float preval;
    //public float currentval;
    //public float[] val=new float[1000000];
    //public int j;
    private void Update()
    {
       // Debug.Log(h.xposition.ToString());
        //Debug.Log(h.yposition.ToString());
        //Debug.Log(h.theta2.ToString());
        //Debug.Log(h.force_total.ToString());
        //timer += Time.deltaTime;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotateY = 0f;


        if (mousePos.x < transform.position.x)
        {
            rotateY = 180f;


        }
        transform.eulerAngles = new Vector3(transform.rotation.x, rotateY, transform.position.z);


        //  else
        //transform.eulerAngles = new Vector3(rotateY,transform.rotation.x, transform.position.z);
        // {
        //   transform.eulerAngles = new Vector3(transform.position.x,0f, transform.position.z);
        //  }


        //if (h.greenbuttonState == 0)
        //{
        //    Shoot();
        //}

        if (h.buttonPin1State == 0)
        {
            Shoot();
        }
        if (h.buttonPin2State == 0)
        {
            Shoot();
        }
        if (h.buttonPin3State == 0)
        {
            Shoot();
        }
        if (h.buttonPin4State == 0)
        {
            Shoot();
        }


        if (h.force_total > 5f)
        {
            Shoot();
        }

        //player.transform.position = new Vector3(-309.48f, 0.0f, 0.0f);
        //float value = h.theta2 - 300f;
        //float pl = player.transform.position.x;
        //float x = (value / 10)+30f; 

        // float x = (0.055f * h.theta2 - 0f);

        //float x = ((0.055f * h.theta2) - 3f);
        //float y = ((0.055f * h.theta1) - 3f);
        //player.transform.position = new Vector3(x, y, 0.0f);

        // float a = h.xposition-694;
        //float b = h.yposition-683;
        //player.transform.position = new Vector3(0f, 0f, 0f);
        //if (700 < h.yposition)
        //{
        //    a = movement();
        //    player.transform.position = new Vector3(a, 0f, 0f);
        //}
        //if (700 > h.yposition)
        //{
        //    a = movement1();
        //    player.transform.position = new Vector3(a, 0f, 0f);
        //}


        // player.transform.position = new Vector3(a, b, 0.0f);

        //player.transform.position = new Vector3((pl - h.theta2), 0, 0);

    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(bullet, 5f);
    }



    //public void movement()
    //{

    //  float step = speed * timer;
    //float pl = player.transform.position.x;

    //Debug.Log("player pos"+pl.ToString());




    //  float value = h.theta2;


    // Debug.Log(val);


    // float x = pl + (val/1000);

    //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    // player.transform.position = new Vector3(x, 0.0f, 0.0f);
    //player.transform.position =Vector3.MoveTowards(transform.position, player.transform.position, step);

    // player.transform.position = new Vector3(x, 0.0f, 0.0f);

    //}
    //public int k1 = 0;
    //public int k2 = 0;
    //public int k3 = 0;
    //public int k4 = 0;
    //public float movement()
    //{
    //    for (k1 = 0; k1 < 25; k1++)
    //        k2 = k1;
    //    return k2;
    //}
    //public float movement1()
    //{
    //    for (k3 = 0; k3 >-25; k3--)
    //        k4 = k3;
    //    return k4;
    //}

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag.Equals("Bullet"))
    //    {

    //        Destroy(col.gameObject);
    //        Destroy(Friendly);
    //        Background.SetActive(true);
    //        //gameManager.IncrementScore();
    //    }

    //    if (col.gameObject.tag.Equals("Player"))
    //    {

    //        Destroy(col.gameObject);
    //        Destroy(Friendly);
    //        Background.SetActive(true);
    //        //gameManager.IncrementScore();
    //    }

    //}


}
