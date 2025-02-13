using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoverCar2D : MonoBehaviour
{
    public float objectSpeed = -3.0f;
    public float objectMoveX = 0;
    public float objectMoveY = 1;
    public float objectMoveZ = 0;


    void Update()
    {
        //objectSpeed=-2.2f-AppData.startGameLevelSpeed * .1f;
        transform.Translate(new Vector3(objectMoveX, objectMoveY, objectMoveZ) * objectSpeed * GameManager_Car2D.instance.gameSpeed * Time.deltaTime);
    }
}
