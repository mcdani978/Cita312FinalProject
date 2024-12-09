using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float minX = -10f, maxX = 10f, minZ = -10f, maxZ = 5f; 
    private Camera mainCamera; 

   
    void Start()
    {
        mainCamera = Camera.main; 
    }

 
    void Update()
    {
      
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

     
        Vector3 move = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;

      
        transform.position += move;


        ClampPlayerPosition();
    }


    void ClampPlayerPosition()
    {

        Vector3 clampedPosition = transform.position;


        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);


        transform.position = clampedPosition;
    }


    void OnValidate()
    {

        if (mainCamera != null)
        {

            if (mainCamera.orthographic)
            {
                float camSize = mainCamera.orthographicSize;
                maxX = camSize * mainCamera.aspect;
                maxZ = camSize;
                minX = -maxX;
                minZ = -maxZ;
            }
        }
    }
}
