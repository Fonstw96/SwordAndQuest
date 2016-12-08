using UnityEngine;
using System.Collections;

public class DialogueHolder : MonoBehaviour {

    // Script by Delano

    private DialogueManager dialogueManager;

    public string[] dialogueLines;

    void Start () {
        dialogueManager = FindObjectOfType<DialogueManager>();
	}
	
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space) && !dialogueManager.dialogueBox.active)
            {
                if (!dialogueManager.dialogueActive)
                {
                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
                }

                // make npc stop moving
            }
        }
    }   
}
