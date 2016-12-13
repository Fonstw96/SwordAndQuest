using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public int id = 0;
    public int iLives = 2;
     private GameObject goTarget;
     protected float fDistance = 0;
    public float fAttackRange = 2;
    public float fPerceptionRange = 15;
     private float fLastAttack;
     private float fAttackDelay = 0.75f;
    public int iMinAttackMilliseconds = 100;
    public int iMaxAttackMilliseconds = 250;
     private int iRand = 0;

     private Animator anim;
    public float fRunSpeed = 0.45f;

    void Start ()
    {
        goTarget = GameObject.FindGameObjectWithTag("Player");

        fLastAttack = 0;
        fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
        
        anim = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();

        fDistance = CalculateDistance(goTarget);
	}

    void FixedUpdate()
    {
    }

    void Update()
    {
        /* ====== DIE ====== */
        if (iLives < 0)
        {
            //if (anim.GetInteger("moving") > 0/* && anim.GetInteger("moving") < 12*/)
            //    anim.SetInteger("moving", 0);
        }
        else if (iLives == 0)
        {
            iRand = Random.Range(1, 3);
            //anim.SetInteger("battle", 0);
            //anim.SetInteger("moving", 11 + iRand);

            iLives = -1;
        }
        /* ====== LIFE ====== */
        else
        {
            fDistance = CalculateDistance(goTarget);

            /* ====== ATTACK ====== */
            if (fDistance < fAttackRange)
            {
                if (iRand != 0)
                    iRand = 0;
                //else
                //    anim.SetInteger("moving", 0);

                if (Time.time - fLastAttack > fAttackDelay)
                {
                    iRand = Random.Range(1, 3);
                    //anim.SetInteger("moving", 2 + iRand);

                    goTarget.GetComponent<Collider>().SendMessage("LifeLoss");

                    fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                    fLastAttack = Time.time;
                }
            }
            /* ====== CHASE ====== */
            else if (fDistance < fPerceptionRange)
            {
                //if (anim.GetInteger("battle") == 0)
                //    anim.SetInteger("battle", 1);
                //if (anim.GetInteger("moving") > 2)
                //    anim.SetInteger("moving", 0);
                //else if (anim.GetInteger("moving") == 0)
                //    anim.SetInteger("moving", 2);

                float angle = Mathf.Atan2(-(goTarget.transform.position.x - transform.position.x), goTarget.transform.position.z - transform.position.z);
                float xspeed = Mathf.Sin(angle) * fRunSpeed;
                float zspeed = Mathf.Cos(angle) * fRunSpeed;

                transform.rotation = Quaternion.Euler(0, -angle * 180 / Mathf.PI, 0);

                transform.position += new Vector3(-xspeed, 0, zspeed);
            }
            /* ====== IDLE ====== */
            else
            {
                //if (anim.GetInteger("moving") != 0)
                //    anim.SetInteger("moving", 0);
                //if (anim.GetInteger("battle") == 1)
                //    anim.SetInteger("battle", 0);
            }
        }
    }

    private float CalculateDistance(GameObject Target)
    {
        float x_dist = this.transform.position.x - goTarget.transform.position.x;
        x_dist *= x_dist;
        float y_dist = this.transform.position.y - goTarget.transform.position.y;
        y_dist *= y_dist;
        float z_dist = this.transform.position.z - goTarget.transform.position.z;
        z_dist *= z_dist;
        
        return Mathf.Sqrt(x_dist + y_dist + z_dist);
    }

    private void LifeLoss()
    {
        if (iLives > 0)
        {
            iLives--;

            if (iLives > 0)
            {
                iRand = Random.Range(1, 3);
                //anim.SetInteger("moving", 9 + iRand);
            }
        }
    }
}
