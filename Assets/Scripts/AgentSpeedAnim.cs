using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpeedAnim : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeedZero()
    {
        agent.speed = 0;
    }
    public void SetSpeedWalk()
    {
        agent.speed = 6;
    }

    public void SetSpeedSkate()
    {
        agent.speed = 30;
    }

    public void PushOnce()
    {
        player.SetBool("Push", false);
    }
}
