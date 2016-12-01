using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Animator anim;

    float x, y, z;
    float anglex, angley, anglez;

    public int levens = 10;

    string area = "safe";

    bool walk;
    bool run;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();

        transform.rotation = Quaternion.Euler(0, angley, 0);
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
    }

    void HandleInput()
    {

        // levens
        if (levens == 0)
        {
            anim.SetTrigger("Die");
        }
        // running animations
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.04f;
            run = true;
            walk = false;
        }

        // walking animations
        else if (Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.W))
            {
                angley += 4;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                angley += 4;
                transform.position -= transform.forward * 0.02f;
            }
            else
            {
                transform.position += transform.forward * 0.005f;
                angley += 8;
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                angley -= 4;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                angley -= 4;
                transform.position -= transform.forward * 0.02f;
            }
            else
            {
                transform.position += transform.forward * 0.005f;
                angley -= 8;
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.02f;
            walk = true;
            run = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.02f;
            walk = true;
            run = false;
        }
        
        else
        {
            walk = false;
            run = false;
        }

        }
    
}
