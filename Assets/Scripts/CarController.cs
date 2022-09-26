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
    private bool isOpen;
    private bool firstPosition = false;
    private Vector3 firstLeftCarPosition = new Vector3(-12.8f, 4.4f, 31.5f);
    private Vector3 firstRightCarPosition = new Vector3(13.2f, 4.4f, 31.5f);

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tick = this.transform.Find("tick").gameObject;
        if(this.transform.position == firstLeftCarPosition)
        {
            firstPosition = true;
        }

        if(this.transform.position == firstRightCarPosition)
        {
            firstPosition = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isOpen = bbController.isOpen;
        if(isOpen)
        {
            MoveTowards(gridTransform.position);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else if(collision.gameObject.tag == "Line")
        {
            UnityEngine.Debug.Log("Line");
            rb.velocity = new UnityEngine.Vector3(0f, 0f, 0f);
        }
        else if(collision.gameObject.tag == "Left Grid" || collision.gameObject.tag == "Right Grid")
        {
            UnityEngine.Debug.Log("Grid");
        }
    }

    void CheckGrid()
    {
        
    }
}
