using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class Player : MonoBehaviour {

    Animator anim;

    float x, y, z;
    float anglex, angley, anglez;

    public int levens = 10;

    string area = "safe";

    bool walk;
    bool run;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();

        transform.rotation = Quaternion.Euler(0, angley, 0);
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
    }

    void HandleInput()
    {

        // levens
        if (levens == 0)
        {
            anim.SetTrigger("Die");
        }
        // running animations
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.04f;
            run = true;
            walk = false;
        }

        // walking animations
        else if (Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.W))
            {
                angley += 4;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                angley += 4;
                transform.position -= transform.forward * 0.02f;
            }
            else
            {
                transform.position += transform.forward * 0.005f;
                angley += 8;
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                angley -= 4;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                angley -= 4;
                transform.position -= transform.forward * 0.02f;
            }
            else
            {
                transform.position += transform.forward * 0.005f;
                angley -= 8;
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.02f;
            walk = true;
            run = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.02f;
            walk = true;
            run = false;
        }
        
        else
        {
            walk = false;
            run = false;
        }

        }
    
=======
public class Player : MonoBehaviour
{
    private GameObject goEnemy;
    private Collider cEnemy;

    //private Animator anim;
    
    protected float angle;

     public int levens = 10;
     public float fSpeed = 0.45f;
    private float speed;

    //bool walk;
    //bool run;
    protected bool bDead = false;

    protected bool bDefend = false;
    protected bool bAttack = false;
    protected float fDistance = 0;
     public float fAttackRange = 2;
    
    void Start()
    {
        //anim = GetComponent<Animator>();
        goEnemy = GameObject.FindGameObjectWithTag("Enemy");
        cEnemy = goEnemy.GetComponent<Collider>();
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
            Destroy(GameObject.FindGameObjectWithTag("PlayerModel"));
        }
    }

    void HandleMovement()
    {
        float InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left

        // handle speed and animations
        if (Input.GetKey(KeyCode.LeftShift) && InputV > 0)
        {
            speed = fSpeed * 1.333333f;
            //run = true;
            //walk = false;
        }
        else
        {
            speed = fSpeed;
            //run = false;
            //walk = true;
        }

        // forward/backward
        if (InputV > 0)   // W / Up / Left_Analog_Stick_Up
            transform.position += transform.forward * speed * InputV;
        else if (InputV < 0)   // S / Down / Left_Analog_Stick_Down
            transform.position -= transform.forward * speed * 0.75f * -InputV;

        // left/right
        if (InputH > 0)
            angle += 4 * InputH;
        else if (InputH < 0)
            angle -= 4 * -InputH;


        transform.rotation = Quaternion.Euler(0, angle, 0);
        //anim.SetBool("Walk", walk);
        //anim.SetBool("Run", run);
    }

    private void HandleCombat()
    {
        if (levens <= 0)
            bDead = true;
        else
        {
            bool LMB = Input.GetMouseButtonDown(0);
            bool RMB = Input.GetMouseButtonDown(1);

            if (RMB)
            {
                bDefend = true;
                bAttack = false;
            }
            else if (LMB)
            {
                bDefend = false;
                bAttack = true;
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
        if (!bDefend)
            levens--;

        // hit anim?
    }
>>>>>>> Fons
}
