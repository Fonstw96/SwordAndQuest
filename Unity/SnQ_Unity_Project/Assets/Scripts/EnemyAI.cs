using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int id = 0;   // for debugging
    public int iLives = 2;
     private GameObject goTarget;
     protected float fDistance = 0;
    public float fAttackRange = 2;
    public float fPerceptionRange = 15;
     private float fLastAttack;
     private float fAttackDelay = 0.75f;
     private int iAttackSequence = -1;
    public int iMinAttackMilliseconds = 200;
    public int iMaxAttackMilliseconds = 300;
    
    public bool isdead = false;
    public bool dummy = false;
     private Animator anim;
    public float fRunSpeed = 0.15f;

    void Start()
    {
        transform.Rotate(0, Random.Range(0, 360), 0);

        goTarget = GameObject.FindGameObjectWithTag("Player");
        if (goTarget == null)
            Debug.Log("Enemy " + id + " couldn't find a player!");

        fLastAttack = 0;
        fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
        
        anim = GetComponent<Animator>();

        fDistance = CalculateDistance(goTarget);
	}

    void FixedUpdate()
    {
        /* ====== DIE ====== */
        if (iLives <= 0)
        {
            isdead = true;
            anim.SetTrigger("Die");
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponent<SphereCollider>());
        }
        /* ====== LIFE ====== */
        else if (goTarget.GetComponent<Player>().levens > 0)
        {
            fDistance = CalculateDistance(goTarget);

            /* ====== ATTACK ====== */
            if (fDistance < fAttackRange)
            {
                anim.SetBool("Run", false);

                if (Time.time - fLastAttack > fAttackDelay)
                {
                    iAttackSequence++;
                    if (iAttackSequence == 1)
                        anim.SetTrigger("Attack2");
                    else
                        anim.SetTrigger("Attack1");

                    if (iAttackSequence >= 2)
                        iAttackSequence = -1;
                    
                    if (!dummy)
                        goTarget.GetComponent<Player>().LifeLoss(1);
                    else
                        goTarget.GetComponent<Player>().LifeLoss(0);

                    fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                    fLastAttack = Time.time;
                }
            }
            /* ====== CHASE ====== */
            else if (fDistance < fPerceptionRange)
            {
                anim.SetBool("Run", true);

                float angle = Mathf.Atan2(-(goTarget.transform.position.x - transform.position.x), goTarget.transform.position.z - transform.position.z);
                float xspeed = Mathf.Sin(angle) * fRunSpeed;
                float zspeed = Mathf.Cos(angle) * fRunSpeed;

                transform.rotation = Quaternion.Euler(0, -angle * 180 / Mathf.PI, 0);

                transform.position += new Vector3(-xspeed, 0, zspeed);
            }
            /* ====== IDLE ====== */
            else
            {
                anim.SetBool("Run", false);
                //anim.SetBool("Walk", false);
            }
        }
    }

    private float CalculateDistance(GameObject Target)
    {
        float x_dist = this.transform.position.x - Target.transform.position.x;
        x_dist *= x_dist;
        float y_dist = this.transform.position.y - Target.transform.position.y;
        y_dist *= y_dist;
        float z_dist = this.transform.position.z - Target.transform.position.z;
        z_dist *= z_dist;
        
        return Mathf.Sqrt(x_dist + y_dist + z_dist);
    }

    private void LifeLoss()
    {
        //Debug.Log("Enemy " + id + " got hit, " + iLives + " lives left!");
        iLives--;

        /* ====== PAIN ===== */
        if (iLives > 0)
            anim.SetTrigger("Hit");
        /* ====== DEATH ====== */
        else
        {
            GameObject Master = null;
            Master = GameObject.FindGameObjectWithTag("Boss");
            if (Master != null)
                Master.SendMessage("MinionDied");

            anim.SetTrigger("Die");
            // Destroy RB en CC zodat je over de lijken heen kan lopen.
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<CapsuleCollider>());
            // Destroy script voor performance
            Destroy(GetComponent<EnemyAI>());
            // We laten het model en alles wel staan zodat je de enemy nog wel kan zien vallen en liggen
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - fLastAttack > iMaxAttackMilliseconds)
    	    fLastAttack = Time.time + 1;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().bAttack && Vector3.Angle(other.transform.forward, transform.position - other.transform.position) < 30)
            LifeLoss();
    }
}