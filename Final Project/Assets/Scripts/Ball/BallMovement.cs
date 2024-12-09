using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;
    private bool isRegularMovementActive = false;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isRegularMovementActive)
        {
           
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    public void EnableRegularMovement()
    {
        
        isRegularMovementActive = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isRegularMovementActive)
        {
            
            isRegularMovementActive = true;

           
            Vector3 reflectDir = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = reflectDir.normalized * speed;
        }
        else
        {
          
            if (collision.gameObject.CompareTag("Brick"))
            {
                Vector3 currentVelocity = rb.velocity;
                Vector3 newVelocity = new Vector3(
                    currentVelocity.x * 0.7f,
                    Mathf.Abs(currentVelocity.magnitude) * 0.8f,
                    currentVelocity.z * 0.7f
                );
                rb.velocity = newVelocity.normalized * speed;
            }
            else
            {
                Vector3 reflectDir = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
                rb.velocity = reflectDir.normalized * speed;
            }
        }
    }
}
