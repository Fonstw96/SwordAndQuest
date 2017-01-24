using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeEvent : MonoBehaviour {


    public GameObject npc;
    
    // word later veranderd in een list maak je geen zorgen!
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;


    private bool nodeEventFound = false;
    private bool nodeEventStarted = false;
    private bool nodeEventComplete = false;

    private int currentnode;

    public bool EndEvent;
    private bool pathComplete;
    public bool EnemyEvent;

	void Start () {

    }

	void Update () {

        currentnode = npc.gameObject.GetComponent<BehaviourNpc>().currentNode;

        if (nodeEventFound && !nodeEventStarted && EnemyEvent)
        {
            if (currentnode == 4)
            {
                enemyPrefab1.SetActive(true);
                enemyPrefab2.SetActive(true);
                enemyPrefab3.SetActive(true);
                nodeEventStarted = true;
            }
            else if (currentnode == 7)
            {
                enemyPrefab1.SetActive(true);
                enemyPrefab2.SetActive(true);
                enemyPrefab3.SetActive(true);
                nodeEventStarted = true;
            }
            else if (currentnode == 10)
            {
                enemyPrefab1.SetActive(true);
                enemyPrefab2.SetActive(true);
                enemyPrefab3.SetActive(true);
                nodeEventStarted = true;
            }
        }

        if (currentnode == 4 && nodeEventStarted)
        {
            if (enemyPrefab1.gameObject.GetComponent<EnemyAI>() == null && enemyPrefab2.gameObject.GetComponent<EnemyAI>() == null && enemyPrefab3.gameObject.GetComponent<EnemyAI>() == null)
            {
                nodeEventComplete = true;
            }
        } else if (currentnode == 7 && nodeEventStarted)
        {
            if (enemyPrefab1.gameObject.GetComponent<EnemyAI>() == null && enemyPrefab2.gameObject.GetComponent<EnemyAI>() == null && enemyPrefab3.gameObject.GetComponent<EnemyAI>() == null)
            {
                nodeEventComplete = true;
            }
        } else if (currentnode == 10 && nodeEventStarted)
        {
            if (enemyPrefab1.gameObject.GetComponent<EnemyAI>() == null && enemyPrefab2.gameObject.GetComponent<EnemyAI>() == null && enemyPrefab3.gameObject.GetComponent<EnemyAI>() == null)
            {
                nodeEventComplete = true;
            }
        }

        if (nodeEventComplete)
        {
            npc.gameObject.GetComponent<BehaviourNpc>().isWalking = true;
            nodeEventStarted = false;
            nodeEventFound = false;
            nodeEventComplete = false;
            Debug.Log("isWaling true door nodeEvent nodeEventComplete");
        }


    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "EscortNpc")
        {

            if (EnemyEvent)
            {
            Debug.Log("NPC collided with EventNode");
            other.gameObject.GetComponent<BehaviourNpc>().isWalking = false;
            nodeEventFound = true;
            npc = other.gameObject;
            }

            else if (EndEvent)
            {
                other.gameObject.GetComponent<BehaviourNpc>().isWalking = false;
            }

        }
    }
}
