using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Singleton<PlayerController>
{
    public int pietro;
    public bool walk;
    public Camera cam;
    public NavMeshAgent agent;
    public bool canMove;
    public Animator animPostac;
    public Animator animDeskorolka;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(-horInput, 0f, -verInput);
        Vector3 moveDestination = transform.position + movement;
        agent.speed = 10;
        agent.destination = moveDestination;
         */

        if(walk == true)
        {
            if (Input.GetMouseButtonDown(0) && canMove == true)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);

                    
                }
            }
        }
        else
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(-horInput, 0f, -verInput);
            Vector3 moveDestination = transform.position + movement;
            agent.destination = moveDestination;
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {            
            AnimDeskorolkaPush();           
        }

        if(walk == false && agent.speed > 0)
        {
            agent.speed -= 1.5f * Time.deltaTime;
            if(agent.remainingDistance < 0.15f)
            {
                agent.speed = 0.5f;
            }
        }
        //animation
        if(walk == true)
        {
            if (agent.remainingDistance < 0.15f)
            {
                AnimIdle();
            }
            else
            {
                AnimWalk();
            }
        }
        
        
    }
    public void AnimIdle()
    {
        animPostac.SetFloat("Walk", 0);
    }

    public void AnimWalk()
    {
        animPostac.SetFloat("Walk", 2);
    }

    public void AnimDeskorolkaStart()
    {
        animPostac.SetBool("Deskorolka", true);
        animDeskorolka.SetBool("Deskorolka", true);
    }

    public void AnimDeskorolkaPush()
    {
        animPostac.SetBool("Push", true);
    }

    public void AnimDeskorolkaKoniec()
    {
        animPostac.SetBool("Deskorolka", false);
        animDeskorolka.SetBool("Deskorolka", false);
        agent.speed = 0.5f;
    }

    public void SetSpeed0()
    {

    }
}
