using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float x, y, z;
    private float anglex, angley, anglez;


	// Use this for initialization
	void Start () {

        this.transform.rotation = Quaternion.Euler(anglex, angley, anglez);
        
	}
	
	// Update is called once per frame
	void Update () {

        angley = Input.GetAxis("Horizontal");
        anglex = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.D))
        {
            angley += .6f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            angley -= .6f;
        }
    }
}
