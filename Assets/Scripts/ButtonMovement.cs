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

    public GameObject barrier;
    public float barrierSpeed = 1f;
    private float barrierRotationAmount = -90f;
    private UnityEngine.Vector3 startRotation;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = barrier.transform.localRotation.eulerAngles;
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
        OpenBarrier();
        /*
        if(isOpen)
        {
            CloseBarrier();
        }
         */
    }

    void OpenBarrier()
    {
        UnityEngine.Debug.Log("girdi");
        UnityEngine.Quaternion endRotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, startRotation.z + barrierRotationAmount));

        barrier.transform.localRotation = UnityEngine.Quaternion.Slerp(barrier.transform.localRotation, endRotation, barrierSpeed * Time.deltaTime);
        if(barrierRotationAmount >= barrier.transform.rotation.z) 
        {
            isOpen = true;
        }
    }

    void CloseBarrier()
    {
        UnityEngine.Debug.Log("girdiClose");
        UnityEngine.Quaternion endRotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, 0));

        barrier.transform.rotation = UnityEngine.Quaternion.Slerp(barrier.transform.rotation, endRotation, barrierSpeed * Time.deltaTime);
        UnityEngine.Debug.Log("girdiClose");
        if(endRotation == barrier.transform.rotation) 
        {
            isOpen = false;
        }
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
