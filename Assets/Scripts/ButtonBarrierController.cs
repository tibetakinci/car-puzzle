using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class ButtonBarrierController : MonoBehaviour
{
    private UnityEngine.Vector3 startPosition;
    private float targetYPosition = -0.6f;
    public float buttonSpeed;
    private bool isPressed = false;
    private bool isUp = true;
    private bool isClicked = false;

    public GameObject barrier;
    public float barrierSpeed;
    private float barrierRotationAmount = 270f;
    private float closedBarrierRotationAmount = 358f;
    public bool isOpen = false;

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
            if(isClicked)
                AnimateButton();
            StartCoroutine(AnimateBarrier());
        }
    }

    void OnMouseDown()
    {
        isPressed = true;
        isClicked = true;
    }

    private IEnumerator AnimateBarrier()
    {
        if(!isOpen)
        {
            OpenBarrier();
        }
        if(barrierRotationAmount >= barrier.transform.rotation.eulerAngles.z)
        {
            isOpen = true;
            yield return new WaitForSeconds(0.5f);
        }
        if(isOpen)
        {
            CloseBarrier();
        }
        if(closedBarrierRotationAmount <= barrier.transform.rotation.eulerAngles.z)
        {
            isOpen = false;
            isPressed = false;
        }
    }

    void OpenBarrier()
    {
        barrier.transform.Rotate(UnityEngine.Vector3.back * barrierSpeed * Time.deltaTime);
    }

    void CloseBarrier()
    {
        barrier.transform.Rotate(UnityEngine.Vector3.forward * barrierSpeed * Time.deltaTime);
    }

    void AnimateButton()
    {
        if(isUp)
        {
            ButtonPushDown();
        }
        if(transform.position.y <= targetYPosition)
        { 
            isUp = false;
        }
        if(!isUp) 
        {
            ButtonPullUp();
        }
        if(transform.position.y >= startPosition.y)
        {
            isUp = true;
            isClicked = false;
        }
    }

    void ButtonPushDown()
    {
        transform.position -= transform.forward * buttonSpeed * Time.deltaTime;
    }

    void ButtonPullUp()
    {
        transform.position += transform.forward * buttonSpeed * Time.deltaTime;
    }
}
