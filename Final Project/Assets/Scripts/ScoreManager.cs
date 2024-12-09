using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance

    public int score; 
    public int lives = 5; 
    public int initialLives = 5; 
    public Camera mainCamera; 
    public CameraFollow cameraFollow; 
    public bool gameOverTriggered = false; 

    public GameObject ballPrefab; 
    public TextMeshProUGUI finalScoreText; 

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        
        if (mainCamera != null)
        {
            cameraFollow = mainCamera.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.enabled = false; // Disable at the start
            }
        }
    }

    // Methods to manage score
    public void AddScore(int amount)
    {
        if (gameOverTriggered) return; // Prevent score increase after game over
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }

    // Methods to manage lives
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
        }

        if (lives == 0 && !gameOverTriggered)
        {
            TriggerGameOverEffect();
        }
    }

    private void TriggerGameOverEffect()
    {
        if (cameraFollow != null)
        {
            cameraFollow.enabled = true; // Enable the CameraFollow script
        }

        gameOverTriggered = true;

        // Display the final score
        if (finalScoreText != null)
        {
            finalScoreText.text = $"Your Final Score is: {score}";
            finalScoreText.gameObject.SetActive(true); // Show the final score text
        }

        Debug.Log("Game Over! Ball is free to bounce.");
    }

    public int GetLives()
    {
        return lives;
    }

    //public void ResetGame()
    //{
    //    // Reset score and lives
    //    score = 0;
    //    lives = initialLives;

    //    // Reset game-over state
    //    gameOverTriggered = false;

    //    if (finalScoreText != null)
    //    {
    //        finalScoreText.gameObject.SetActive(false); // Hide the final score text
    //    }

    //    if (cameraFollow != null)
    //    {
    //        cameraFollow.enabled = false; // Disable the CameraFollow script
    //    }

    //    Debug.Log("Game Reset!");
    //}
}
