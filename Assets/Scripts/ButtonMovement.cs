using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    private UnityEngine.Vector3 startPosition;
    private float targetYPosition = -0.6f;
    public float buttonAcceleration;
    private bool isPressed = false;
    private bool isBottom = false;
    private bool isUp = true;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            AnimateButton();
        }
    }

    void OnMouseDown()
    {
        isPressed = true;
    }


    void AnimateButton() 
    {
        if(isPressed && isUp)
        {
            PushDown();
        }
        if(transform.position.y <= targetYPosition)
        { 
            isBottom = true;
            isUp = false;
        }
        if(isPressed && isBottom) 
        {
            PullUp();
        }
        if(transform.position.y >= startPosition.y)
        {
            isUp = true;
            isBottom = false;
            isPressed = false;
        }
    }

    void PushDown()
    {
        transform.position -= transform.forward * buttonAcceleration * Time.deltaTime;
    }

    void PullUp()
    {
        transform.position += transform.forward * buttonAcceleration * Time.deltaTime;
    }
}
