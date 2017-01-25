using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    public string scenedestination;
    public int startpos;

<<<<<<< HEAD

	void Start ()
    {
=======
    private GameObject player;

    public Player playerscript;



	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerscript = player.GetComponent<Player>();
>>>>>>> parent of f5ca85a... bye bye
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" && scenedestination != null)
        {
            int i = 0;
            int[] ItemList = collision.gameObject.GetComponent<Player>().iInventory;
            foreach (int Item in ItemList)
            {
                PlayerPrefs.SetInt("Inventory" + i, Item);
                i++;
            }

            PlayerPrefs.SetInt("startposision", startpos);
            SceneManager.LoadScene(scenedestination);
        }


    }
}