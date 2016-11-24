using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
     private GameObject goTarget;
     private Rigidbody rb;
    public int id = 0;
    public int iLives = 2;
     protected float fDistance;
    public float fSpeed = 3;
    public float fPerceptionRange = 15;
    
	void Start ()
    {
        if (goTarget == null)
            goTarget = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

        fDistance = CalculateDistance(goTarget);
	}
	
	void Update ()
    {
	}

    void FixedUpdate()
    {
        fDistance = CalculateDistance(goTarget);

        if (fDistance < 2)
        {
            // attack
        }
        else if (fDistance < fPerceptionRange)
        {
            Vector3 direction = goTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            transform.position += direction * fSpeed * Time.deltaTime;
        }
    }

    float CalculateDistance(GameObject Target)
    {
        float x_dist = this.transform.position.x - goTarget.transform.position.x;
        x_dist *= x_dist;
        float y_dist = this.transform.position.y - goTarget.transform.position.y;
        y_dist *= y_dist;
        float z_dist = this.transform.position.z - goTarget.transform.position.z;
        z_dist *= z_dist;

        float return_value = Mathf.Sqrt(x_dist + y_dist + z_dist);
        Debug.Log("Enemy ID " + id + " distance is " + return_value);
        return return_value;
    }
}
