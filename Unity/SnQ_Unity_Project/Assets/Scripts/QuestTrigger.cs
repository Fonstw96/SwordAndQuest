using UnityEngine;
using System.Collections;

public class QuestTrigger : MonoBehaviour {

    // made by delano

    private QuestManager questManager;
    public int questNumber;

    public bool startQuest;
    public bool endQuest;

	void Start () {
        questManager = FindObjectOfType<QuestManager>();
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!questManager.questCompleted[questNumber])
            {
                if(startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].gameObject.SetActive(true);
                    questManager.quests[questNumber].StartQuest();
                }

                if(endQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].EndQuest();
                }
            }
        }
    }
}
