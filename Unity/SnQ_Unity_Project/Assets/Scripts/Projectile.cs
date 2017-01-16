using UnityEngine;

public class Projectile : MonoBehaviour
{
     private float fFlyTimer = 0;
     private float fGravity = 0;
    public Rigidbody rb = null;
     private GameObject goSender = null;

	void Start()
    {
        fFlyTimer = Time.time + .5f;

        // Distance * Sizes / SpeedPerFixedUpdate; Sizes is 1 van de Player's hoogte + .5 van de Projectile's straal
        float FlyingTime = CalculateDistance(GameObject.FindGameObjectWithTag("Player")) * 1.5f / 0.6818f;
        float StartingY = 3.6f;
        fGravity = StartingY / FlyingTime;

        goSender = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Update()
    {
        if (fFlyTimer > 0 && fFlyTimer - Time.time <= 0)
        {
            rb.AddForce(transform.forward * 1728);   // 12³, dozenmale 1.000. Makkelijker mee te rekenen
            fFlyTimer = -1;
        }
    }

    private void FixedUpdate()
    {
        if (fFlyTimer < 0)
        {
            transform.Translate(0, -fGravity, 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (goSender.GetComponent<BossAI>().iLives == 3)
                other.gameObject.GetComponent<Player>().LifeLoss(2);
            else
                other.gameObject.GetComponent<Player>().LifeLoss(3);
        }
        
        Destroy(this.gameObject);
    }

    private float CalculateDistance(GameObject Target)
    {
        float x_dist = transform.position.x - Target.transform.position.x;
        x_dist *= x_dist;
        float y_dist = transform.position.y - Target.transform.position.y;
        y_dist *= y_dist;
        float z_dist = transform.position.z - Target.transform.position.z;
        z_dist *= z_dist;

        return Mathf.Sqrt(x_dist + y_dist + z_dist);
    }
}
