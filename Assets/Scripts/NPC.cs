using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject canvasImieNazwisko;
    private TextMeshProUGUI canvasText;
    public int storyLineNumber;
    public QuestionNPC[] questions;
    public QuestionNPC basic;
    public bool rozmowa;
    public int pietro;
    public bool podejrzany;
    public Transform currentTarget;
    public List<QuestionNPC> mozliwePytania;
    public GameObject npcGraphic;
    public List<Note> mozliweNotatki;
    public List<WayPoints> wayPoints;
    private int wayPointsIterator;
    private int checkNumber;
    private bool canMove;



    private void Start()
    {
        canMove = true;
        canvasText = canvasImieNazwisko.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        /*for(int i = 0; i<questions.Length; i++)
        {
            if(questions[i].odpowiedz.Length > 175)
            {
                buttonSizeOdpowiedzi.Add(2);
            }
            else if(questions[i].odpowiedz.Length > 70)
            {
                buttonSizeOdpowiedzi.Add(1);
            }
            else
            {
                buttonSizeOdpowiedzi. = 0;
            }

            if(questions[i].pytanie.Length > 175)
            {
                buttonSizePytania[i] = 2;
            }
            else if (questions[i].pytanie.Length > 70)
            {
                buttonSizePytania[i] = 1;
            }
            else
            {
                buttonSizePytania[i] = 0;
            }

            
        }
        
        for(int i = 0; i<questions.Length;i++)
        {
            for(int j = 0; j<questions[i].wymaganyPoziom.Length;j++)
            {
                if (questions[i].wymaganyPoziom[j] == 0)
                {
                    mozliwePytania.Add(questions[i]);
                }
            }
            
        }
        */

        QuestionsUpdate(GameManager.Instance.progressStory);
        SetActiveNPC();
    }
    private void Update()
    {
        if(currentTarget == null)
        {
            SetPointsMovement();
        }
        else if(agent.remainingDistance < 0.15f && canMove == true)
        {
            currentTarget = null;
            canMove = false;
        }

        if(rozmowa == true)
        {
            Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
        }
    }

    public void QuestionsUpdate(int[] progress)
    {
        for (int i = 0; i < questions.Length; i++)
        {
            for(int j = 0; j<questions[i].wymaganyPoziom.Length;j++)
            {
                if (questions[i].wymaganyPoziom[j] == progress[j])
                {
                    checkNumber++;
                }
            }
            //Debug.Log(checkNumber);
            if(checkNumber == questions[i].wymaganyPoziom.Length)
            {
                mozliwePytania.Add(questions[i]);
            }
            checkNumber = 0;
           
        }
    }
    /*public void QuestionsUpdate(int progress)
    {
        for (int i = 0; i < questions.Length; i++)
        {
            if (questions[i].wymaganyPoziom == progress)
            {
                mozliwePytania.Add(questions[i]);
            }
        }
    }
    */
    private void SetPointsMovement()
    {
        currentTarget = wayPoints[wayPointsIterator].point;
        if(wayPoints[wayPointsIterator].whatToDo == WayPoints.whatToDO.nothing)
        {
            agent.SetDestination(currentTarget.position);
            canMove = true;
        }
        else if (wayPoints[wayPointsIterator].whatToDo == WayPoints.whatToDO.wait)
        {
            StartCoroutine(WaitNPCCoroutine(wayPoints[wayPointsIterator].timeToWait));
        }
        else if (wayPoints[wayPointsIterator].whatToDo == WayPoints.whatToDO.work)
        {
            agent.SetDestination(currentTarget.position);
            canMove = true;
        }        
        if (wayPointsIterator < wayPoints.Count -1)
            wayPointsIterator++;
        else
            wayPointsIterator = 0;
    }
    IEnumerator WaitNPCCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        agent.SetDestination(currentTarget.position);
        canMove = true;
    }

    public void SetTalkWithPlayer()
    {
        rozmowa = false;
        agent.SetDestination(PlayerController.Instance.transform.position);

    }
    public void SetActiveNPC()
    {
        // set active NPC

        if (pietro == PlayerController.Instance.pietro)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    void OnMouseEnter()
    {
        canvasImieNazwisko.SetActive(true);
        if(mozliweNotatki.Any())
        {
            canvasText.text = gameObject.name;
        }
        else
        {
            canvasText.text = "?";
        }
        transform.GetComponent<Outline>().OutlineWidth = 1.7f;
    }
    void OnMouseExit()
    {
        canvasImieNazwisko.SetActive(false);
        transform.GetComponent<Outline>().OutlineWidth = 0f;
    }
}
