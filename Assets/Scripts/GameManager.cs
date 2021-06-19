using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Grafika")]     
    public GameObject playerGraphic;
    
    [Header("Pozycje")]
    public Transform npcPosRozmowa;
    public Transform playerPosRozmowa;

    public Transform laptop;

    [Header("Obiekty/Itemy")]
    private List<GameObject> pytaniaNPC = new List<GameObject>();
    public GameObject button;
    public GameObject canvasLaptop_Item;
    public GameObject canvasRozmowa;
    public GameObject canvasPrzyciski;
    public GameObject player;
    public GameObject npcOdpowiedz;
    public List<NPC> poznaniPodejrzaniNPC = new List<NPC>();



    [Header("Zmienne")]
    public Camera camera;
    //public int progressStory;
    public int[] progressStory;
    public Vector3 sala108;
    public Vector3 sala106;
    public Vector3 sala112;
    public Vector3 sala113;
    public Vector3 sala114;
    public Vector3 sala114B;
    public static string obecnyPokoj = "Sala108";
    RaycastHit hit;
    private float temp = 15;
    private string activeItem;
    private NavMeshAgent playerAgent;
    private PlayerController playerController;
    private GameObject playerTemp;
    private GameObject npcTemp;
    public NPC npcRef;
    public NPC[] allNPC;
    

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
                else if(hit.transform.CompareTag("NPC"))
                {
                    npcRef = hit.transform.GetComponent<NPC>();
                    activeItem = "NPC";
                }              
                else
                {
                    npcRef = null;
                    activeItem = "";
                }
                
            }
        
        }
        if(activeItem == "Laptop_Item" && Vector3.Distance(player.transform.position, laptop.position) < 4f)
        {
            canvasLaptop_Item.SetActive(true);
            canvasPrzyciski.SetActive(false);
            playerController.canMove = false;
            activeItem = "";
        }
        else if (activeItem == "NPC" && Vector3.Distance(player.transform.position, npcRef.transform.position) < 4f)
        {
            // grafika
            playerTemp = Instantiate(playerGraphic,playerPosRozmowa.position, Quaternion.Euler(-30f,-33.4f,0f));
            npcTemp = Instantiate(npcRef.npcGraphic, npcPosRozmowa.position, Quaternion.Euler(-30f, -33.4f, 0f));
            playerTemp.transform.localScale = new Vector3(12,12,12);
            npcTemp.transform.localScale = new Vector3(12, 12, 12);
            // mechanika
            playerAgent.SetDestination(player.transform.position);
            npcRef.SetTalkWithPlayer();
            StartCoroutine(ExecuteAfterTime(1.5f));
            canvasRozmowa.SetActive(true);
            canvasPrzyciski.SetActive(false);
            playerController.canMove = false;
            activeItem = "";
            
        }
        

        //Debug.Log(obecnyPokoj);
        if (obecnyPokoj == "Sala108")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala108, 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Sala106")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala106, 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Sala112")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala112, 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Sala113")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala113, 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Sala114")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala114, 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Sala114B")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, sala114B, 200f * Time.deltaTime);
        }
        else if(obecnyPokoj == "Korytarz")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(player.transform.position.x - 44f, player.transform.position.y + 48f, player.transform.position.z + 65f), 200f * Time.deltaTime);
        }
        else if (obecnyPokoj == "Winda")
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(player.transform.position.x - 44f, player.transform.position.y + 48f, player.transform.position.z + 65f), 200f * Time.deltaTime);
        }

    }

    public void DestroyGraphics()
    {
        Destroy(playerTemp);
        Destroy(npcTemp);
        npcOdpowiedz.SetActive(false);
        foreach (GameObject button in pytaniaNPC)
        {
            Destroy(button);
        }
    }

    public void UpdateProgress(int i)
    {
        progressStory[i]++;

        foreach(NPC npc in allNPC)
        {
            npc.QuestionsUpdate(progressStory);
        }
    }
    /*public void UpdateProgress()
    {
        progressStory++;
        foreach(NPC npc in allNPC)
        {
            npc.QuestionsUpdate(progressStory);
        }
    }
    */
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        ShowOptions();
    }

    void ShowOptions()
    {
        if(npcRef.mozliwePytania.Count > 0)
        {
            for (int i = 0; i < npcRef.mozliwePytania.Count; i++)
            {
                GameObject butt = Instantiate(button, canvasRozmowa.transform.position + new Vector3(-12, temp, 0), canvasRozmowa.transform.rotation);
                butt.transform.SetParent(canvasRozmowa.transform);
                butt.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.mozliwePytania[i].pytanie;
                if (i == 0)
                {
                    butt.transform.GetComponent<Button>().onClick.AddListener(CustomButton_onClick0);
                }
                else if (i == 1)
                {
                    butt.transform.GetComponent<Button>().onClick.AddListener(CustomButton_onClick1);
                }
                else if (i == 2)
                {
                    butt.transform.GetComponent<Button>().onClick.AddListener(CustomButton_onClick2);
                }
                else if (i == 3)
                {
                    butt.transform.GetComponent<Button>().onClick.AddListener(CustomButton_onClick3);
                }

                butt.transform.localScale = new Vector3(1, 1, 1);
                //butt.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
                ChangeSizeOfImagePytania(i, butt);
                //Debug.Log(pytaniaNPC);
                //Debug.Log(butt);
                pytaniaNPC.Add(butt);
            }
        }
        else
        {
            // pytanie retoryczne
            GameObject butt = Instantiate(button, canvasRozmowa.transform.position + new Vector3(-12, temp, 0), canvasRozmowa.transform.rotation);
            butt.transform.SetParent(canvasRozmowa.transform);
            butt.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.basic.pytanie;
            butt.transform.GetComponent<Button>().onClick.AddListener(CustomButton_Basic);
            butt.transform.localScale = new Vector3(1, 1, 1);
            butt.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
            butt.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160f);
            pytaniaNPC.Add(butt);
        }
        //canvasOpcje.SetActive(true);
        
        temp = 15;
    }

    void ChangeSizeOfImageOdpowiedzi(int i)
    {
        if (npcRef.mozliwePytania[i].chmurkaOdpowiedz == 0)
        {
            npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
            npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160f);
        }
        else if (npcRef.mozliwePytania[i].chmurkaOdpowiedz == 1)
        {
            npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
            npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
        }
        else if (npcRef.mozliwePytania[i].chmurkaOdpowiedz == 2)
        {
            npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300f);
            npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250f);
        }
    }
    void ChangeSizeOfImagePytania(int i, GameObject pytanie)
    {
        if (npcRef.mozliwePytania[i].chmurkaPytanie == 0)
        {
            pytanie.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
            pytanie.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160f);
            temp -= 9f;
        }
        else if (npcRef.mozliwePytania[i].chmurkaPytanie == 1)
        {
            pytanie.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
            pytanie.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
            pytanie.transform.position = new Vector3(pytanie.transform.position.x, pytanie.transform.position.y - 3f, pytanie.transform.position.z);  
            temp -= 16f;
        }
        else if (npcRef.mozliwePytania[i].chmurkaPytanie == 2)
        {
            pytanie.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300f);
            pytanie.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250f);
            pytanie.transform.position = new Vector3(pytanie.transform.position.x, pytanie.transform.position.y - 8f, pytanie.transform.position.z);
            temp -= 24f;
        }
    }

    void Refresh()
    {
        foreach (GameObject button in pytaniaNPC)
        {
            Destroy(button);
        }
        ShowOptions();
    }

    void CustomButton_Basic()
    {
        if (npcOdpowiedz.active == false)
        {
            npcOdpowiedz.SetActive(true);
        }
        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.basic.odpowiedz;

        //nie ma w mozliwych pytaniach
        //ChangeSizeOfImageOdpowiedzi(0);
        npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
        npcOdpowiedz.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160f);

        npcOdpowiedz.transform.localScale = new Vector3(1, 1, 1);
    }
    void CustomButton_onClick0()
    {
        if (!poznaniPodejrzaniNPC.Contains(npcRef) && npcRef.podejrzany == true)
        {
            poznaniPodejrzaniNPC.Add(npcRef);
        }

        if (npcOdpowiedz.active == false)
        {
            npcOdpowiedz.SetActive(true);
        }

        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.mozliwePytania[0].odpowiedz;
        for(int i =0; i< progressStory.Length; i++)
        {
            if (npcRef.mozliwePytania[0].progressList[i] == true)
            {
                UpdateProgress(i);
            }
        }
        //if(npcRef.mozliwePytania[0].progress == true)
        //{
        //    UpdateProgress();
        //}
        
        ChangeSizeOfImageOdpowiedzi(0);

        npcOdpowiedz.transform.localScale = new Vector3(1, 1, 1);

        // update questions
        foreach(NPC npc in allNPC)
        {
            //Debug.Log(npc.nazwa + "  " + npcRef.mozliwePytania[0].nazwaNPC);
            if(npc.name == npcRef.mozliwePytania[0].nazwaNPC)
            {
                
                npc.mozliweNotatki.Add(new Note(npcRef.mozliwePytania[0].pytanie, npcRef.mozliwePytania[0].odpowiedz, npcRef.mozliwePytania[0].nazwaNPC));

                if(npcRef.mozliwePytania[0].storyUpdate != 0)
                {
                    gameObject.GetComponent<Story>().UpdateStory(npcRef.mozliwePytania[0].storyUpdate);
                }               
                break;
            }
        }
        npcRef.mozliwePytania.Remove(npcRef.mozliwePytania[0]);
        Refresh();
    }

    void CustomButton_onClick1()
    {
        if (!poznaniPodejrzaniNPC.Contains(npcRef) && npcRef.podejrzany == true)
        {
            poznaniPodejrzaniNPC.Add(npcRef);
        }

        if (npcOdpowiedz.active == false)
        {
            npcOdpowiedz.SetActive(true);
        }

        for (int i = 0; i < progressStory.Length; i++)
        {
            if (npcRef.mozliwePytania[1].progressList[i] == true)
            {
                UpdateProgress(i);
            }
        }

        ChangeSizeOfImageOdpowiedzi(1);

        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.mozliwePytania[1].odpowiedz;
        npcOdpowiedz.transform.localScale = new Vector3(1, 1, 1);

        // update questions
        foreach (NPC npc in allNPC)
        {
            if (npc.name == npcRef.mozliwePytania[1].nazwaNPC)
            {
                //Debug.Log(npcRef.mozliwePytania[1].storyUpdate);
                if (npcRef.mozliwePytania[1].storyUpdate != 0)
                {
                    gameObject.GetComponent<Story>().UpdateStory(npcRef.mozliwePytania[1].storyUpdate);
                }
                npc.mozliweNotatki.Add(new Note(npcRef.mozliwePytania[1].pytanie, npcRef.mozliwePytania[1].odpowiedz, npcRef.mozliwePytania[1].nazwaNPC));
                break;
            }
        }
        npcRef.mozliwePytania.Remove(npcRef.mozliwePytania[1]);
        Refresh();
    }

    void CustomButton_onClick2()
    {
        if (!poznaniPodejrzaniNPC.Contains(npcRef) && npcRef.podejrzany == true)
        {
            poznaniPodejrzaniNPC.Add(npcRef);
        }
        if (npcOdpowiedz.active == false)
        {
            npcOdpowiedz.SetActive(true);
        }
        for (int i = 0; i < progressStory.Length; i++)
        {
            if (npcRef.mozliwePytania[2].progressList[i] == true)
            {
                UpdateProgress(i);
            }
        }
        ChangeSizeOfImageOdpowiedzi(2);

        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.mozliwePytania[2].odpowiedz;
        npcOdpowiedz.transform.localScale = new Vector3(1, 1, 1);

        // update questions
        foreach (NPC npc in allNPC)
        {
            if (npc.name == npcRef.mozliwePytania[2].nazwaNPC)
            {

                if (npcRef.mozliwePytania[2].storyUpdate != 0)
                {
                    gameObject.GetComponent<Story>().UpdateStory(npcRef.mozliwePytania[2].storyUpdate);
                }
                npc.mozliweNotatki.Add(new Note(npcRef.mozliwePytania[2].pytanie, npcRef.mozliwePytania[2].odpowiedz, npcRef.mozliwePytania[2].nazwaNPC));
                break;
            }
        }
        npcRef.mozliwePytania.Remove(npcRef.mozliwePytania[2]);
        Refresh();
    }

    void CustomButton_onClick3()
    {
        if (!poznaniPodejrzaniNPC.Contains(npcRef) && npcRef.podejrzany == true)
        {
            poznaniPodejrzaniNPC.Add(npcRef);
        }
        if (npcOdpowiedz.active == false)
        {
            npcOdpowiedz.SetActive(true);
        }
        for (int i = 0; i < progressStory.Length; i++)
        {
            if (npcRef.mozliwePytania[3].progressList[i] == true)
            {
                UpdateProgress(i);
            }
        }
        ChangeSizeOfImageOdpowiedzi(3);

        npcOdpowiedz.GetComponentInChildren<TextMeshProUGUI>().text = npcRef.mozliwePytania[3].odpowiedz;
        npcOdpowiedz.transform.localScale = new Vector3(1, 1, 1);

        // update questions
        foreach (NPC npc in allNPC)
        {
            if (npc.name == npcRef.mozliwePytania[3].nazwaNPC)
            {

                if (npcRef.mozliwePytania[3].storyUpdate != 0)
                {
                    gameObject.GetComponent<Story>().UpdateStory(npcRef.mozliwePytania[3].storyUpdate);
                }
                npc.mozliweNotatki.Add(new Note(npcRef.mozliwePytania[3].pytanie, npcRef.mozliwePytania[3].odpowiedz, npcRef.mozliwePytania[3].nazwaNPC));
                break;
            }
        }
        npcRef.mozliwePytania.Remove(npcRef.mozliwePytania[3]);
        Refresh();
    }
   


}

