using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string scene;
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && scene != null)
            SceneManager.LoadScene(scene);
    }
}
