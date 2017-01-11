using UnityEngine;
using System.Collections;

public class QuestBasic : MonoBehaviour {

    public GameObject questId;
    public GameObject questManager;

    public bool questStarted;
    public bool questComplete;

    void Start () {
        questManager = GameObject.FindGameObjectWithTag("QuestManager");
    }
	

	void Update () {
        if (questId.activeInHierarchy && !questStarted)
        {
            Debug.Log("quest_started");
            questStarted = true;
        } else if (!questId.activeInHierarchy && questStarted)
        {
            Debug.Log("quest_complete");
            questStarted = false;
            questComplete = true;
        }
    }
}
