using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Stage1Clear()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Stage2Clear()
    {
        SceneManager.LoadScene("ClearScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
