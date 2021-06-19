using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInside : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.obecnyPokoj = "Winda";       
        canvas.SetActive(true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);   
    }
}
