using UnityEngine;
using System.Collections;

public class EndBarrier : MonoBehaviour {

    private DialogueManager dialogueManager;

<<<<<<< HEAD
    private GameObject playerscript;

    public Player player;
=======
    private GameObject player;

    public Player playerscript;
>>>>>>> parent of f5ca85a... bye bye

    public string[] dialogueLines;

    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        playerscript = GameObject.FindGameObjectWithTag("Player");

        player = player.GetComponent<Player>();
=======
        player = GameObject.FindGameObjectWithTag("Player");

        playerscript = player.GetComponent<Player>();
>>>>>>> parent of f5ca85a... bye bye

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
<<<<<<< HEAD
        if (player.sword == true)
        {
            Destroy(this.gameObject);
        }
        int Scene1Completed = PlayerPrefs.GetInt("Scene1Completed");
        if (Scene1Completed == 1)
        {
            Destroy(this.gameObject);
        }
        
=======
        if (playerscript.sword == true)
        {

            Destroy(this.gameObject);
           
        }
>>>>>>> parent of f5ca85a... bye bye

    }
    void OnCollisionEnter(Collision _collision)
    {
        
        if (_collision.gameObject.tag == "Player")
        {
            
<<<<<<< HEAD
            if (player.sword == false) { 
=======
            if (playerscript.sword == false) { 
>>>>>>> parent of f5ca85a... bye bye
                


                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
            }
        }
    }
}
