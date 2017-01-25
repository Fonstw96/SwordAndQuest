using UnityEngine;
using System;

public class BossAI : MonoBehaviour
{
    public int iLives = 2401;
     private Rigidbody rb = null;
     private GameObject goTarget = null;
     private Player cPlayer = null;
     protected float fDistance = 0;
    public float fAttackRange = 2.15f;
     private float fLastAttack = 0;
     private float fAttackDelay = 0.75f;
    public GameObject goProjectile = null;
    public int iMinAttackMilliseconds = 100;
    public int iMaxAttackMilliseconds = 250;
    public GameObject Minion = null;

    public AudioClip sndBackGroundMusic = null;

     private Animator anim = null;
    public float fRunSpeed = 0.45f;

     private float fSpawnDelay = -1;
     private int iMinionCount = 0;
     private GameObject[] goMask = null;
     private GameObject goLight = null;

    void Start()
    {
        iLives = 0;

        rb = GetComponent<Rigidbody>();

        goTarget = GameObject.FindGameObjectWithTag("Player");
        if (goTarget == null)
            Debug.Log("The Boss couldn't find a player.");
        else
        	cPlayer = goTarget.GetComponent<Player>();

        fLastAttack = 0;
        fAttackDelay = UnityEngine.Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;

        anim = GetComponent<Animator>();

        goMask = GameObject.FindGameObjectsWithTag("Boss Mask");
        goLight = GameObject.FindGameObjectWithTag("Boss Light");
        if (goLight != null)
            goLight.SetActive(false);
    }

    /* ====== SPAWN ====== */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && iLives == 0)
        {
            //print("The Boss is waking...");
            anim.SetTrigger("Spawn");
            fSpawnDelay = Time.time + 2;

            /* ====== PLAY AUDIO U BAKA ====== */

            // Weg met de trigger-collider voor performance en om aanval-bugs te voorkomen (zodat dit niet blijft aanroepen)
            Destroy(GetComponent<BoxCollider>());
        }
    }

    private void Update()
    {
        if (fSpawnDelay > 0 && fSpawnDelay - Time.time <= 0)
        {
            iLives = 3;
            //print("The Boss has awoken!");
            fSpawnDelay = 0;
        }
    }

    void FixedUpdate()
    {
        if (cPlayer.levens > 0 && iMinionCount == 0 && iLives > 0)
        {
            fDistance = CalculateDistance(goTarget);

            /* ====== MELEE ====== */
            if (fDistance < fAttackRange)
            {
                if (iLives > 1)
                {
                    if (Time.time - fLastAttack > fAttackDelay)
                    {
                        anim.SetTrigger("Smash");

                        if (iLives == 3)
                            cPlayer.LifeLoss(2);
                        else
                            cPlayer.LifeLoss(3);

                        fAttackDelay = UnityEngine.Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                        fLastAttack = Time.time;
                    }
                }
                else if (Time.time - fLastAttack > fAttackDelay/2)
                {
                    anim.SetTrigger("Smash");

                    cPlayer.LifeLoss(3);

                    fAttackDelay = UnityEngine.Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                    fLastAttack = Time.time;
                }
            }
            /* ====== RANGE ====== */
            else
            {
                float angle = Mathf.Atan2(-(goTarget.transform.position.x - transform.position.x), goTarget.transform.position.z - transform.position.z);
                transform.rotation = Quaternion.Euler(0, -angle * 180 / Mathf.PI, 0);

                /* ====== FLEE ====== */
                if (iLives == 1 && fDistance < 18)
                    rb.AddForce(-transform.forward * 100 * fRunSpeed);

                if (Time.time - fLastAttack > fAttackDelay)
                {
                    anim.SetTrigger("Throw");

                    Vector3 ProjectilePosition = transform.position;
                    ProjectilePosition.y = 3.6f;
                    Instantiate(goProjectile, ProjectilePosition, transform.rotation);

                    fAttackDelay = UnityEngine.Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
                    fLastAttack = Time.time;
                }
            }
        }
    }

    private float CalculateDistance(GameObject Target)
    {
        float x_dist = transform.position.x - Target.transform.position.x;
        x_dist *= x_dist;
        float y_dist = transform.position.y - Target.transform.position.y;
        y_dist *= y_dist;
        float z_dist = transform.position.z - Target.transform.position.z;
        z_dist *= z_dist;

        return Mathf.Sqrt(x_dist + y_dist + z_dist);
    }

    private void LifeLoss()
    {
        if (iMinionCount == 0)
        {
            //Debug.Log("The Boss got hit, " + iLives + " stages left!");
            iLives--;

            /* ====== SWITCH STAGE ===== */
            if (iLives == 2)
            {
                foreach (GameObject Target in goMask)
                    Destroy(Target.gameObject);

                Instantiate(Minion, new Vector3(23.5f, 0, 125), new Quaternion(0, 270, 0, 0));
                Instantiate(Minion, new Vector3(-11.5f, 0, 125), new Quaternion(0, 90, 0, 0));

                iMinionCount = 2;
                anim.SetBool("Collapsed", true);
            }
            else if (iLives == 1)
            {
                goLight.SetActive(true);

                Instantiate(Minion, new Vector3(23.5f, 0, 125), new Quaternion(0, 270, 0, 0));
                Instantiate(Minion, new Vector3(6, 0, 142.5f), new Quaternion(0, 180, 0, 0));
                Instantiate(Minion, new Vector3(-11.5f, 0, 125), new Quaternion(0, 90, 0, 0));
                Instantiate(Minion, new Vector3(6, 0, 107.5f), new Quaternion(0, 0, 0, 0));

                iMinionCount = 4;
                anim.SetBool("Collapsed", true);
            }
            /* ====== DEATH ====== */
            else
            {
	            int index = Array.IndexOf(cPlayer.iInventory, 0);

	            if (index > -1)
	            {
	                Debug.Log("You found Mithril Planks for your boat's bow!");
	                cPlayer.iInventory[index] = 2;
	            }
	            else
	            {
	            	index = Array.IndexOf(cPlayer.iInventory, 1);

	            	cPlayer.levens = cPlayer.iInitialLives;
	                Debug.Log("You found Mithril Planks for your boat's bow! You had to give up a Health Potion for that.");
	                cPlayer.iInventory[index] = 2;
	                Debug.Log("Inventory is already full.");
	                // dialogue inv full
	            }

                anim.SetTrigger("Death");
                // Destroy RB zodat je over het lijk heen kan lopen
                Destroy(GetComponent<Rigidbody>());
                // Destroy script voor performance
                Destroy(GetComponent<BossAI>());
                // We laten het model en alles wel staan zodat je de boss nog wel kan zien vallen en liggen
            }
        }
    }

    private void MinionDied()
    {
        iMinionCount--;

        if (iMinionCount == 0)
        {
            // Zodat je niet onmiddelijk een rost naar je kop krijgt!
            fAttackDelay = UnityEngine.Random.Range(iMinAttackMilliseconds, iMaxAttackMilliseconds) / 100.0f;
            fLastAttack = Time.time;

            anim.SetBool("Collapsed", false);
        }
    }

    /* ====== GET HIT ====== */
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().bAttack && Vector3.Angle(other.transform.forward, transform.position - other.transform.position) < 15)
            LifeLoss();
    }
}
