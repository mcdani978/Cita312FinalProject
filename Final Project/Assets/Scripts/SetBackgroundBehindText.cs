using UnityEngine;

public class SetBackgroundBehindText : MonoBehaviour
{
    public GameObject backgroundImage;  
    public GameObject gameOverText;     

    void Start()
    {
        
        SetBackgroundLayer();
    }

    void SetBackgroundLayer()
    {
       
        Canvas textCanvas = gameOverText.GetComponent<Canvas>();
        Canvas bgCanvas = backgroundImage.GetComponent<Canvas>();

        
        if (textCanvas == null)
        {
            textCanvas = gameOverText.AddComponent<Canvas>();
        }
        if (bgCanvas == null)
        {
            bgCanvas = backgroundImage.AddComponent<Canvas>();
        }

       
        if (textCanvas != null && bgCanvas != null)
        {
            textCanvas.sortingOrder = 1;  
            bgCanvas.sortingOrder = 0;    
        }
    }
}
