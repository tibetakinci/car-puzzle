using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    private UnityEngine.Vector3 targetPosition;
    private UnityEngine.Vector3 startPosition;
    private float targetYPosition = 2.2f;
    public float buttonAcceleration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        AnimateButton();
    }


    void AnimateButton() 
    {
        UnityEngine.Debug.Log("it is working");

        transform.position -= transform.forward * buttonAcceleration * Time.deltaTime;
       
        UnityEngine.Debug.Log(transform.position);
    }
}
