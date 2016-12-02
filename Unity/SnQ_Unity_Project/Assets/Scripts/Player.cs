using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Animator anim;
    
    protected float angle;

    public int levens = 10;
    public float fSpeed = 0.45f;
    private float speed;

    bool walk;
    bool run;
    protected bool bDead = false;

    protected bool bDefend = false;
    protected bool bAttack = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!bDead)
        {
            HandleMovement();
            HandleCombat();
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
            run = true;
            walk = false;
        }
        else
        {
            speed = fSpeed;
            run = false;
            walk = true;
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
    }
}
