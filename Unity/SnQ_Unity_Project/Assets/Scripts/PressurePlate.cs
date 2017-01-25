using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] goTargets = { };
    public float fHeight = 0;
    public AudioClip sndClick = null;
     private bool bPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (bPressed == false && other.tag == "Puzzle block" && goTargets.Length > 0)
        {
            foreach (GameObject Target in goTargets)
            {
                Vector3 pos = Target.transform.position;
                Target.transform.position = new Vector3(pos.x, fHeight, pos.z);
            }

            if (sndClick != null)
                AudioSource.PlayClipAtPoint(sndClick, transform.position);

            bPressed = true;
        }
    }
}
