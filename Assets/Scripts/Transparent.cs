using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    public Material current;
    public float transparent;
    // Start is called before the first frame update
    void Start()
    {
        current.SetFloat("_Mode", 2);
        current.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        current.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        current.SetInt("_ZWrite", 0);
        current.DisableKeyword("_ALPHATEST_ON");
        current.EnableKeyword("_ALPHABLEND_ON");
        current.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        current.renderQueue = 3000;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
