using UnityEngine;

public class BossAI : MonoBehaviour
{
     public int iLives = 2401;
     private GameObject goTarget = null;
     protected float fDistance = 0;
    public float fAttackRange = 2.15f;
    public float fPerceptionRange = 35;
     private float fLastAttack = 0;
     private float fAttackDelay = 0.75f;
    public int iMinAttackMilliseconds = 100;
    public int iMaxAttackMilliseconds = 250;
    
     private Animator anim = null;
    public float fRunSpeed = 0.45f;

     private float fSpawnDelay = -1;

    void Start()
    {
        goTarget = GameObject.FindGameObjectWithTag("Player");
        if (goTarget == null)
            Debug.Log("The Boss couldn't find a player.");

        fLastAttack = 0;
        fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;

        anim = GetComponent<Animator>();

        iLives = 0;
    }

    /* ====== SPAWN ====== */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && iLives == 0)
        {
            Debug.Log("The Boss is waking...");
            anim.SetTrigger("Spawn");
            fSpawnDelay = Time.time + 2;

            // Weg met de trigger-collider voor performance en om aanval-bugs te voorkomen (zodat dit niet blijft aanroepen)
            Destroy(GetComponent<BoxCollider>());
        }
    }

    private void Update()
    {
        if (fSpawnDelay > 0 && fSpawnDelay - Time.time <= 0)
        {
            iLives = 3;
            Debug.Log("The Boss has awoken!");
            fSpawnDelay = -1;
        }
    }

    void FixedUpdate()
    {
        if (goTarget.GetComponent<Player>())
        {
            fDistance = CalculateDistance(goTarget);

            /* ====== MELEE ====== */
            if (fDistance < fAttackRange)
            {
                //if (iLives == 1)
                //    fAttackDelay /= 2;

                if (Time.time - fLastAttack > fAttackDelay)
                {
                    anim.SetTrigger("Smash");

                    goTarget.GetComponent<Collider>().SendMessage("LifeLoss");

                    fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                    fLastAttack = Time.time;
                }
            }
            /* ====== RANGE ====== */
            else if (fDistance < fPerceptionRange)
            {
                float angle = Mathf.Atan2(-(goTarget.transform.position.x - transform.position.x), goTarget.transform.position.z - transform.position.z);
                //float xspeed = Mathf.Sin(angle) * fRunSpeed;
                //float zspeed = Mathf.Cos(angle) * fRunSpeed;

                transform.rotation = Quaternion.Euler(0, -angle * 180 / Mathf.PI, 0);

                if (Time.time - fLastAttack > fAttackDelay)
                {
                    anim.SetTrigger("Throw");

                    //goTarget.GetComponent<Collider>().SendMessage("LifeLoss");

                    fAttackDelay = Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                    fLastAttack = Time.time;
                }

                //transform.position += new Vector3(-xspeed, 0, zspeed);
            }
            /* ====== IDLE ====== */
            else
                anim.SetBool("Walk", false);
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
        Debug.Log("The Boss got hit, " + iLives + " lives left!");
        iLives--;

        /* ====== PAIN ===== */
        if (iLives > 0)
            anim.SetTrigger("Hit");
        /* ====== DEATH ====== */
        else
        {
            anim.SetTrigger("Death");
            // Destroy RB en CC zodat je over de lijken heen loopt.
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<BossAI>());
            // We laten het model en alles wel staan zodat je de boss nog wel kan zien vallen en liggen
        }
    }

    /* ====== GET HIT ====== */
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().bAttack && Vector3.Angle(other.transform.forward, transform.position - other.transform.position) < 15)
            LifeLoss();
    }
}
