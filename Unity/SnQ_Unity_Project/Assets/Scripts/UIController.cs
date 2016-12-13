using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    private GameObject goLives;
    private GameObject goPlayer;
    private Player scrPlayer;

	void Start ()
    {
        goLives = GameObject.FindGameObjectWithTag("Lives");
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        scrPlayer = goPlayer.GetComponent<Player>();
    }
	
	void Update ()
    {
        int NewWidth = scrPlayer.levens * 80;
        goLives.GetComponent<RectTransform>().sizeDelta = new Vector2(NewWidth, 80);
	}
}
