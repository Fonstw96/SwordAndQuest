using UnityEngine;
using System.Collections;

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

    }
    
    void Update()
    {
        if (!bDead)
        {
            HandleMovement();
            HandleCombat();
        }
        else
        {
            anim.SetTrigger("Die");
        }
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

        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);

        //handle gravity with fixed update
        falldelay--;
        if (falldelay <= 0) falldelay = 0;
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

        if (bAttack)
        {
            fDistance = CalculateDistance(goEnemy);
            if (fDistance < fAttackRange)
                cEnemy.SendMessage("LifeLoss");

            bAttack = false;
        }
    }

    private float CalculateDistance(GameObject Target)
    {
        float x_dist = this.transform.position.x - goEnemy.transform.position.x;
        x_dist *= x_dist;
        float y_dist = this.transform.position.y - goEnemy.transform.position.y;
        y_dist *= y_dist;
        float z_dist = this.transform.position.z - goEnemy.transform.position.z;
        z_dist *= z_dist;

        return Mathf.Sqrt(x_dist + y_dist + z_dist);
    }

    private void LifeLoss()
    {
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
    }
}
