using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground_car2D : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    float groundHorizontalLength;
    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(groundHorizontalLength);
        //   Debug.Log(transform.position.x);
        if (transform.position.y < -groundHorizontalLength)
        {
            RepositionBackgound();
        }

    }
    private void RepositionBackgound()
    {
        Vector2 groundoffset = new Vector2(0, groundHorizontalLength*2f);
        transform.position = (Vector2)transform.position + groundoffset;
    }
}
