using UnityEngine;
using System;

public class Player : MonoBehaviour
{
<<<<<<< HEAD
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

    public int levens = 10;
    private int attack;
    private int falldelay = 0;

    bool walk;
    bool run;

    bool inAir = true;

    protected bool bDead = false;
    public bool sword = false;
    protected bool bAttack = false;
    

    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        cEnemy = goEnemy.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

=======
     protected Animator anim;
    
     protected float angle;

    public int levens = 5;
     private int iInitialLives = 0;
     private float fLastRegen = 0;
    public float fRegenDelay = 3;

    public float fSpeed = 0.45f;
     private float speed;
    
     protected bool bDead = false;
    
    public bool bAttack = false;

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
        //anim = GetComponent<Animator>();

        anim = GetComponent<Animator>();

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
>>>>>>> origin/Fons
    }
    
    void Update()
    {
        if (!bDead)
        {
            HandleMovement();
            HandleCombat();
        }
        else
            anim.SetInteger("Animation", 99);
    }

    void FixedUpdate()
    {
        float magnitude;
        float InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down

        Vector3 movement = transform.forward * 10;
        //Vector3 gravity = new Vector3(0, 35, 0);

        
        //rb.AddForce(gravity * -10);
        //rb.AddForce(movement * 10);
        // handle speed and animations
        if (Input.GetKey(KeyCode.LeftShift) && InputV > 0)
        {
<<<<<<< HEAD
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
        else if ( InputV == 0)
        {
            rb.velocity = rb.velocity * 0.5f;
            run = false;
            walk = false;
            
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
=======
            speed = fSpeed * 1.5f;

            anim.SetInteger("Animation", 2);
        }
        else if (InputV > 0)
        {
            speed = fSpeed;

            anim.SetInteger("Animation", 1);
        }
        else if (InputV < 0)
        {
            speed = fSpeed * 0.75f;

            anim.SetInteger("Animation", 1);
        }
        else if (!bAttack)
        {
            speed = fSpeed;
            
            anim.SetInteger("Animation", 0);
>>>>>>> origin/Fons
        }

        if (inAir == true)
        {
            if(falldelay == 0)
            //GetComponent<Rigidbody>().AddForce(Physics.gravity * 20, ForceMode.Acceleration);
            Debug.Log("test");
        }
        Debug.Log(falldelay);
    }

    void HandleMovement()
    {
        
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left

        // forward/backward
        //getting handled in fixed update

        // left/right
        angle = 4 * InputH;
        transform.Rotate(0, angle, 0);
<<<<<<< HEAD

        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);

        //handle gravity with fixed update
        falldelay--;
        if (falldelay <= 0) falldelay = 0;
=======
>>>>>>> origin/Fons
    }

    private void HandleCombat()
    {
<<<<<<< HEAD
        if (levens <= 0)
            bDead = true;
        else if(sword == true)
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
=======
        if (Input.GetMouseButtonDown(0))
            bAttack = true;
        else
            bAttack = false;

>>>>>>> origin/Fons

        if (bAttack)
            anim.SetInteger("Animation", 3 + UnityEngine.Random.Range(0, 2));   // 3..4

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
<<<<<<< HEAD
        if(!goEnemy.GetComponent<EnemyAI>().dummy)
            levens--;

        anim.SetTrigger("Hit");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
            inAir = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
            inAir = true;
            falldelay = 10;
        }
=======
        anim.SetInteger("Animation", 5);
        levens--;
        if (levens <= 0)
            bDead = true;

        fLastRegen = Time.time;
>>>>>>> origin/Fons
    }
}
