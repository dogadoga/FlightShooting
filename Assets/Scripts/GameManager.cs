using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using TMPro;
using unityroom.Api;

public class GameManager : DesignPatterns.Singleton<GameManager>
{
    public enum GameState
    {
        Start,
        Prepare,
        Play,
        End
    }

    public GameState CurrentState { get; private set; }
    private GameState AwakeState = GameState.Start;
    public bool isEnemyDefeated = false;
    public bool isPlayerDefeated = false;
    public bool isEnded = false;
    public GameObject endPanel;
    public TextMeshProUGUI endText;
    public AudioSource EndingAS;
    public AudioClip endingTheme;


    public void Init()
    {
        CurrentState = AwakeState;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // リスタート
        if(CurrentState == GameState.End && Input.GetKey(KeyCode.R))
        {
            SceneTransition.I.LoadStartScene();
            if (CurrentState == GameState.Start) SceneTransition.I.LoadMainScene();
        }
        //if(CurrentState == GameState.Start && Input.GetKey(KeyCode.R))
        //{
        //    //InitiateAll();
        //    SceneTransition.I.LoadMainScene();
        //}
    }
    /// <summary>
    /// 状態遷移
    /// </summary>
    /// <param name="gameState"></param>
    public void SetState(GameState gameState)
    {
        CurrentState = gameState;
    }

    /// <summary>
    /// パラメータを初期化
    /// </summary>
    public void InitiateAll()
    {
        EnemyManager.I.Init();
        Timer.I.Init();
        Init();
    }
    public void EndGame()
    {
        SceneTransition.I.LoadEndScene();
        // エンディングテーマ
        EndingAS.clip = endingTheme;
        EndingAS.Play();

        endPanel.SetActive(true);


        if (isPlayerDefeated)
        {
            endText.text = "配達失敗！\n\n";
            endText.text += "Press R to Restart";
            UnityroomApiClient.Instance.SendScore(1, 9999f, ScoreboardWriteMode.HighScoreAsc);
        }
        else
        {
            endText.text = "配達成功！\n";
            endText.text += "クリアタイム: "+Timer.I.timeString + "\n\n";
            endText.text += "Press R to Restart";
            // ボードNo1にスコア123.45fを送信する。
            UnityroomApiClient.Instance.SendScore(1, Timer.I.timeElapsed, ScoreboardWriteMode.HighScoreAsc);
        }
    }
}
