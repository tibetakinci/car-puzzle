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
    private bool isUp = true;

    public GameObject fence;
    private float fenceTargetYPosition = 5.1f;


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
            AnimateBarrier();
        }
    }

    void OnMouseDown()
    {
        isPressed = true;
    }

    void AnimateBarrier()
    {
        
    }

    void AnimateButton()
    {
        if(isPressed && isUp)
        {
            ButtonPushDown();
        }
        if(transform.position.y <= targetYPosition)
        { 
            isUp = false;
        }
        if(isPressed && !isUp) 
        {
            ButtonPullUp();
        }
        if(transform.position.y >= startPosition.y)
        {
            isUp = true;
            isPressed = false;
        }
    }

    void ButtonPushDown()
    {
        transform.position -= transform.forward * buttonAcceleration * Time.deltaTime;
    }

    void ButtonPullUp()
    {
        transform.position += transform.forward * buttonAcceleration * Time.deltaTime;
    }
}
