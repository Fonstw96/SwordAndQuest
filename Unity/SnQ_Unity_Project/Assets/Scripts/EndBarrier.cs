using UnityEngine;
using System.Collections;

public class EndBarrier : MonoBehaviour {

    private DialogueManager dialogueManager;

    private GameObject playerscript;

    public Player player;

    public string[] dialogueLines;

    // Use this for initialization
    void Start()
    {
        playerscript = GameObject.FindGameObjectWithTag("Player");

        player = player.GetComponent<Player>();

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (player.sword == true)
        {
            Destroy(this.gameObject);
        }
        int Scene1Completed = PlayerPrefs.GetInt("Scene1Completed");
        if (Scene1Completed == 1)
        {
            Destroy(this.gameObject);
        }
        

    }
    void OnCollisionEnter(Collision _collision)
    {
        
        if (_collision.gameObject.tag == "Player")
        {
            
            if (player.sword == false) { 
                


                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
            }
        }
    }
}
