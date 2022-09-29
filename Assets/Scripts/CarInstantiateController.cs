using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInstantiateController : MonoBehaviour
{
    private int TotalRightCarAmount = 4;
    private int TotalLeftCarAmount = 5;
    private int RightCarCount = 1;
    private int LeftCarCount = 1;
    private Quaternion rotation = new Quaternion(0f, 180f, 180f, 0f);

    public void CarInstantiate(Object obj, Vector3 transform, string carType)
    {
        if(CheckCarCount(carType))
            Instantiate(obj, transform, rotation);
            IncrementCount(carType);
    }

    void IncrementCount(string carType)
    {
        if(carType == "Left")
            LeftCarCount++;
        else if(carType == "Right")
            RightCarCount++;
    }

    private bool CheckCarCount(string carType)
    {
        return (carType == "Left" && LeftCarCount < TotalLeftCarAmount) || (carType == "Right" && RightCarCount < TotalRightCarAmount);
    }
}
