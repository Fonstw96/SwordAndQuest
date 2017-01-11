using UnityEngine;
using System.Collections;

public class EscortQuest : MonoBehaviour {

    public GameObject thisObject;

	void Start () {
        thisObject = this.gameObject;
	}

	void Update () {
        if (thisObject.GetComponent<QuestBasic>().questStarted)
        {

        } 
    }

}
