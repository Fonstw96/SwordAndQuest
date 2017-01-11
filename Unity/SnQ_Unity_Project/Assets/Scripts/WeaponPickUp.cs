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
	
    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            myWeapon.SetActive(true);
            WeaponOnGround.SetActive(false);

            playerscript.sword = true;
        }
        }
    
}
