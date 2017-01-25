using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

    public void ExitUnityGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif


    }
}
