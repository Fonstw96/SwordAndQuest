using UnityEngine;
using System.Collections;

public class QuestObject : MonoBehaviour {

    // script by Delano

    public int questNumber;
    public QuestManager questManager;

    public string startText;
    public string endText;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void StartQuest()
    {
        questManager.ShowQuestText(startText);
    }

    public void EndQuest()
    {
        questManager.ShowQuestText(endText);
        questManager.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
