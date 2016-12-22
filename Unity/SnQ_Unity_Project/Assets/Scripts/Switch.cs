using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject[] goTarget;
     private bool bSwitched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!bSwitched && other.CompareTag("Player"))
        {
            bSwitched = true;

            if (goTarget.Length > 0)
            {
                foreach (GameObject switchable in goTarget)
                    switchable.transform.position = new Vector3(switchable.transform.position.x, -5.75f, switchable.transform.position.z);
            }
            else
                Debug.Log("No target set");
        }
    }
}
