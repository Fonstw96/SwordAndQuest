using UnityEngine;
using System.Collections;

public class BehaviourNpc : MonoBehaviour {

    Animator anim;
    public bool isWalking;

    void Start () {
        anim = GetComponent<Animator>();
    }
	
	
	void Update () {
	    if (isWalking)
        {
            anim.SetBool("walk", true);
        } else if (!isWalking)
        {
            anim.SetBool("walk", false);
        }
	}


}
