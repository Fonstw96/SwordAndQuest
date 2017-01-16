using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string sScene = null;

    private void OnCollisionEnter(Collision collision)
    {
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
    }
}
