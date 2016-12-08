using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private GameObject goEnemy;
    private Collider cEnemy;

    Animator anim;
    
    protected float angle;

     public int levens = 10;
     public float fSpeed = 0.45f;
    private float speed;

    bool walk;
    bool run;
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
        anim = GetComponent<Animator>();
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

    void HandleMovement()
    {
        float InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left

        // handle speed and animations
        if (Input.GetKey(KeyCode.LeftShift) && InputV > 0)
        {
            speed = fSpeed * 1.5f;
            run = true;
            walk = false;
        }
        else if (InputV > 0)
        {
            speed = fSpeed * 0.75f;
            run = false;
            walk = true;
        }
        else if (InputV < 0)
        {
            speed = fSpeed * 0.75f;
            run = false;
            walk = true;
        }
        else
        {
            speed = fSpeed;
            run = false;
            walk = false;
        }

        // forward/backward
        transform.Translate(0, 0, speed * InputV);

        // left/right
        angle = 4 * InputH;
        transform.Rotate(0, angle, 0);



        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
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
}
