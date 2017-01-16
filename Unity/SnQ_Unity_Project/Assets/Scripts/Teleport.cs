using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
<<<<<<< HEAD
    public string scene;
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
=======
    public string sScene = null;
>>>>>>> origin/Fons

    private void OnCollisionEnter(Collision collision)
    {
<<<<<<< HEAD
        if (other.gameObject.tag == "Player" && scene != null)
            SceneManager.LoadScene(scene);
=======
        if (collision.gameObject.tag == "Player" && sScene != null)
        {
            int i = 0;
            int[] ItemList = collision.gameObject.GetComponent<Player>().iInventory;
            foreach (int Item in ItemList)
            {
                PlayerPrefs.SetInt("Inventory" + i, Item);
                i++;
            }

            SceneManager.LoadScene(sScene);
        }
>>>>>>> origin/Fons
    }
}
