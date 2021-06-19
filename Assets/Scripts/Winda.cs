using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Winda : MonoBehaviour
{
    public Animator windaL;
    public Animator windaR;
    public GameObject canvas;
    public PlayerController player;
    public NavMeshAgent playerAgent;
    public GameObject pietro0Link;
    public GameObject pietro1Link;
    public Material material;
    private bool moveTo0;
    private bool moveTo1;
    // Start is called before the first frame update
    void Start()
    {      
         windaL.SetBool("Open", false);
         windaR.SetBool("Open", false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTo0 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 17.9f, transform.position.z), 10f * Time.deltaTime);
            if(transform.position.y <= -17.9f)
            {
                moveTo0 = false;
                canvas.SetActive(true);
                player.canMove = true;
                playerAgent.speed += 1;
                material.color = new Color(1f, 1f, 1f, 0.1f);
            }
        }
        else if (moveTo1 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 17.9f, transform.position.z), 10f * Time.deltaTime);
            if (transform.position.y >= 0f)
            {
                moveTo1 = false;
                canvas.SetActive(true);
                player.canMove = true;
                playerAgent.speed += 1;
                material.color = new Color(1f, 1f, 1f, 0.1f);
            }
        }
    }

    public void MoveTo0()
    {
        if(transform.position.y > -17.9f)
        {
            material.color =new Color (1f, 1f, 1f, 1f);
            player.transform.position = pietro1Link.transform.position;
            moveTo0 = true;
            canvas.SetActive(false);
            playerAgent.speed -= 1;
            player.canMove = false;
            playerAgent.SetDestination(pietro0Link.transform.position);
            player.pietro = 0;
            // set active NPC
            foreach (NPC all in GameManager.Instance.allNPC)
            {
                all.SetActiveNPC();
            }
        }
        
    }

    public void MoveTo1()
    {
        if(transform.position.y < 0)
        {
            material.color = new Color(1f, 1f, 1f, 1f);
            player.transform.position = pietro0Link.transform.position;
            moveTo1 = true;
            canvas.SetActive(false);
            playerAgent.speed -= 1;
            player.canMove = false;
            playerAgent.SetDestination(pietro1Link.transform.position);
            player.pietro = 1;
            // set active NPC
            foreach (NPC all in GameManager.Instance.allNPC)
            {
                all.SetActiveNPC();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            windaL.SetBool("Open", true);
            windaR.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            windaL.SetBool("Open", false);
            windaR.SetBool("Open", false);
        }
        
    }
}
