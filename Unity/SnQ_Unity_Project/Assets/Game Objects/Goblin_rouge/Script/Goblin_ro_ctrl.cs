using UnityEngine;
using System.Collections;

public class Goblin_ro_ctrl : MonoBehaviour
{
	 private Animator anim;
	 private CharacterController controller;
	 private bool battle_state;
	public float speed = 6.0f;
	public float runSpeed = 1.7f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
	 private Vector3 moveDirection = Vector3.zero;
	
	void Start ()
    {
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) //attack1
		{
			anim.SetInteger("moving", 3);
		}
		else if (Input.GetMouseButtonDown (1)) //attack2
		{
			anim.SetInteger("moving", 4);
		}
		else if (Input.GetMouseButtonDown (2)) //cast
		{
			anim.SetInteger("moving", 5);
		}
		
		if (Input.GetKeyDown("space")) //jump
		{
			anim.SetInteger("moving", 7);
		}
		
		
		if (Input.GetKeyDown("o")) //die_1
		{
			anim.SetInteger("moving", 12);
		}
		else if (Input.GetKeyDown("i")) //die_2
		{
			anim.SetInteger("moving", 13);
		}
		
		if (Input.GetKeyDown("u")) //defence
		{
			int n = Random.Range(0,2);
			if (n == 0)
				anim.SetInteger("moving", 10);
            else
                anim.SetInteger("moving", 11);
		}
		
		
		
		
		if(controller.isGrounded)
		{
			moveDirection=transform.forward * Input.GetAxis ("Vertical") * speed * runSpeed;
		}
		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
		
	}
}

