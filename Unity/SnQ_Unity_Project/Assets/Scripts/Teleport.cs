using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string scenedestination;
    public int startpos;

    private GameObject player;

    public Player playerscript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerscript = player.GetComponent<Player>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && scenedestination != null)
        {
            PlayerPrefs.SetInt("startposision", startpos);
            SceneManager.LoadScene(scenedestination);
        }
    }
}