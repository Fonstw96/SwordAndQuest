using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject[] goTarget;
     private bool bSwitched = false;
     private Animation aAction;
    public float fDownwards = 0;

    private void Start()
    {
        aAction = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (!bSwitched && other.gameObject.tag == "Player")
        {
            if (aAction != null)
            {
                aAction.Play();
            }

            bSwitched = true;

            if (goTarget.Length > 0)
            {
                foreach (GameObject switchable in goTarget)
                    switchable.transform.position = new Vector3(switchable.transform.position.x, -fDownwards, switchable.transform.position.z);
            }
            else
                Debug.Log("No target set");
        }
    }
}
