using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
            PlayerPrefs.SetInt("startposision", 0);
            PlayerPrefs.SetInt("scenedes", 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
