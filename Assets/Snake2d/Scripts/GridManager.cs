using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{   public int width, height;
    public GameObject VerticalGridPrefab;
    public GameObject HorizontalGridPrefab;
    public GameObject GridPrefab;
   // public Transform camera;

    // Start is called before the first frame update
    void Start()
    {   //for (var i = 0; i < 60; i++)
        //    {
        //        Instantiate(VerticalGridPrefab, new Vector2(i * 1.0f, 0), Quaternion.identity);
        //    }

        //for (var i = 0; i < 30; i++)
        //{
        //    Instantiate(HorizontalGridPrefab, new Vector2(0, i * 1.0f), Quaternion.identity);
        //}
        GenerateGrid();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateGrid()
    {   for (int x = -51; x < width; x++)
        {
            for ( int y = -37; y < height; y++)
            {
                Instantiate(GridPrefab, new Vector3(x, y), Quaternion.identity);
            }
        }
        //camera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f); 
    }


}
