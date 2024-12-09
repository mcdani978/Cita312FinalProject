using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionHandling : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Vector3 lastPosition; 

    void Update()
    {
       
        lastPosition = transform.position;

        
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

     
        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;

      
        transform.position += movement;
    }

    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Wall"))
        {
         
            transform.position = lastPosition;
        }
    }
}
