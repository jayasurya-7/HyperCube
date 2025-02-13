using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject FoodPrefab;
    public Transform borderTop;
    public Transform borderLeft;
    public Transform borderRight;
    public Transform borderBottom;
   
   
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 3, 4);
    }

    //spawn one piece of food
    void Spawn() 
    {
        // x position between left and right borders
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        // y position between top and bottom borders
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);
        Instantiate(FoodPrefab, new Vector2(x, y), Quaternion.identity);
    }

}
