﻿using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class StartGame : MonoBehaviour {

public void onClick(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }


public void onClick(string sceneName)
{
    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
}
}
