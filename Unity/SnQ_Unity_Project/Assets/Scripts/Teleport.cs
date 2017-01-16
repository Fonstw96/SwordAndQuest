using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string sScene = null;
    public int iKey = -1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("FIRST Item " + iKey + " is needed as a key.");

        if (other.gameObject.tag == "Player" && sScene != null)
        {
            Debug.Log("Item " + iKey + " is needed as a key.");
            int i = 0;
            Debug.Log("Item " + iKey + " is needed as a key.");
            int[] ItemList = other.gameObject.GetComponent<Player>().iInventory;
            Debug.Log("Item " + iKey + " is needed as a key.");
            foreach (int Item in ItemList)
            {
                Debug.Log("Item " + iKey + " is needed as a key.");
                PlayerPrefs.SetInt("Inventory" + i, Item);
                Debug.Log("Item " + iKey + " is needed as a key.");
                i++;
                Debug.Log("Item " + iKey + " is needed as a key.");
            }

            Debug.Log("Item " + iKey + " is needed as a key.");
            if (Array.IndexOf(ItemList, iKey) > -1 || iKey == -1)
            {
                Debug.Log("Item " + iKey + " is needed as a key.");
                SceneManager.LoadScene(sScene);
                Debug.Log("Item " + iKey + " is needed as a key.");
            }
            Debug.Log("Item " + iKey + " is needed as a key. LAST");
        }
    }
}
