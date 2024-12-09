using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBrickColorAssigner : MonoBehaviour
{
    public Transform parentBrick; // Reference to the parent brick object in the hierarchy

    void Start()
    {
        AssignRandomColorsToBricks();
    }

    void AssignRandomColorsToBricks()
    {
        if (parentBrick == null)
        {
            Debug.LogError("ParentBrick is not assigned in the Inspector!");
            return;
        }

        // Loop through each child of the parent brick
        foreach (Transform brick in parentBrick)
        {
            // Get or add a Renderer component on the brick
            Renderer brickRenderer = brick.GetComponent<Renderer>();
            if (brickRenderer != null)
            {
                // Generate a random color that isn't too close to white
                Color randomColor = GenerateRandomHexColor();

                // Assign the color to the material
                brickRenderer.material.color = randomColor;
            }
            else
            {
                Debug.LogWarning($"Brick {brick.name} does not have a Renderer component.");
            }
        }
    }

    Color GenerateRandomHexColor()
    {
        Color randomColor;

        do
        {
            // Generate random values for R, G, B between 0 and 1
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            randomColor = new Color(r, g, b);

        } while (IsTooCloseToWhite(randomColor)); // Keep generating if the color is too close to white

        return randomColor;
    }

    bool IsTooCloseToWhite(Color color)
    {
        // A color is considered "too white" if all RGB values are above 0.8
        return color.r > 0.8f && color.g > 0.8f && color.b > 0.8f;
    }
}
