using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
    //private Rigidbody rb;
    private Animator anim;
    public AudioClip Attack1 = null;
    public AudioClip Attack2 = null;

    protected float angle = 0;
    
    public float fSpeed = 0.2f;
    public float fAttackRange = 2;
     private float fInvincibility = 0;
    private float fInputV = 0;
    private float fInputH = 0;

    public int levens = 10;
    public int iInitialLives = 0;
    private int attack;
    private int falldelay = 0;

    bool walk;
    bool run;
    private bool bInputRun = false;

    //bool inAir = true;

    public bool bDead = false;
    public bool sword = false;
    public bool bAttack = false;
    public bool bUse = false;

    public int[] iInventory;

    void Start()
    {
    	bDead = false;

        anim = GetComponent<Animator>();

        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (goEnemy == null)
            goEnemy = GameObject.FindGameObjectWithTag("Boss");

        //rb = GetComponent<Rigidbody>();

        if (UIController.imInventory != null)
        {
            iInventory = new int[UIController.imInventory.Length - 1];
            Debug.Log("Player's inventory size is " + iInventory.Length);

            if (PlayerPrefs.HasKey("Inventory0"))
            {
                for (int i = 0; i < iInventory.Length; i++)
                {
                    int item = PlayerPrefs.GetInt("Inventory" + i);
                    iInventory[i] = item;
                    Debug.Log("Item " + item + " set into inventory slot " + i);
                }
            }
        }
        else
        {
            iInventory = new int[1];
            Debug.Log("No inventory set, player's inventory size is 1");
        }

        iInitialLives = levens;
    }
    
    void Update()
    {
        if (!bDead)
        {
            HandleCombat();

            fInputV = Input.GetKey(KeyCode.W) ? 1 : 0;
            if (fInputV == 0) fInputV = Input.GetKey(KeyCode.S) ? -1 : 0;
            if (fInputV == 0) fInputV = Input.GetAxis("LeftV");
            fInputH = Input.GetKey(KeyCode.D) ? 1 : 0;
            if (fInputH == 0) fInputH = Input.GetKey(KeyCode.A) ? -1 : 0;
            if (fInputH == 0) fInputH = Input.GetAxis("LeftH");

            bInputRun = Input.GetButton("Sprint") || Input.GetAxis("RightTrig") > .5f;

            HandleAnimation();
        }
        else
            anim.SetTrigger("Die");
    }

    void FixedUpdate()
    {
        if (!bDead)
        {
            HandleMovement();

            bUse = Input.GetButtonDown("Use");
        }
    }

    private void HandleCombat()
    {
        if (levens <= 0)
            bDead = true;
        else if (Input.GetButtonDown("Potion") && levens < iInitialLives)
        {
            int index = Array.IndexOf(iInventory, 1);

            if (index > -1)
            {
                iInventory[index] = 0;
                levens = iInitialLives;
            }
        }
        else if (sword == true)
        {
            bAttack = Input.GetButtonDown("Attack");

            if (bAttack)
            {
                if (attack >= 3)
                {
                    anim.SetTrigger("Attack 2");

                    if (Attack2 != null)
                        AudioSource.PlayClipAtPoint(Attack2, transform.position, .5f);
                    attack = 1;
                }
                else
                {
                    anim.SetTrigger("Attack1");

                    if (Attack1 != null)
                        AudioSource.PlayClipAtPoint(Attack1, transform.position);
                    attack++;
                }
            }
        }
    }

    private void HandleMovement()
    {
        float walkspeed = fSpeed * fInputV;

        if (bInputRun && fInputV > 0)
            walkspeed *= 1.5f;
        else if (fInputV < 0)
            walkspeed *= .75f;

        float xwalk = -Mathf.Sin(angle) * walkspeed;
        float zwalk = Mathf.Cos(angle) * walkspeed;
        Vector3 Walk = new Vector3(xwalk, 0, zwalk);

        float strafespeed = fSpeed * fInputH;
        float xstrafe = Mathf.Cos(angle) * strafespeed;
        float zstrafe = Mathf.Sin(angle) * strafespeed;
        Vector3 Strafe = new Vector3(xstrafe, 0, zstrafe);

        Vector3 Here = transform.position;
        Here.y += 1;

        if (walkspeed > 0)
        {
            Debug.DrawRay(Here, transform.forward, Color.blue);
            if (!Physics.Raycast(Here, transform.forward, walkspeed))
                transform.Translate(Walk);
        }
        else if (walkspeed < 0)
        {
            Debug.DrawRay(Here, -transform.forward, Color.blue);
            if (!Physics.Raycast(Here, -transform.forward, -walkspeed))
                transform.Translate(Walk);
        }

        if (strafespeed > 0)
        {
            Debug.DrawRay(Here, transform.right, Color.red);
            if (!Physics.Raycast(Here, transform.right, strafespeed))
                transform.Translate(Strafe);
        }
        else if (strafespeed < 0)
        {
            Debug.DrawRay(Here, -transform.right, Color.red);
            if (!Physics.Raycast(Here, -transform.right, -strafespeed))
                transform.Translate(Strafe);
        }

        float TurnCamera = Input.GetAxis("MouseH");
        if (TurnCamera == 0) TurnCamera = Input.GetAxis("RightH") * 4;

        if (TurnCamera != 0)
            transform.Rotate(0, TurnCamera, 0);

        falldelay--;
        if (falldelay <= 0) falldelay = 0;
    }

    private void HandleAnimation()
    {
        if (bInputRun && fInputV >= .6667f)
        {
            run = true;
            walk = false;
        }
        else if (fInputV != 0 || fInputH != 0)
        {
            run = false;
            walk = true;
        }
        else
        {
            run = false;
            walk = false;
        }
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
    }

    private float CalculateDistance(GameObject DistanceTo)
    {
        float x_dist = this.transform.position.x - DistanceTo.transform.position.x;
        x_dist *= x_dist;
        float y_dist = this.transform.position.y - DistanceTo.transform.position.y;
        y_dist *= y_dist;
        float z_dist = this.transform.position.z - DistanceTo.transform.position.z;
        z_dist *= z_dist;

        return Mathf.Sqrt(x_dist + y_dist + z_dist);
    }

    public void LifeLoss(int iDamage)
    {
        if (Time.time - fInvincibility > .5f)
        {
            levens -= iDamage;

            if (levens <= 0)
                bDead = true;
            else
                anim.SetTrigger("Hit");

            fInvincibility = Time.time;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
            //inAir = true;
            falldelay = 10;
        }
    }
}
