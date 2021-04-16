using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    void OnMouseEnter()
    {
        transform.GetComponent<Outline>().OutlineWidth = 1.7f;
    }
    void OnMouseExit()
    {
        transform.GetComponent<Outline>().OutlineWidth = 0f;
    }
}
