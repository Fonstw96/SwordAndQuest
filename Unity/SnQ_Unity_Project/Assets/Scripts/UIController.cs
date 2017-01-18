using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
     private GameObject goLives;
     private GameObject goPlayer;
     private Player cPlayer;
     private GameObject goStages;
     private GameObject goBoss;
     private BossAI cBoss;
     private bool bBoss = false;
    static public Image[] imInventory;
    public Sprite[] t2ItemIcons;

	void Start ()
    {
        goLives = GameObject.FindGameObjectWithTag("Lives");
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        cPlayer = goPlayer.GetComponent<Player>();

        imInventory = GameObject.FindGameObjectWithTag("Inventory").gameObject.GetComponentsInChildren<Image>();
        Debug.Log(imInventory.Length + " inventory positions found.");
        Debug.Log(t2ItemIcons.Length + " item icons found.");

        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            goStages = GameObject.FindGameObjectWithTag("Boss lives");
            goStages.SetActive(true);
            goBoss = GameObject.FindGameObjectWithTag("Boss");
            cBoss = goBoss.GetComponent<BossAI>();

            bBoss = true;
        }
    }
	
	void Update ()
    {
        // Lives
        int NewWidth = (cPlayer.levens * 13);
        goLives.GetComponent<RectTransform>().sizeDelta = new Vector2(NewWidth, 26);

        // Inventory
        for (int i=0; i<imInventory.Length - 1; i++)
        {
            if (cPlayer.iInventory[i] > 0)
            {
            	if (t2ItemIcons.Length >= cPlayer.iInventory[i] - 1)
            	{
	                imInventory[i + 1].sprite = t2ItemIcons[cPlayer.iInventory[i] - 1];
                	imInventory[i + 1].color = new Color(1, 1, 1, 1);
                }
            }
            else if (imInventory[i + 1].color.a > 0)
                imInventory[i + 1].color = new Color(0, 0, 0, 0);
        }

        if (bBoss)
        {
            NewWidth = cBoss.iLives * 100;
            goStages.GetComponent<RectTransform>().sizeDelta = new Vector2(NewWidth, 136);
        }
	}
}