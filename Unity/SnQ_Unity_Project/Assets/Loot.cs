using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    public int iItem = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Vector3.Angle(other.transform.forward, transform.position - other.transform.position) < 15 && iItem != 0)
        {
            int[] inventory = other.GetComponent<Player>().iInventory;
            for (int s=0; s<inventory.Length; s++)
            {
                if (inventory[s] == 0)
                {
                    other.GetComponent<Player>().iInventory[s] = iItem;
                    iItem = 0;
                }
            }
        }
    }
}
