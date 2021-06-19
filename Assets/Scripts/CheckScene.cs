using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScene : MonoBehaviour
{
    // Start is called before the first frame update

    public Material current;
    private bool isTransparent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isTransparent == false)
            {
                current.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                current.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                current.SetInt("_ZWrite", 1);
                current.DisableKeyword("_ALPHATEST_ON");
                current.DisableKeyword("_ALPHABLEND_ON");
                current.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                current.renderQueue = -1;
                isTransparent = true;
                GameManager.obecnyPokoj = gameObject.name;
                /*if(gameObject.name == "Korytarz")
                {
                    PlayerController.Instance.AnimDeskorolkaStart();
                    PlayerController.Instance.walk = false;
                    PlayerController.Instance.agent.speed = 0;

                }
                else if(PlayerController.Instance.walk == false)
                {                   
                    PlayerController.Instance.AnimDeskorolkaKoniec();
                    PlayerController.Instance.walk = true;                                    
                }
                */
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isTransparent == true)
            {
                current.SetFloat("_Mode", 2);
                current.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                current.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                current.SetInt("_ZWrite", 0);
                current.DisableKeyword("_ALPHATEST_ON");
                current.EnableKeyword("_ALPHABLEND_ON");
                current.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                current.renderQueue = 3000;
                //Colormaterial.color = new Color(Colormaterial.color.r, Colormaterial.color.g, Colormaterial.color.b, transparent);
                isTransparent = false;
            }
        }
    }

}
