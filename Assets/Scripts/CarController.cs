using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;
    private GameObject tick;

    public Transform gridTransform;
    private float t = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tick = this.transform.Find("tick").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCar();
    }

    void FixedUpdate()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = gridTransform.position;
        transform.position = Vector3.MoveTowards(startPosition, Vector3.Lerp(startPosition, endPosition, t), speed * Time.deltaTime);
    }

    void MoveCar()
    {
        GoForward();
        //StartCoroutine(MoveToPosition());
    }

    void GoForward()
    {
        Vector3 direction = Vector3.back;
        rb.velocity = direction * speed;

        //Vector3 move = Vector3.Lerp(transform.position, transform.position+direction, 0.1f) * speed * Time.deltaTime;
        //rb.MovePosition(transform.position + direction*speed);
    }

    private IEnumerator MoveToPosition()
    {
        float t = 0;
        Vector3 start = transform.position;
        Vector3 direction = Vector3.forward;
 
        while (t <= 1)
        {
            t += Time.fixedDeltaTime / speed;
            rb.MovePosition(Vector3.Lerp (start, start-direction, t));

            yield return null;
        }
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
