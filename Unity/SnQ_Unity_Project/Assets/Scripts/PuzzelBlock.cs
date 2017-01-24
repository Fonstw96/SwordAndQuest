using UnityEngine;
using System.Collections;

public class PuzzelBlock : MonoBehaviour
{
    private Rigidbody rb = null;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        rb.AddRelativeForce(-rb.velocity * 12);
    }
}
