using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using UnityEngine.SceneManagement;

public class SceneTransition : Singleton<SceneTransition>
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadStartScene()
    {
        LoadScene("Start");
        GameManager.I.SetState(GameManager.GameState.Start);
    }
    public void LoadMainScene()
    {
        LoadScene("GameScene");
        GameManager.I.SetState(GameManager.GameState.Prepare);
    }

    public void LoadEndScene()
    {
        LoadScene("End");
        GameManager.I.SetState(GameManager.GameState.End);
    }
}
