using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("LoadGame");
        SceneTransition.I.LoadMainScene();
        //SceneManager.LoadScene("GameScene");
    }
}
