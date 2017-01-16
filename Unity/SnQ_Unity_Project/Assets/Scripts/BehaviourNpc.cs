using UnityEngine;
using System.Collections;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> origin/Fons

public class BehaviourNpc : MonoBehaviour {

    Animator anim;
    public bool isWalking;

<<<<<<< HEAD
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
    }


	void Update () {
	    if (isWalking)
        {
            MoveTowards();
=======
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	
	void Update () {
	    if (isWalking)
        {
>>>>>>> origin/Fons
            anim.SetBool("walk", true);
        } else if (!isWalking)
        {
            anim.SetBool("walk", false);
        }
<<<<<<< HEAD

	}

=======
	}


>>>>>>> origin/Fons
}
