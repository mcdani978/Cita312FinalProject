using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBrickPositioner : MonoBehaviour
{
    public Transform parentBrick; 
    public float minY = -211f; 
    public float maxY = -120f; 
    private List<Transform> brickChildren = new List<Transform>();
    public BallManager ballManager; 

    void Start()
    {
       
        if (parentBrick != null)
        {
            foreach (Transform brick in parentBrick)
            {
                brickChildren.Add(brick);
            }
        }
        else
        {
            Debug.LogError("ParentBrick is not assigned in the Inspector!");
        }

        RandomizeBrickPositions();
    }

    void Update()
    {
       
        if (AreAllBricksDestroyed())
        {
            Debug.Log("All bricks destroyed. Resetting...");
            ResetBricks();
            ResetBall();
        }
    }

    void RandomizeBrickPositions()
    {
        if (parentBrick == null)
        {
            Debug.LogError("ParentBrick is not assigned in the Inspector!");
            return;
        }

        Debug.Log("Randomizing brick positions...");

    
        foreach (Transform brick in brickChildren)
        {
            if (brick != null)
            {

                if (brick.gameObject.activeSelf)
                {

                    float randomY = Random.Range(minY, maxY);

        
                    Vector3 newPosition = new Vector3(brick.position.x, randomY, brick.position.z);
                    brick.position = newPosition;

                    Debug.Log($"Brick {brick.name} randomized to position {newPosition}");
                }
            }
            else
            {
                Debug.LogWarning("A brick reference is null!");
            }
        }
    }

    bool AreAllBricksDestroyed()
    {
   
        foreach (Transform brick in brickChildren)
        {
            if (brick != null && brick.gameObject.activeSelf)
            {
                return false; 
            }
        }
        Debug.Log("No active bricks found.");
        return true; 
    }

    void ResetBricks()
    {
        Debug.Log("ResetBricks method called!");

       
        foreach (Transform brick in brickChildren)
        {
            if (brick != null)
            {
                brick.gameObject.SetActive(true); // Reactivate the brick
                Debug.Log($"Brick {brick.name} reactivated.");
            }
            else
            {
                Debug.LogWarning("A brick reference is null during reset!");
            }
        }

 
        RandomizeBrickPositions();
    }

    void ResetBall()
    {
        Debug.Log("Resetting ball...");
        if (ballManager != null)
        {
            //ballManager.DestroyBall(); // Destroy the current ball
            //ballManager.SpawnIdleBall(); // Spawn a new idle ball
        }
        else
        {
            Debug.LogError("BallManager is not assigned in the Inspector!");
        }
    }
}
