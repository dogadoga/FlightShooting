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
        // ���X�^�[�g
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
    /// ��ԑJ��
    /// </summary>
    /// <param name="gameState"></param>
    public void SetState(GameState gameState)
    {
        CurrentState = gameState;
    }

    /// <summary>
    /// �p�����[�^��������
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
        // �G���f�B���O�e�[�}
        EndingAS.clip = endingTheme;
        EndingAS.Play();

        endPanel.SetActive(true);


        if (isPlayerDefeated)
        {
            endText.text = "�z�B���s�I\n\n";
            endText.text += "Press R to Restart";
            UnityroomApiClient.Instance.SendScore(1, 9999f, ScoreboardWriteMode.HighScoreAsc);
        }
        else
        {
            endText.text = "�z�B�����I\n";
            endText.text += "�N���A�^�C��: "+Timer.I.timeString + "\n\n";
            endText.text += "Press R to Restart";
            // �{�[�hNo1�ɃX�R�A123.45f�𑗐M����B
            UnityroomApiClient.Instance.SendScore(1, Timer.I.timeElapsed, ScoreboardWriteMode.HighScoreAsc);
        }
    }
}
