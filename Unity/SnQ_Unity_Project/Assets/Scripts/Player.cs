using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
    private Collider cEnemy;
    private Rigidbody rb;
    private Animator anim;

    protected float angle;
    private float speed;
    private float maxSpeed;



    public float fSpeed = 0.45f;
    protected float fDistance = 0;
    public float fAttackRange = 2;
    private float InputVt = 0;
    private float InputV;

    public int levens = 7;
    private int attack;

    bool walk;
    bool run;


    public bool bDead = false;

    bool inAir = true;

    public bool sword = false;
    public bool bAttack = false;
    private bool versnel = false;
    private bool isGrounded = true;

    public int iInitialLives = 0;
    private float fLastRegen = 0;
    public float fRegenDelay = 3;

    public int[] iInventory;

    private int falldelay = 0;

    void Start()
    {
        bDead = false;
        isGrounded = false;
        anim = GetComponent<Animator>();
        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        cEnemy = goEnemy.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        StartPos();

        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (goEnemy == null)
            goEnemy = GameObject.FindGameObjectWithTag("Boss");

        rb = GetComponent<Rigidbody>();

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
            HandleMovement();

            // Dit moet na de input worden ingesteld en alleen wanneer het toch al zichtbaar is, niet tijdens A en D!!
            anim.SetBool("Walk", walk);
            anim.SetBool("Run", run);
        }
        else
        { 
            anim.SetTrigger("Die");
            transform.GetComponent<Respawn>().RespawnPlayer();
        }

    }

    void FixedUpdate()
    {
        if (!bDead)
        {
            float magnitude;
            float fallspeed;


            Vector3 movement = transform.forward * 10;

            // handle speed and animations
            if (Input.GetKey(KeyCode.LeftShift) && InputV > 0)
            {
                rb.AddForce(movement * 10);
                run = true;
                walk = false;
                maxSpeed = 20;
            }
            else if (InputV > 0)
            {
                if (InputV > InputVt) versnel = true;
                else if (InputV < InputVt) versnel = false;
                rb.AddForce(movement * 5);
                run = false;
                walk = true;
                maxSpeed = 10;
            }
            else if (InputV < 0)
            {
                rb.AddForce(movement * -5);
                run = false;
                walk = true;
                maxSpeed = 10;
            }

            rb.useGravity = true;



            //rb.AddForce(gravity * -10);
            //rb.AddForce(movement * 10);
            // handle speed and animations
            if (Input.GetKey(KeyCode.LeftShift) && InputV > 0)
            {
                rb.AddForce(movement * 10);
                run = true;
                walk = false;
                maxSpeed = 20;
            }
            else if (InputV > 0)
            {

                rb.AddForce(movement * 5);
                run = false;
                walk = true;
                maxSpeed = 10;
            }
            else if (InputV < 0)
            {
                rb.AddForce(movement * -5);
                run = false;
                walk = true;
                maxSpeed = 10;
            }
            else if (InputV == 0)
            {
                rb.velocity = rb.velocity * 0.5f;
                run = false;
                walk = false;


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

                InputVt = InputV;

                //gravity
                fallspeed = 0;

                Vector3 gravity = new Vector3(0.0f, 9.81f, 0.0f) * -1;


                if (isGrounded == false)
                    fallspeed += 50;
                else if (isGrounded == true)
                {
                    fallspeed = 0;

                }



                Vector3 downforce = gravity * fallspeed;

                rb.AddForce(downforce, ForceMode.Acceleration);

                if (inAir == true)
                {
                    //if (falldelay == 0)
                    //GetComponent<Rigidbody>().AddForce(Physics.gravity * 20, ForceMode.Acceleration);

                }


            }
        }
    }

    void HandleMovement()
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


        else if(sword == true)
        {
            bool LMB = Input.GetMouseButtonDown(0);
            bool RMB = Input.GetMouseButtonDown(1);

            if (LMB)
            {
                bAttack = true;


            
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

    public void LifeLoss(int iDamage)
    {
        levens -= iDamage;

        if (levens <= 0)
            bDead = true;
        else
            anim.SetTrigger("Hit");

        fLastRegen = Time.time;
    }

    void OnCollisionEnter(Collision _Collision)
    {
        if (_Collision.gameObject.tag == "Terrain")
        {
            isGrounded = true;
        }
        if (_Collision.gameObject.tag == "Cave")
        {
            isGrounded = true;
        }
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision _Collision)
    {
        if (_Collision.gameObject.tag == "Terrain")
        {
            isGrounded = false;
        }
    }

    private void StartPos()
    {
        
        int startpos = 0;
        startpos = PlayerPrefs.GetInt("startposision");
        if (startpos == 0) transform.position = new Vector3(194.0727f, 3.317121f, 412.2244f);// spawn start
        if (startpos == 1) transform.position = new Vector3(458.9476f,0.3f, 46.84577f);//naar overworld van tutorial level
        if (startpos == 2) transform.position = new Vector3(73,0.01413554f,51);//naar tutorial level
        if (startpos == 3) transform.position = new Vector3(0, 0, 5);//naar temple
        if (startpos == 4) transform.position = new Vector3(109,0.5f,102.6f);//naar overworld van temple
        if (startpos == 5) transform.position = new Vector3(95, 0.31f, 321);//naar forest
        if (startpos == 6) transform.position = new Vector3(431.2f,0.5f,957.4f);//naar overworld van forest
        if (startpos == 7) transform.position = new Vector3(5, 0, 255);//naar maze
        if (startpos == 8) transform.position = new Vector3(164.9f,0.5f,863.7f);//naar overworld van maze


        Debug.Log(startpos);
    }
}
