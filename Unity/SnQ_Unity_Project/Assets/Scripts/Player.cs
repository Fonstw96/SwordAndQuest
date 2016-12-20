using UnityEngine;
using System;

public class Player : MonoBehaviour
{
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

    void HandleMovement()
    {
        float InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left

        // handle speed and animations
        if (Input.GetKey(KeyCode.LeftShift) && InputV > 0)
        {
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
        }

        // forward/backward
        transform.Translate(0, 0, speed * InputV);

        // left/right
        angle = 4 * InputH;
        transform.Rotate(0, angle, 0);
    }

    private void HandleCombat()
    {
        if (Input.GetMouseButtonDown(0))
            bAttack = true;
        else
            bAttack = false;


        if (bAttack)
            anim.SetInteger("Animation", 3 + UnityEngine.Random.Range(0, 2));   // 3..4

        if (levens < iInitialLives && Time.time - fLastRegen > fRegenDelay)
        {
            fLastRegen = Time.time;
            levens++;
        }
        else if (Input.GetKeyDown("1") && levens < iInitialLives)
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
        anim.SetInteger("Animation", 5);
        levens--;
        if (levens <= 0)
            bDead = true;

        fLastRegen = Time.time;
    }
}
