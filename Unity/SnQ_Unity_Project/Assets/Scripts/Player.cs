using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
<<<<<<< HEAD
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

    public bool bDead = false;
    public bool sword = false;
    public bool bAttack = false;

    private int iInitialLives = 0;
    private float fLastRegen = 0;
    public float fRegenDelay = 3;

    public int[] iInventory;
=======
    private Rigidbody rb;
    private Animator anim;
    
    protected float angle = 0;
    private float maxSpeed = 0;
    
    public float fSpeed = 0.2f;
    public float fAttackRange = 2;
    private float fLastRegen = 0;
    public float fRegenDelay = 5;

    public int levens = 5;
    public int iInitialLives = 0;
    private int attack;
    private int falldelay = 0;

    bool walk;
    bool run;

    //bool inAir = true;
>>>>>>> origin/Fons

    protected bool bDead = false;
    public bool sword = false;
    public bool bAttack = false;

    public int[] iInventory;


    void Start()
    {
        anim = GetComponent<Animator>();
        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        cEnemy = goEnemy.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

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

            // Dit moet na de input worden ingesteld en alleen wanneer het toch al zichtbaar is, niet tijdens A en D!!
            anim.SetBool("Walk", walk);
            anim.SetBool("Run", run);
        }
        else
<<<<<<< HEAD
        {
            anim.SetInteger("Animation", 99);
            transform.GetComponent<Respawn>().RespawnPlayer();
        }
=======
            anim.SetTrigger("Die");
>>>>>>> origin/Fons
    }

    void FixedUpdate()
    {
<<<<<<< HEAD
        float magnitude;
        float InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down

        Vector3 movement = transform.forward * 10;
        //Vector3 gravity = new Vector3(0, 35, 0);

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
        }

        if (inAir == true)
        {
            if (falldelay == 0) ;
            //GetComponent<Rigidbody>().AddForce(Physics.gravity * 20, ForceMode.Acceleration);

        }
    }

    void HandleMovement()
    {
        
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left

        // forward/backward
        //getting handled in fixed update

        // left/right
        angle = 4 * InputH;
        transform.Rotate(0, angle, 0);

        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);

        //handle gravity with fixed update
        falldelay--;
        if (falldelay <= 0) falldelay = 0;

=======
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
>>>>>>> origin/Fons
    }

    private void HandleCombat()
    {
        if (levens <= 0)
            bDead = true;
<<<<<<< HEAD
        else if(sword == true)
        {
            bool LMB = Input.GetMouseButtonDown(0);
            bool RMB = Input.GetMouseButtonDown(1);

            if (LMB)
            { 
                bAttack = true;
                if (attack == 3)
=======
        else if (sword == true)
        {
            bAttack = Input.GetButtonDown("Fire1");

            if (bAttack)
            {
                if (attack >= 3)
>>>>>>> origin/Fons
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
<<<<<<< HEAD
        if(!goEnemy.GetComponent<EnemyAI>().dummy)
            levens--;
=======
        levens -= iDamage;
>>>>>>> origin/Fons

        if (levens <= 0)
            bDead = true;
        else
            anim.SetTrigger("Hit");

        fLastRegen = Time.time;
    }

<<<<<<< HEAD
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
            inAir = false;
        }
    }
=======
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.name == "Terrain")
    //    {
    //        inAir = false;
    //    }
    //}
>>>>>>> origin/Fons

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
<<<<<<< HEAD
            inAir = true;
=======
            //inAir = true;
>>>>>>> origin/Fons
            falldelay = 10;
        }
    }
}
