using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
    private Rigidbody rb;
    private Animator anim;
    
    protected float angle = 0;
    private float maxSpeed = 0;
    

    public float fSpeed = 0.2f;
    public float fAttackRange = 2;

    public int levens = 5;
    private int attack;
    private int falldelay = 0;

    bool walk;
    bool run;

    //bool inAir = true;

    protected bool bDead = false;
    public bool sword = false;
    public bool bAttack = false;

    private int iInitialLives = 0;
    private float fLastRegen = 0;
    public float fRegenDelay = 5;

    public int[] iInventory;

    void Start()
    {
        anim = GetComponent<Animator>();

        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (goEnemy == null)
            goEnemy = GameObject.FindGameObjectWithTag("Boss");

        rb = GetComponent<Rigidbody>();

        if (UIController.imInventory != null)
        {
            iInventory = new int[UIController.imInventory.Length - 1];
            Debug.Log("Player's inventory size is " + iInventory.Length);
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

            // Dit moet na de input worden ingesteld en alleen wanneer het toch al zichtbaar is, niet tijdens A en D!!
            anim.SetBool("Walk", walk);
            anim.SetBool("Run", run);
        }
        else
            anim.SetTrigger("Die");
    }

    void FixedUpdate()
    {
        if (!bDead)
        {
            float magnitude;
            float InputV = Input.GetAxis("Vertical");
            float InputH = Input.GetAxis("Horizontal");

            bool InputRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton5);

            Vector3 movement = transform.forward * 100.0f * fSpeed;
            float speed = fSpeed;
            //Vector3 gravity = new Vector3(0, 35, 0);


            //rb.AddForce(gravity * -10);
            //rb.AddForce(movement * 10);
            // handle speed and animations

            if (InputRun && InputV > 0)
            {
                rb.AddForce(movement * 1.5f);
                speed *= 1.5f;
                run = true;
                walk = false;
                maxSpeed = 20;
            }
            else if (InputV != 0)
            {
                if (InputV > 0)
                    rb.AddForce(movement);
                else
                    rb.AddForce(-movement);
                run = false;
                walk = true;
                maxSpeed = 10;
            }
            else
            {
                rb.velocity = Vector3.zero;
                run = false;
                walk = false;
            }

            if (InputH != 0)
            {
                //magnitude = rb.velocity.magnitude;
                //rb.velocity = Vector3.zero;
                //rb.velocity = transform.forward * magnitude * speed * 0.5f;

                angle = 4 * InputH;
                transform.Rotate(0, angle, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                magnitude = rb.velocity.magnitude;
                rb.velocity = Vector3.zero;
                rb.velocity = transform.forward * magnitude * speed * 0.5f;
            }

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            falldelay--;
            if (falldelay <= 0) falldelay = 0;

            //if (inAir == true)
            //{
            //    if(falldelay == 0)
            //    //GetComponent<Rigidbody>().AddForce(Physics.gravity * 20, ForceMode.Acceleration);
            //    Debug.Log("test");
            //}
        }
    }

    private void HandleCombat()
    {
        if (levens <= 0)
            bDead = true;
        else if (sword == true)
        {
            bAttack = Input.GetButtonDown("Fire1");

            if (bAttack)
            {
                if (attack >= 3)
                {
                    anim.SetTrigger("Attack 2");
                    attack = 1;
                }
                else
                {
                    anim.SetTrigger("Attack1");
                    attack++;
                }
            }
        }


        if (levens < iInitialLives && Time.time - fLastRegen > fRegenDelay)
        {
            fLastRegen = Time.time;
            levens++;
        }
        else if (Input.GetKeyDown(KeyCode.F) && levens < iInitialLives)
        {
            int index = Array.IndexOf(iInventory, 1);

            if (index > -1)
            {
                iInventory[index] = 0;
                levens = iInitialLives;
            }
        }
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

    private void LifeLoss()
    {
        if (goEnemy.tag == "Enemy")
        {
            if (!goEnemy.GetComponent<EnemyAI>().dummy)   // dit staat hier en niet hierboven vanwege errors
                levens--;
        }
        else if (goEnemy.tag == "Boss")
        {
            if (goEnemy.GetComponent<BossAI>().iLives == 3)   // hetzelfde verhaal
                levens -= 2;
            else
                levens -= 3;
        }

        if (levens <= 0)
            bDead = true;
        else
            anim.SetTrigger("Hit");

        fLastRegen = Time.time;
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.name == "Terrain")
    //    {
    //        inAir = false;
    //    }
    //}

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
            //inAir = true;
            falldelay = 10;
        }
    }
}
