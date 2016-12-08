using UnityEngine;
using System.Collections;

public class WeaponPickUp : MonoBehaviour {

    public GameObject myWeapon;
    public GameObject WeaponOnGround;

	// Use this for initialization
	void Start () {
        myWeapon.SetActive(false);
	}
	
    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            myWeapon.SetActive(true);
            WeaponOnGround.SetActive(false);
        }
        }
    
}
