using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCar();
    }

    void MoveCar()
    {
        //GoForward();
        MoveToPosition();
    }

    void GoForward()
    {
        Vector3 direction = Vector3.forward;
        //rb.velocity = Vector3.Lerp(transform.position, direction, speed*Time.deltaTime);

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
            rb.MovePosition (Vector3.Lerp (start, start-direction, t));

            yield return null;
        }
    }
}
