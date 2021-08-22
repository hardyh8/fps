using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vignette : MonoBehaviour
{
    public String SceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
