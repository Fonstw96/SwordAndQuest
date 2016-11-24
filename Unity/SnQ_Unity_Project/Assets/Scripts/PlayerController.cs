using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    float x, y, z;
    float anglex, angley, anglez;


	// Use this for initialization
	void Start () {

        
        
	}
	
	// Update is called once per frame
	void Update () {

        HandleInput();

        transform.rotation = Quaternion.Euler(0, angley, 0);

    }
    
    void HandleInput()
    {

        angley -= Input.GetAxis("Horizontal");



        if (Input.GetKey(KeyCode.D))
        {
            //angley += 60;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //angley -= 6;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.2f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.2f;
        }
    }
}
