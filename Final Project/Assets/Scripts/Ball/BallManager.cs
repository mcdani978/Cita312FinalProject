using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab; 
    public Transform playerTransform; 
    public Vector3 ballSpawnOffset = Vector3.zero; 
    public TextMeshProUGUI spacePromptText; 
    public TextMeshProUGUI gameOverText; 
    public float promptDuration = 3f; 

    public GameObject currentBall; 
    public bool ballReadyToRelease = false; 
    public bool gameOverEffectActive = false; 
    public CameraFollow cameraFollowScript; 
    public bool canRun;

    //private Vector3 initialCameraPosition = new Vector3(-11f, 397.1f, 3.9f); // Initial camera position

    void Start()
    {
        cameraFollowScript = Camera.main.GetComponent<CameraFollow>();

      
        if (spacePromptText != null)
        {
            spacePromptText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("SpacePromptText is not assigned in the Inspector.");
        }

  
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }


        SpawnIdleBall();

    
        StartCoroutine(ShowSpacePrompt());
    }

    void Update()
    {
        // Check for space bar input to release the ball
        if (Input.GetKeyDown(KeyCode.Space) && ballReadyToRelease)
        {
            ReleaseBall();
        }

        // Activate game-over effect if lives reach 0
        if (ScoreManager.Instance != null && ScoreManager.Instance.GetLives() == 0 && !gameOverEffectActive)
        {
            ActivateGameOverEffect();
        }

        // Transition to the next scene if space is pressed after game over
        if (gameOverEffectActive && Input.GetKeyDown(KeyCode.Space))
        {
            //canRun = false;
            //DestroyBall();
            //ResetCameraPosition();
            LoadNextScene();
        }
    }

    public IEnumerator ShowSpacePrompt()
    {
     
        yield return new WaitForSeconds(promptDuration);

        // Hide the prompt
        if (spacePromptText != null)
        {
            spacePromptText.gameObject.SetActive(false);
        }
    }

    public void SpawnIdleBall()
    {
        // Spawn the ball at the center of the player
        Vector3 spawnPosition = playerTransform.position + ballSpawnOffset;
        currentBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        // Make the ball a child of the player so it moves with the player
        currentBall.transform.SetParent(playerTransform);

        // Disable ball movement initially
        Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector3.zero;
            ballRb.isKinematic = true; // Make the ball stay stationary
        }

        ballReadyToRelease = true; // The ball is now ready to be released
    }

    public void ReleaseBall()
    {
        
        Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.isKinematic = false; 
            ballRb.velocity = new Vector3(0f, -200f, 10f); 
        }

        
        currentBall.transform.SetParent(null);

        ballReadyToRelease = false; 
    }

    public void RemoveBall()
    {
        if (gameOverEffectActive) return; 

        if (ScoreManager.Instance.GetLives() > 0)
        {
            if (currentBall != null)
            {
                Destroy(currentBall);
            }

           
            SpawnIdleBall();

         
            if (spacePromptText != null)
            {
                spacePromptText.gameObject.SetActive(true);
            }

      
            StartCoroutine(ShowSpacePrompt());
        }
        else
        {
          
            Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                ballRb.isKinematic = false; 
            }
        }
    }

    public void ActivateGameOverEffect()
    {
        gameOverEffectActive = true;

        
        if (cameraFollowScript != null && currentBall != null)
        {
            cameraFollowScript.ball = currentBall.transform; 
            cameraFollowScript.enabled = true; 
        }

    
        if (gameOverText != null)
        {
            Debug.Log("Activating Game Over Text");
            gameOverText.gameObject.SetActive(true); 
        }
        else
        {
            Debug.LogError("GameOverText is not assigned or missing in the Inspector.");
        }
    }

    public void DestroyBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
    }

    //private void ResetCameraPosition()
    //{
    //    Camera.main.transform.position = initialCameraPosition;
    //}

    public void LoadNextScene()
    {
        //// Ensure any game-ending processes are stopped
        //if (cameraFollowScript != null)
        //{
        //    cameraFollowScript.enabled = false; // Disable the camera follow script
        //}

        
        SceneManager.LoadScene("Level1");

       

        // Reset the gameOverEffectActive flag to prevent reloading
       // gameOverEffectActive = false;
    }
}