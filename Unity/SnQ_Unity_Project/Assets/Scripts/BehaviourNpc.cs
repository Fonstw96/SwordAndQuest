﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourNpc : MonoBehaviour {

    Animator anim;
    public bool isWalking;

    public float speed = 2f;

    public Transform path;
    private List<Transform> nodes;
    public int currentNode = 0;
    private float reachDistance = 0f;

   

    void Start () {
        anim = GetComponent<Animator>();

        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }


    void MoveTowards()
    {
            float dist = Vector3.Distance(nodes[currentNode].position, transform.position);


            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, step);

            transform.LookAt(nodes[currentNode].position);

            if (dist <= reachDistance)
            {
                currentNode++;
            }
<<<<<<< HEAD

            if (currentNode == nodes.Count)
        {
<<<<<<< HEAD
=======
            currentNode = 0;
        }
=======
>>>>>>> parent of f5ca85a... bye bye
    }


	void Update () {
	    if (isWalking)
        {
>>>>>>> master
            anim.SetBool("walk", true);
<<<<<<< HEAD
            MoveTowards();
=======
>>>>>>> parent of f5ca85a... bye bye
        } else if (!isWalking)
        {
            anim.SetBool("walk", false);
        }

	}
	


}
