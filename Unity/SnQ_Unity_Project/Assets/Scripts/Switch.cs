using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject[] goTarget;
     private bool bSwitched = false;
    public AudioClip sndClick = null;
     private Animation aAction;
    public float fHeight = 0;

    private void Start()
    {
        aAction = GetComponent<Animation>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!bSwitched && other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Player>().bUse)
            {
                if (sndClick != null)
                    AudioSource.PlayClipAtPoint(sndClick, transform.position);
                if (aAction != null)
                    aAction.Play();

                bSwitched = true;

                if (goTarget.Length > 0)
                {
                    foreach (GameObject switchable in goTarget)
                    {
                        Vector3 pos = switchable.transform.position;
                        switchable.transform.position = new Vector3(pos.x, fHeight, pos.z);
                    }
                }
                else
                    print("No target set");
            }
        }
    }
}
