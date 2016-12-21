using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
    public int iItem = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<Player>().bAttack && Vector3.Angle(other.transform.forward, transform.position - other.transform.position) < 25 && iItem != 0)
        {
            int index = Array.IndexOf(other.GetComponent<Player>().iInventory, 0);

            if (index > -1)
            {
                Debug.Log("You found a Health Potion!");
                other.GetComponent<Player>().iInventory[index] = iItem;
                iItem = 0;
            }
            else
            {
                Debug.Log("Inventory is already full.");
                // dialogue inv full
            }
        }
    }
}
