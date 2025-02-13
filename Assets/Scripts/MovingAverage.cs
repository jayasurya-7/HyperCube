using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageValues : MonoBehaviour
{
    public int NewValue; //this is an example for the value to be averaged
    public int MovingAverageLength = 10; //made public in case you want to change it in the Inspector, if not, could be declared Constant

    private int count;
    private float movingAverage;


    // Update is called once per frame
    void Update()
    {
        count++;

        //This will calculate the MovingAverage AFTER the very first value of the MovingAverage
        if (count > MovingAverageLength)
        {
            movingAverage = movingAverage + (NewValue - movingAverage) / (MovingAverageLength + 1);

            //Debug.Log("Moving Average: " + movingAverage); //for testing purposes

        }
        else
        {
            //NOTE: The MovingAverage will not have a value until at least "MovingAverageLength" values are known (10 values per your requirement)
            movingAverage += NewValue;

            //This will calculate ONLY the very first value of the MovingAverage,
            if (count == MovingAverageLength)
            {
                movingAverage = movingAverage / count;
                //Debug.Log("Moving Average: " + movingAverage); //for testing purposes
            }
        }

    }
}