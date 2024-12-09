using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransparentWall : MonoBehaviour
{
    private TMP_Text lifeText;
    private BallManager ballManager;

    void Start()
    {
    
        GameObject lifeObject = GameObject.Find("LifeText");
        if (lifeObject != null)
        {
            lifeText = lifeObject.GetComponent<TMP_Text>();
        }

        ballManager = FindObjectOfType<BallManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
          
            ScoreManager.Instance.LoseLife();

         
            if (lifeText != null)
            {
                lifeText.text = "Life: " + Mathf.Max(ScoreManager.Instance.GetLives(), 0);
            }

           
            ballManager.RemoveBall();

          
            if (ScoreManager.Instance.GetLives() <= 0)
            {
                Debug.Log("Game Over");
                
            }
        }
    }
}
