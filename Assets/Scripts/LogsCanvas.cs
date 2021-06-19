using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsCanvas : MonoBehaviour
{
    private Vector3 startPos;
    public GameObject npcCanvas;
    public GameObject target;
    public GameObject canvasPrzyciski;
    //public GameObject player;
    //public GameObject opcje;
   // public GameObject npcOdpowiedz;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        //transform.Translate(new Vector3(0,100f,0) * Time.deltaTime, Space.World);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 100f * Time.deltaTime);
    }

    public void ExitGraphics()
    {
        GameManager.Instance.DestroyGraphics();
        PlayerController.Instance.canMove = true;
        npcCanvas.SetActive(false);
        canvasPrzyciski.SetActive(true);
        
        //gameObject.SetActive(false);
        //opcje.SetActive(false);
        //npcOdpowiedz.SetActive(false);
        gameObject.transform.position = startPos;
    }
    public void Exit()
    {
        PlayerController.Instance.canMove = true;
        gameObject.SetActive(false);
        canvasPrzyciski.SetActive(true);
        gameObject.transform.position = startPos;
    }
}
