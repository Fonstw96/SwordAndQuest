using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
     private GameObject goLives;
     private GameObject goPlayer;
     private Player scrPlayer;
    static public Image[] imInventory;
    public Sprite[] t2ItemIcons;

	void Start ()
    {
        goLives = GameObject.FindGameObjectWithTag("Lives");
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        scrPlayer = goPlayer.GetComponent<Player>();

        imInventory = GameObject.FindGameObjectWithTag("Inventory").gameObject.GetComponentsInChildren<Image>();
        Debug.Log(imInventory.Length + " inventory positions found.");
        Debug.Log(t2ItemIcons.Length + " item icons found.");
    }
	
	void Update ()
    {
        // Lives
        int NewWidth = scrPlayer.levens * 80;
        goLives.GetComponent<RectTransform>().sizeDelta = new Vector2(NewWidth, 80);

        // Inventory
        for (int i=0; i<imInventory.Length - 1; i++)
        {
            if (scrPlayer.iInventory[i] > 0)
            {
            	if (t2ItemIcons.Length >= scrPlayer.iInventory[i] - 1)
            	{
	                imInventory[i + 1].sprite = t2ItemIcons[scrPlayer.iInventory[i] - 1];
                	imInventory[i + 1].color = new Color(1, 1, 1, 1);
                }
            }
            else if (imInventory[i + 1].color.a > 0)
                imInventory[i + 1].color = new Color(0, 0, 0, 0);
        }
	}
}
