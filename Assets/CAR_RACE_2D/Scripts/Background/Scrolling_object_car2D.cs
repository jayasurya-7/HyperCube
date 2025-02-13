using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_object_car2D : MonoBehaviour
{
    private Rigidbody2D rigid;
    //private float scrollspeed = -4f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0,GameManager_Car2D.instance.scrollspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager_Car2D.instance.isGameOver)
        {
            StartCoroutine(StopMoving());
        }
        else
        {
            rigid.velocity = new Vector2(0, GameManager_Car2D.instance.scrollspeed);
        }
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector2.zero;
    }
}
