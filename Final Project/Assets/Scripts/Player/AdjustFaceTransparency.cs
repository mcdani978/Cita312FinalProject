using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustFaceTransparency : MonoBehaviour
{
    public GameObject topFace;      
    public GameObject bottomFace;   
    public float transparency = 0.5f; 

    private Material topMaterial;
    private Material bottomMaterial;

    void Start()
    {
        
        topMaterial = topFace.GetComponent<Renderer>().material;
        bottomMaterial = bottomFace.GetComponent<Renderer>().material;

        
        SetTransparency(topMaterial, transparency);
        SetTransparency(bottomMaterial, transparency);
    }

    void Update()
    {
        SetTransparency(topMaterial, transparency);
        SetTransparency(bottomMaterial, transparency);
    }

    private void SetTransparency(Material material, float alpha)
    {
        if (material != null)
        {
            Color color = material.color;
            color.a = Mathf.Clamp01(alpha); 
            material.color = color;

           
            if (material.shader.name == "Standard")
            {
                material.SetFloat("_Mode", 3); 
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
            }
        }
    }
}
