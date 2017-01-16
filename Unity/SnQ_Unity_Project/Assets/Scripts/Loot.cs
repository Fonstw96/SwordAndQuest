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
                switch (iItem)
                {
                    case (1):
                        Debug.Log("You found a Health Potion!");
                        break;
                    case (2):
                        Debug.Log("You found Mythril Planks for your boat's bow!");
                        break;
                    case (3):
                        Debug.Log("You found a roll of cloth for your boat's sail!");
                        break;
                    case (4):
                        Debug.Log("You found a bag of tools to repair your boat with!");
                        break;
                    default:
                        Debug.Log("You found something! What could it be?");
                        break;
                }
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
