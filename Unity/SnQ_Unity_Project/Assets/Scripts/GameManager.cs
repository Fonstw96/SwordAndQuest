using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	void Start ()
    {
        PlayerPrefs.SetInt("startposision", 0);
        PlayerPrefs.SetInt("scenedes", 0);

        for (int r = 0; r < 9; r++)
            PlayerPrefs.SetInt("Inventory" + r, 0);
    }
}
