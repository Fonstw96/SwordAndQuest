using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
    public int iItem = 0;
    public GameObject goLid = null;
    private int iSteps = 0;
    public AudioClip sndOpen = null;

    private void FixedUpdate()
    {
        if (iItem == 0 && goLid != null && iSteps < 60)
        {
            goLid.transform.Rotate(-.2f, 0, 0);
            iSteps++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<Player>().bUse && Vector3.Angle(other.transform.forward, transform.position - other.transform.position) < 100 && iItem != 0)
        {
            int index = Array.IndexOf(other.GetComponent<Player>().iInventory, 0);

            if (index > -1)
            {
                switch (iItem)
                {
                    case (1):
                        print("You found a Health Potion!");
                        break;
                    case (2):
                        print("You found Mythril Planks for your boat's bow!");
                        break;
                    case (3):
                        print("You found a roll of cloth for your boat's sail!");
                        break;
                    case (4):
                        print("You found a bag of tools to repair your boat with!");
                        break;
                    default:
                        print("You found something! What could it be?");
                        break;
                }
                other.GetComponent<Player>().iInventory[index] = iItem;
                iItem = 0;

                if (sndOpen != null)
                AudioSource.PlayClipAtPoint(sndOpen, transform.position);
            }
            else
                print("Inventory is already full.");
        }
    }
}
