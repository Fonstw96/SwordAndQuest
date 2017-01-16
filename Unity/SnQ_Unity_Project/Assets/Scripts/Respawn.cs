using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public string currentScene;

    void Start () {
        currentScene = Application.loadedLevelName;
    }
	

	void Update () {
       
    }

    public void RespawnPlayer()
    {
        transform.GetComponent<Player>().bDead = false;

        if (currentScene == "Forest")
        {
            transform.position = new Vector3(95, 0.31f, 321);
        } else if (currentScene == "Temple")
        {
            transform.position = new Vector3(0, 0, 5);
        } else if (currentScene == "Maze")
        {
            transform.position = new Vector3(5, 0, 255);
        }

    }
}
