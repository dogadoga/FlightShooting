using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class StartGame : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("LoadGame");
        SceneTransition.I.LoadMainScene();
        //SceneManager.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        SceneTransition.I.LoadStartScene();
        if (GameManager.I.CurrentState == GameState.Start) SceneTransition.I.LoadMainScene();
    }
}
