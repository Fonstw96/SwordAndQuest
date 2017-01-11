using UnityEngine;
using System.Collections;

public class SwordSpawnController : MonoBehaviour {

    public GameObject Sword;
    private int random;
    private string SpawnLocation;

	// Use this for initialization
	void Start () {
        random = Random.Range(0, 5);
        Instantiate(Sword);
        random++;
        //random = 1;
        for (int i = 1; i < 6; i++)
        {
            SpawnLocation = "SwordSpawn" + i;
            if (random == i)
            {
                Sword.transform.position = GameObject.FindGameObjectWithTag(SpawnLocation).transform.position;
                Sword.transform.rotation = GameObject.FindGameObjectWithTag(SpawnLocation).transform.rotation;
            }
            Destroy(GameObject.FindGameObjectWithTag(SpawnLocation));
        }
        Debug.Log(random);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
