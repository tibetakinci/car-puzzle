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
    public bool isParked;

    private CarInstantiateController carInstantiateController;
    private GridController gridController;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tick = this.transform.Find("tick").gameObject;
        isInstantiate = false;
        isFirst = false;
        isMoving = false;
        isParked = false;
        carInstantiateController = Camera.main.GetComponent<CarInstantiateController>();
        gridController = Camera.main.GetComponent<GridController>();
    }

    // Update is called once per frame
    void Update()
    {
        isOpen = bbController.isOpen;
        if(isOpen || isMoving)
        {
            if (isFirst && !isParked)
            {
                GameObject gridObject = new GameObject();
                if(this.gameObject.tag == "LeftCar")
                    gridObject = gridController.GetLeftGrid();
                else if(this.gameObject.tag == "RightCar")
                    gridObject = gridController.GetRightGrid();
                
                MoveTowards(gridObject.transform.position);
                isMoving = CheckPosition(gridObject.transform.position);
                
                if(!isMoving)
                    isParked = true;
            }
            else
            {
                if(this.gameObject.tag == "LeftCar" && !isParked)
                {
                    MoveTowards(firstLeftCarPosition);
                    isMoving = CheckPosition(firstLeftCarPosition);
                }
                else if(this.gameObject.tag == "RightCar" && !isParked)
                {
                    MoveTowards(firstRightCarPosition);
                    isMoving = CheckPosition(firstRightCarPosition);
                }
            }
        }

        if((CheckZPosition(firstRightCarPosition.z) || CheckZPosition(firstLeftCarPosition.z)) && !isOpen)
        {
            isFirst = true;
            if(!isInstantiate)
            {
                isInstantiate = true;
                if(this.gameObject.tag == "LeftCar")
                {
                    carInstantiateController.CarInstantiate(this, secondLeftCarPosition, secondCarRotation, "Left");
                }
                else if(this.gameObject.tag == "RightCar")
                { 
                    carInstantiateController.CarInstantiate(this, secondRightCarPosition, secondCarRotation, "Right");
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
        return (this.gameObject.tag == "LeftCar" && collision.gameObject.tag == "RightCar") || (this.gameObject.tag == "RightCar" && collision.gameObject.tag == "LeftCar");
    }
}
