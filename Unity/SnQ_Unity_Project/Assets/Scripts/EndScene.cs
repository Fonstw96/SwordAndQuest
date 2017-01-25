using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

    public int thisscene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Scene1Completed", 1);

        }
    }
}
