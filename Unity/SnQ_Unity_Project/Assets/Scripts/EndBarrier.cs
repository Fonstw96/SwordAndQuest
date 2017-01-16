using UnityEngine;
using System.Collections;

public class EndBarrier : MonoBehaviour {

    private DialogueManager dialogueManager;

    private GameObject player;

    public Player playerscript;

    public string[] dialogueLines;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerscript = player.GetComponent<Player>();

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (playerscript.sword == true)
        {

            Destroy(this.gameObject);
           
        }

    }
    void OnCollisionEnter(Collision _collision)
    {
        
        if (_collision.gameObject.tag == "Player")
        {
            
            if (playerscript.sword == false) { 
                


                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
            }
        }
    }
}
