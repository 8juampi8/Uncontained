using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_scene : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}