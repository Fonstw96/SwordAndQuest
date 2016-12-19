using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public int iSwitchID = 0;
     private bool bSwitched = false;
     private GameObject goTarget;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bSwitched && other.CompareTag("Player"))
        {
            bSwitched = true;

            goTarget = GameObject.Find("Switchable" + iSwitchID);

            if (goTarget != null)
                goTarget.transform.Translate(0, -2.75f, 0);
            else
                Debug.Log("There is no object with the name \"Switchable" + iSwitchID + "\" to be found.");
        }
    }
}
