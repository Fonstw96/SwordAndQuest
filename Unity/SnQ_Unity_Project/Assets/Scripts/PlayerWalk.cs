using UnityEngine;
using System.Collections;

public class PlayerWalk : MonoBehaviour
{
    public int iLives = 3;
    public float fSpeed = 0.6f;
     private int iDelay = 100;
     private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical   = Input.GetAxis("Vertical");

        transform.position += new Vector3(moveHorizontal * fSpeed, 0, moveVertical * fSpeed);
    }
}