using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene("Temple");
    }
}
