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

    public int levens = 10;
    private int attack;

    bool walk;
    bool run;

    protected bool bDead = false;
    public bool sword = false;
    public bool bAttack = false;
    private bool versnel = false;
    private bool isGrounded = true;

    private int iInitialLives = 0;
    private float fLastRegen = 0;
    public float fRegenDelay = 3;

    public int[] iInventory;

    protected struct Target
    {
        public void Set(int id, float dis)
        {
            ID = id;
            fDistance = dis;
        }

        public int ID;
        public float fDistance;
    }


    void Start()
    {
        isGrounded = false;
        anim = GetComponent<Animator>();
        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        cEnemy = goEnemy.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        StartPos();

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
            HandleMovement();
            HandleCombat();
        }
        else
            anim.SetTrigger("Die");

        
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
        }
        
        
    }

    void HandleMovement()
    {
        InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left
        

        // forward/backward
        if (InputV < 1 && versnel == false)
        {
            //rb.velocity = rb.velocity * 0.5f;
            rb.velocity = Vector3.zero;
            run = false;
            walk = false;

        }
        //getting handled in fixed update

        // left/right
        angle = 4 * InputH;
        transform.Rotate(0, angle, 0);

        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);

        //handle gravity with fixed update



    }

    private void HandleCombat()
    {
        if (levens <= 0)
            bDead = true;
        else if (sword == true)
        {
            bool LMB = Input.GetMouseButtonDown(0);
            bool RMB = Input.GetMouseButtonDown(1);

            if (LMB)
            {
                bAttack = true;
                if (attack == 3)
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
        if (!goEnemy.GetComponent<EnemyAI>().dummy)
            levens--;

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
        if (_Collision.gameObject.tag == "Cave")
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
        if (startpos == 4) transform.position = new Vector3(109,0.5f,102.6);//naar overworld van temple
        if (startpos == 5) transform.position = new Vector3(95, 0.31f, 321);//naar forest
        if (startpos == 6) transform.position = new Vector3(431.2f,0.5f,957.4f);//naar overworld van forest
        if (startpos == 7) transform.position = new Vector3(5, 0, 255);//naar maze
        if (startpos == 8) transform.position = new Vector3(164.9f,0.5f,863.7f);//naar overworld van maze


        Debug.Log(startpos);
    }
}
