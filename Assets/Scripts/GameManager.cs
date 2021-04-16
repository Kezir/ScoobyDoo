using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Grafika")]   
    public GameObject[] npcGraphics;   
    public GameObject playerGraphic;
    
    [Header("Pozycje")]
    public Transform npcPosRozmowa;
    public Transform playerPosRozmowa;
    public Transform npc_1;
    public Transform npc_2;
    public Transform laptop;

    [Header("Obiekty/Itemy")]
    public GameObject canvasLaptop_Item;
    public GameObject canvasRozmowa;
    public GameObject canvasOpcje;
    public GameObject player;
    public GameObject[] npcs;
    public GameObject npcOdpowiedz;

    [Header("Text")]
    public GameObject[] opcjeGameObj;
    public TextMeshProUGUI[] opcjeText;


    [Header("Zmienne")]
    public Camera camera;
    public Vector3 sala108;
    public Vector3 sala106;
    public Vector3 korytarz;
    public static string obecnyPokoj = "Sala108";
    RaycastHit hit;  
    private string activeItem;
    private NavMeshAgent playerAgent;
    private PlayerController playerController;
    private GameObject playerTemp;
    private GameObject npcTemp;

    void Start()
    {
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && playerController.canMove == true)
            {
                if(hit.transform.name == "Laptop_Item")
                {
                    activeItem = "Laptop_Item";
                }
                else if(hit.transform.name == "NPC_1")
                {
                    activeItem = "NPC_1";
                }
                else if (hit.transform.name == "NPC_2")
                {
                    activeItem = "NPC_2";
                }
                else
                {
                    activeItem = "";
                }
                
            }
        
        }
        if(activeItem == "Laptop_Item" && Vector3.Distance(player.transform.position, laptop.position) < 4f)
        {
            canvasLaptop_Item.SetActive(true);
            playerController.canMove = false;
            activeItem = "";
        }
        else if (activeItem == "NPC_1" && Vector3.Distance(player.transform.position, npc_1.position) < 4f)
        {
            // grafika
            playerTemp = Instantiate(playerGraphic,playerPosRozmowa.position, playerGraphic.transform.rotation);
            npcTemp = Instantiate(npcGraphics[0], npcPosRozmowa.position, npcGraphics[0].transform.rotation);
            // mechanika
            playerAgent.SetDestination(player.transform.position);
            StartCoroutine(ExecuteAfterTime(1.5f));
            canvasRozmowa.SetActive(true);
            playerController.canMove = false;
            activeItem = "";
        }
        else if (activeItem == "NPC_2" && Vector3.Distance(player.transform.position, npc_2.position) < 4f)
        {
            // grafika
            playerTemp = Instantiate(playerGraphic, playerPosRozmowa.position, playerGraphic.transform.rotation);
            npcTemp = Instantiate(npcGraphics[1], npcPosRozmowa.position, npcGraphics[1].transform.rotation);
            // mechanika
            playerAgent.SetDestination(player.transform.position);
            StartCoroutine(ExecuteAfterTime(1.5f));
            canvasRozmowa.SetActive(true);
            playerController.canMove = false;
            activeItem = "";
        }

        if (obecnyPokoj == "Sala108")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala108, 200f * Time.deltaTime);
            //camera.transform.position = Vector3.Lerp(sala108, camera.transform.position, 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Sala106")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala106, 200f * Time.deltaTime);
            //camera.transform.position = Vector3.Lerp(sala106, camera.transform.position, 200f * Time.deltaTime);
        }
        else if(obecnyPokoj == "Korytarz")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, korytarz, 200f * Time.deltaTime);
            //camera.transform.position = Vector3.Lerp(korytarz, camera.transform.position, 200f * Time.deltaTime);
        }

    }

    public void DestroyGraphics()
    {
        Destroy(playerTemp);
        Destroy(npcTemp);
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        ShowOptions();
      
        ChangeText();
   

        // Code to execute after the delay
    }

    void ShowOptions()
    {
        canvasOpcje.SetActive(true);
        for (int i = 0; i < opcjeGameObj.Length; i++)
        {
            opcjeGameObj[i].SetActive(true);
        }
    }
    void ChangeText()
    {

        if(npcTemp.name == "NPC_1_Graph(Clone)")
        {
            for (int i = 0; i < opcjeGameObj.Length; i++)
            {
                opcjeText[i].text = npcs[0].GetComponent<NPC>().pytania[i];
            }
        }
        else if(npcTemp.name == "NPC_2_Graph(Clone)")
        {
            for (int i = 0; i < opcjeGameObj.Length; i++)
            {
                opcjeText[i].text = npcs[1].GetComponent<NPC>().pytania[i];
            }
        }
        
    }

    public void Odpowiedz1()
    {
        npcOdpowiedz.SetActive(true);
        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcs[0].GetComponent<NPC>().odpowiedzi[0];
    }

    public void Odpowiedz2()
    {
        npcOdpowiedz.SetActive(true);
        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcs[0].GetComponent<NPC>().odpowiedzi[1];
    }

    public void Odpowiedz3()
    {

    }
}
