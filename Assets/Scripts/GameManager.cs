using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;



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

    public void SetState(GameState gameState)
    {
        CurrentState = gameState;

    }

    /// <summary>
    /// ƒpƒ‰ƒ[ƒ^‚ğ‰Šú‰»
    /// </summary>
    public void InitiateAll()
    {
        EnemyManager.I.Init();
        Timer.I.Init();
        Init();

    }

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
    }
}
