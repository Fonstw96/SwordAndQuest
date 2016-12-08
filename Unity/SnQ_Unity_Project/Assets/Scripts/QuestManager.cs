using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

    // script by Delano

    public QuestObject[] quests;
    public bool[] questCompleted;

    public DialogueManager dialogueManager;

	void Start () {
        questCompleted = new bool[quests.Length];
	}
	
	void Update () {
	
	}

    public void ShowQuestText(string questText)
    {
        dialogueManager.dialogueLines = new string[1];
        dialogueManager.dialogueLines[0] = questText;
        dialogueManager.currentLine = 0;
        dialogueManager.ShowDialogue();
    }

}
