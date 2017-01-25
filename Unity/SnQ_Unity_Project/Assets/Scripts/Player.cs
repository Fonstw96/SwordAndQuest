using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
<<<<<<< HEAD
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
=======
    private Collider cEnemy;
    private Rigidbody rb;
    private Animator anim;
<<<<<<< HEAD
    private AudioSource audio;
=======
>>>>>>> parent of f5ca85a... bye bye

    protected float angle;
    private float speed;
    private float maxSpeed;

    bool disable = false;

    public float fSpeed = 0.45f;
    protected float fDistance = 0;
    public float fAttackRange = 2;
    private float InputVt = 0;
    private float InputV;

    public int levens = 7;
    private int attack;
<<<<<<< HEAD
    private int attackdelay;
=======
>>>>>>> parent of f5ca85a... bye bye

    bool walk;
    bool run;

    public int startpos;


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
<<<<<<< HEAD
    public AudioClip[] audioclip;
=======
>>>>>>> parent of f5ca85a... bye bye

    private int falldelay = 0;

    void Start()
    {
        bDead = false;
        isGrounded = false;
        anim = GetComponent<Animator>();
<<<<<<< HEAD
        audio = GetComponent<AudioSource>();
=======
>>>>>>> parent of f5ca85a... bye bye
        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        cEnemy = goEnemy.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        StartPos();
>>>>>>> master

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
            //HandleMovement();
<<<<<<< HEAD
            //float TurnCamera = Input.GetAxis("MouseH");
            //            if (TurnCamera == 0) TurnCamera = Input.GetAxis("RightH") * 4;
            attackdelay--;
            
            //if (TurnCamera != 0)
            //    transform.Rotate(0, TurnCamera, 0);
=======
            float TurnCamera = Input.GetAxis("MouseH");
            if (TurnCamera == 0) TurnCamera = Input.GetAxis("RightH") * 4;

<<<<<<< HEAD
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
=======
            if (TurnCamera != 0)
                transform.Rotate(0, TurnCamera, 0);
>>>>>>> parent of f5ca85a... bye bye
            // Dit moet na de input worden ingesteld en alleen wanneer het toch al zichtbaar is, niet tijdens A en D!!
            anim.SetBool("Walk", walk);
            anim.SetBool("Run", run);
        }
        else
        { 
            anim.SetTrigger("Die");
            transform.GetComponent<Respawn>().RespawnPlayer();
        }
<<<<<<< HEAD
        //if(attackdelay>0)
        Debug.Log(attackdelay);
=======
>>>>>>> parent of f5ca85a... bye bye

>>>>>>> master
    }

    void FixedUpdate()
    {
<<<<<<< HEAD
=======
        if (!bDead)
        {
            float InputV = Input.GetKey(KeyCode.W) ? 1 : 0;
            if (InputV == 0) InputV = Input.GetKey(KeyCode.S) ? -1 : 0;
            if (InputV == 0) InputV = Input.GetAxis("Vertical");
            float InputH = Input.GetKey(KeyCode.D) ? 1 : 0;
            if (InputH == 0) InputH = Input.GetKey(KeyCode.A) ? -1 : 0;
            if (InputH == 0) InputH = Input.GetAxis("Horizontal");

            bool InputRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton7);

            float AnglePi = angle * Mathf.PI / 180;
            float speed = fSpeed * InputV;
            if (InputRun)
                speed *= 1.5f;
            // Sin en Cos omdraaien voor strafe
            float xspeed = Mathf.Sin(AnglePi) * speed;
            float zspeed = Mathf.Cos(AnglePi) * speed;
            Vector3 Movement = new Vector3(xspeed, 0, zspeed);

            if (InputRun && InputV > 0)
            {
                if (!Physics.Raycast(transform.position, transform.forward, speed))
                    transform.Translate(Movement);
                run = true;
                walk = false;
            }
            else if (InputV != 0)
            {
                if (!Physics.Raycast(transform.position, transform.forward, speed))
                    transform.Translate(Movement);

                run = false;
                walk = true;
            }

            if (InputH != 0)
            {
                angle = 4 * InputH;
                transform.Rotate(0, angle, 0);
            }
        }
    }

    void HandleMovement()
    {


>>>>>>> master
        if (!bDead)
        {
            HandleMovement();

            bUse = Input.GetButtonDown("Use");
        }
    }

    private void HandleCombat()
    {
<<<<<<< HEAD
        if (levens <= 0)
            bDead = true;
        else if (Input.GetButtonDown("Potion") && levens < iInitialLives)
        {
            int index = Array.IndexOf(iInventory, 1);
=======
<<<<<<< HEAD
        

=======
>>>>>>> parent of f5ca85a... bye bye
        if (levens <= 0)
            bDead = true;


        else if(sword == true)
        {
            bool LMB = Input.GetMouseButtonDown(0);
            bool RMB = Input.GetMouseButtonDown(1);
<<<<<<< HEAD
            if (attackdelay <= 0)
            {
                if (LMB)
                {
                    bAttack = true;
                    PlaySound(1);
                    attackdelay = 60;
                    Debug.Log(attackdelay);


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
=======
>>>>>>> master

            if (index > -1)
            {
<<<<<<< HEAD
                iInventory[index] = 0;
                levens = iInitialLives;
            }
        }
        else if (sword == true)
        {
            bAttack = Input.GetButtonDown("Attack");
=======
                bAttack = true;
>>>>>>> master


            
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
>>>>>>> parent of f5ca85a... bye bye
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

<<<<<<< HEAD
    void PlaySound(int clip)
    {
        audio.clip = audioclip[clip];
        audio.Play();
    }

=======
>>>>>>> parent of f5ca85a... bye bye
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
        if (Time.time - fInvincibility > .5f)
        {
            levens -= iDamage;
=======
        levens -= iDamage;

        if (levens <= 0)
            bDead = true;
        else
<<<<<<< HEAD
        {
            anim.SetTrigger("Hit");
            PlaySound(0);
        }
=======
            anim.SetTrigger("Hit");
>>>>>>> parent of f5ca85a... bye bye
>>>>>>> master

            if (levens <= 0)
                bDead = true;
            else
                anim.SetTrigger("Hit");

<<<<<<< HEAD
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
=======
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
        
        //startpos = 1;
        if (startpos == 0)
        startpos = PlayerPrefs.GetInt("startposision");
        if (startpos == 0) transform.position = new Vector3(194.0727f, 3.317121f, 412.2244f);// spawn start
        if (startpos == 1) transform.position = new Vector3(458.9476f,0.3f, 46.84577f);//naar overworld van tutorial level
        if (startpos == 2) transform.position = new Vector3(73,0.01413554f,51);//naar tutorial level
        if (startpos == 3) transform.position = new Vector3(0, 0, 5);//naar temple
        if (startpos == 4) transform.position = new Vector3(109,0.5f,102.6f);//naar overworld van temple
        if (startpos == 5) transform.position = new Vector3(95, 0.31f, 321);//naar forest
<<<<<<< HEAD
        if (startpos == 6) transform.position = new Vector3(375,0.5f,856);//naar overworld van forest
        if (startpos == 7) transform.position = new Vector3(5, 0, 255);//naar maze
        if (startpos == 8) transform.position = new Vector3(134,0.5f,541);//naar overworld van maze
=======
        if (startpos == 6) transform.position = new Vector3(431.2f,0.5f,957.4f);//naar overworld van forest
        if (startpos == 7) transform.position = new Vector3(5, 0, 255);//naar maze
        if (startpos == 8) transform.position = new Vector3(164.9f,0.5f,863.7f);//naar overworld van maze
>>>>>>> parent of f5ca85a... bye bye


        Debug.Log(startpos);
    }
>>>>>>> master
}
