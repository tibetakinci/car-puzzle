using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{   
    public float speed = 10.0f;
    private Rigidbody rb;
    private GameObject tick;
    public GameObject button;

    public Transform gridTransform;
    private float t = 0.1f;
    private ButtonBarrierController bbController;
    private Vector3 firstLeftCarPosition = new Vector3(-12.8f, 4.4f, 31.5f);
    private Vector3 secondLeftCarPosition = new Vector3(-12.8f, 4.4f, 42f);
    private Vector3 firstRightCarPosition = new Vector3(13.2f, 4.4f, 31.5f);
    private Vector3 secondRightCarPosition = new Vector3(13.2f, 4.4f, 42f);
    private Quaternion secondCarRotation = new Quaternion(0f, 270f, 180f, 0f);
    public bool isOpen;
    public bool isFirst;
    public bool isMoving;
    private bool isInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tick = this.transform.Find("tick").gameObject;
        isInstantiate = false;
        isFirst = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        isOpen = bbController.isOpen;
        if(isOpen || isMoving)
        {
            if (isFirst)
            {
                MoveTowards(gridTransform.position);
                isMoving = CheckPosition(gridTransform.position);
            }
            else
            {
                if(this.gameObject.tag == "Left Car")
                {
                    MoveTowards(firstLeftCarPosition);
                    isMoving = CheckPosition(firstLeftCarPosition);
                    /*
                    if(!isOpen && CheckFirstPosition(firstLeftCarPosition))
                        isFirst = true;
                        //Instantiate(this.gameObject, secondLeftCarPosition, secondCarRotation);
                        */
                }
                else if(this.gameObject.tag == "Right Car")
                {
                    MoveTowards(firstRightCarPosition);
                    isMoving = CheckPosition(firstRightCarPosition);
                    /*
                    if(!isMoving)
                        Instantiate(this, secondRightCarPosition, secondCarRotation);
                         */
                }
            }
        }

        if((CheckZPosition(firstRightCarPosition.z) || CheckZPosition(firstLeftCarPosition.z)) && !isOpen)
        {
            isFirst = true;
            if(!isInstantiate)
            {
                isInstantiate = true;
                if(this.gameObject.tag == "Left Car")
                {
                    Instantiate(this.gameObject, secondLeftCarPosition, secondCarRotation);
                }
                else if(this.gameObject.tag == "Right Car")
                { 
                    Instantiate(this, secondRightCarPosition, secondCarRotation);
                }
            }
        }
    }

    void Awake()
    {
        bbController = button.GetComponent<ButtonBarrierController>();
    }

    void MoveTowards(Vector3 endPosition)
    {
        Vector3 startPosition = transform.position;
        transform.position = Vector3.MoveTowards(startPosition, Vector3.Lerp(startPosition, endPosition, t), speed * Time.deltaTime);
    }

    bool CheckZPosition(float z)
    {
        return Mathf.Abs(transform.position.z - z) <= 0.1f;
    }

    bool CheckPosition(Vector3 endPosition)
    {
        return Vector3.Distance(this.transform.position, endPosition) >= 0.7f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (CheckCarCollision(collision))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else if(collision.gameObject.tag == "Line")
        {
            UnityEngine.Debug.Log("Line");
        }
    }

    bool CheckCarCollision(Collision collision)
    {
        return (this.gameObject.tag == "Left Car" && collision.gameObject.tag == "Right Car") || (this.gameObject.tag == "Right Car" && collision.gameObject.tag == "Left Car");
    }

    void CheckGrid()
    {
        
    }
}
