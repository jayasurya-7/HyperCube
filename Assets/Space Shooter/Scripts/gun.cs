using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public hyper1 h;
     
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        
       Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
      //  float gunPos = Camera.main.WorldToScreenPoint(transform.position);
        //mousePos.x = mousePos.x - gunPos.x;
        //mousePos.y = mousePos.y - gunPos.y;
       // h.theta2 = h.theta2 - gunPos;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x) {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        }
    }
}
