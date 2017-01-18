using UnityEngine;
using System.Collections;

public class DialogueHolder : MonoBehaviour {

    // Script by Delano

    private DialogueManager dialogueManager;
    public GameObject npc;

    public string[] dialogueLines;

    void Start () {
        dialogueManager = FindObjectOfType<DialogueManager>();
	}
	
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space) && !dialogueManager.dialogueBox.activeSelf)
            {
                if (!dialogueManager.dialogueActive)
                {
                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
                    npc.GetComponent<BehaviourNpc>().isWalking = true;
                    Debug.Log("iswaling true door DialogueHolder");
                } 


            }
        }
    }   
}
