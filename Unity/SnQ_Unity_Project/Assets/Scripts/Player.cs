using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
    // private Rigidbody rb;
    private Animator anim;
    
    protected float angle = 0;
    
    public float fSpeed = 0.2f;
    public float fMouseSensitivity = 1.2f;
    public float fAttackRange = 2;
     private float fInvincibility = 0;

    public int levens = 10;
    public int iInitialLives = 0;
    private int attack;
    private int falldelay = 0;

    bool walk;
    bool run;

    //bool inAir = true;

    public bool bDead = false;
    public bool sword = false;
    public bool bAttack = false;

    public int[] iInventory;

    void Start()
    {
    	bDead = false;

        anim = GetComponent<Animator>();

        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (goEnemy == null)
            goEnemy = GameObject.FindGameObjectWithTag("Boss");

        // rb = GetComponent<Rigidbody>();

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
            anim.SetTrigger("Die");
    }

    void FixedUpdate()
    {
        if (!bDead)
        {
            float InputV = Input.GetKey(KeyCode.W) ? 1 : 0;
            if (InputV == 0) InputV = Input.GetKey(KeyCode.S) ? -1 : 0;
            if (InputV == 0) InputV = Input.GetAxis("Left Analog Vertical");
            float InputH = Input.GetKey(KeyCode.D) ? 1 : 0;
            if (InputH == 0) InputH = Input.GetKey(KeyCode.A) ? -1 : 0;
            if (InputH == 0) InputH = Input.GetAxis("Left Analog Horizontal");

            float TurnCamera = Input.GetAxis("Mouse X");
            //if (TurnCamera == 0) TurnCamera = Input.GetAxis("Right Analog Horizontal");

            bool InputRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton7);

            float walkspeed = fSpeed * InputV;

            if (InputRun && InputV > 0)
                walkspeed *= 1.5f;
            else if (InputV < 0)
                walkspeed *= .75f;

            float xwalk = -Mathf.Sin(angle) * walkspeed;
            float zwalk = Mathf.Cos(angle) * walkspeed;
            Vector3 Walk = new Vector3(xwalk, 0, zwalk);

            float strafespeed = fSpeed * InputH;
            float xstrafe = Mathf.Cos(angle) * strafespeed;
            float zstrafe = Mathf.Sin(angle) * strafespeed;
            Vector3 Strafe = new Vector3(xstrafe, 0, zstrafe);

            Vector3 Here = transform.position;
            Here.y += 1;
            
            if (!Physics.Raycast(Here, Walk, walkspeed))
                transform.Translate(Walk);
            if (!Physics.Raycast(Here, Strafe, strafespeed))
                transform.Translate(Strafe);

            if (InputRun && InputV > 0)
            {
                run = true;
                walk = false;
            }
            else if (InputV != 0 || InputH != 0)
            {
                run = false;
                walk = true;
            }
            else
            {
                run = false;
                walk = false;
            }

            if (TurnCamera != 0)
                transform.Rotate(0, TurnCamera * fMouseSensitivity, 0);

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

        if (Input.GetKeyDown(KeyCode.F) && levens < iInitialLives)
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
