using UnityEngine;
using System.Collections;

public class WeaponPickUp : MonoBehaviour {

    public GameObject myWeapon;
    public GameObject WeaponOnGround;

    private GameObject player;

    public Player playerscript;

	// Use this for initialization
	void Start () {
        myWeapon.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");

        playerscript = player.GetComponent<Player>();
    }
<<<<<<< HEAD

    void OnTriggerEnter(Collider _collider)
    {
        int Scene1Completed = PlayerPrefs.GetInt("Scene1Completed");
        if (Scene1Completed == 0)
        {

            if (_collider.gameObject.tag == "Player")
            {
                myWeapon.SetActive(true);
                WeaponOnGround.SetActive(false);

                playerscript.sword = true;
            }
        }
    }
=======
	
    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            myWeapon.SetActive(true);
            WeaponOnGround.SetActive(false);

            playerscript.sword = true;
        }
        }
>>>>>>> parent of f5ca85a... bye bye
    
}
