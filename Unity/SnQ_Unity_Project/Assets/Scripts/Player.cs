using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
     private GameObject goEnemy;
     private Collider cEnemy;

     protected Animator anim;
    
     protected float angle;

    public int levens = 5;
     private float fLastRegen = 0;
    public float fRegenDelay = 3;

    public float fSpeed = 0.45f;
     private float speed;
    
     protected bool bDead = false;
    
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
            anim.SetInteger("Animation", 99);
    }

    void HandleMovement()
    {
        float InputV = Input.GetAxis("Vertical");     // W / S / Up / Down / Left_Analog_Stick_Up / Left_Analog_Stick_Down
        float InputH = Input.GetAxis("Horizontal");   // D / A / Right / Left / Left_Analog_Stick_Right / Left_Analog_Stick_Left
        bAttack = Input.GetMouseButton(0);

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
        if (levens <= 0)
            bDead = true;
        else
        {
            if (bAttack)
            {
                anim.SetInteger("Animation", 3 + Random.Range(0, 2));   // 3..4

                fDistance = CalculateDistance(goEnemy);
                if (fDistance < fAttackRange)
                    cEnemy.SendMessage("LifeLoss");

                bAttack = false;
            }

            if (levens < 5 && Time.time - fLastRegen > fRegenDelay)
            {
                fLastRegen = Time.time;
                levens++;
            }
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
        anim.SetInteger("Animation", 5);
        levens--;

        fLastRegen = Time.time;
    }
}
