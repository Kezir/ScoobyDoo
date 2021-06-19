using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class NotesManager : Singleton<NotesManager>
{
    public TextMeshProUGUI notatki;
    public GameObject postac;
    public GameObject NPCPosition;
    public Material material;
    public int currentNPC;
    public int currentPage;
    public List<string> notes;
    private int maxPage = 0;
    private GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
        currentNPC = 0;
        currentPage = 0;
    }

    public void Change()
    { 
        //Debug.Log(GameManager.Instance.poznaniPodejrzaniNPC);
        if (GameManager.Instance.poznaniPodejrzaniNPC.Any())
        {
            currentPage = 0;
            Destroy(npc);
            npc = Instantiate(GameManager.Instance.poznaniPodejrzaniNPC[currentNPC].npcGraphic, NPCPosition.transform.position, Quaternion.Euler(-30f, -33.4f, 0f));
            npc.transform.SetParent(gameObject.transform);
            npc.transform.localScale = new Vector3(200, 200, 200);
            npc.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = material;

            UpdateTextChange();
     
        }
       
    }

    private void UpdateTextChange()
    {
        
        notes = new List<string>();
        maxPage = 0;
        notes.Add("");        
        foreach(Note note in GameManager.Instance.poznaniPodejrzaniNPC[currentNPC].mozliweNotatki)
        {
            if (notes[maxPage].Length > 300)
            {
                notes.Add("");
                maxPage++;
            }
            notes[maxPage] += note.pytanie + "\n" + "( " + note.ktoPowiedzial + " ) " + note.odpowiedz + "\n\n";
            
        }
        notatki.text = notes[currentPage];
    }


    public void NextPage()
    {
        if(GameManager.Instance.poznaniPodejrzaniNPC.Any())
        {
            if (currentPage < notes.Count-1)
            {
                currentPage++;
                notatki.text = notes[currentPage];
            }
            else
            {
                currentPage = 0;
                notatki.text = notes[currentPage];
            }
        }
        
    }

    public void NextNPC()
    {
        if (currentNPC < GameManager.Instance.poznaniPodejrzaniNPC.Count-1)
        {
            currentNPC++;
            Change();
        }
        else
        {
            currentNPC = 0;
            Change();
        }
    }

    public void PreviousNPC()
    {
        if (currentNPC <= GameManager.Instance.poznaniPodejrzaniNPC.Count-1 && currentNPC >= 1)
        {
            currentNPC--;
            Change();
        }
        else
        {
            currentNPC = GameManager.Instance.poznaniPodejrzaniNPC.Count-1;
            Change();
        }
    }

    public void OpenNotes()
    {
        if(gameObject.active == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
    }
}
