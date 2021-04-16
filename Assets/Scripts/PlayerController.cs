using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    public bool canMove;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canMove == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        //animation
        if (agent.remainingDistance < 0.15f)
        {
            AnimIdle();
        }
        else
        {
            AnimWalk();
        }
    }
    public void AnimIdle()
    {
        anim.SetFloat("Speed", 0);
    }

    public void AnimWalk()
    {
        anim.SetFloat("Speed", 2);
    }
}
