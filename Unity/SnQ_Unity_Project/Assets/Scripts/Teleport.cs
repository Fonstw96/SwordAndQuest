using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
<<<<<<< HEAD
    public string scenedestination;
    public int startpos;

    private GameObject player;

    public Player playerscript;
    void Start()
=======
<<<<<<< HEAD
    public string scene;
	void Start ()
>>>>>>> master
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerscript = player.GetComponent<Player>();
    }

    void Update()
    {
<<<<<<< HEAD
    }
=======
	
	}
=======
    public string sScene = null;
>>>>>>> origin/Fons
>>>>>>> master

    private void OnCollisionEnter(Collision collision)
    {
<<<<<<< HEAD
        if (other.gameObject.tag == "Player" && scenedestination != null)
        {
            PlayerPrefs.SetInt("startposision", startpos);
            SceneManager.LoadScene(scenedestination);
        }
=======
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
>>>>>>> master
    }
}