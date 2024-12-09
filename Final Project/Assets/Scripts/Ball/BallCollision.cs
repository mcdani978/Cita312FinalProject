using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class BallCollision : MonoBehaviour
{

    private TMP_Text scoreText;


    void Start()
    {
      
        GameObject scoreObject = GameObject.Find("ScoreText");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TMP_Text>();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.CompareTag("Brick"))
        {
         
            ContactPoint[] contactPoints = collision.contacts;
            List<GameObject> bricksInContact = new List<GameObject>();

    
            foreach (ContactPoint contact in contactPoints)
            {
                GameObject collidedObject = contact.otherCollider.gameObject;
                if (collidedObject.CompareTag("Brick"))
                {
                    bricksInContact.Add(collidedObject);
                }
            }

            if (bricksInContact.Count > 0)
            {
      
                int randomIndex = Random.Range(0, bricksInContact.Count);
                Destroy(bricksInContact[randomIndex]);
            }
            else
            {
            
                Destroy(collision.gameObject);
            }

       
            ScoreManager.Instance.AddScore(10);


            if (scoreText != null)
            {
                scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
            }
        }
    }
}