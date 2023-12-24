using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using TMPro;

public class Timer : Singleton<Timer>
{
    public float timeElapsed { get; private set; }
    public string timeString;
    [SerializeField] private TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I.CurrentState == GameManager.GameState.Play)
        {
            timeElapsed += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed - minutes * 60);
            int mseconds = Mathf.FloorToInt((timeElapsed - minutes * 60 - seconds) * 1000);
            timeString = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, mseconds);

            timerText.text = timeString;
        }
    }

    /// <summary>
    /// èâä˙âª
    /// </summary>
    public void Init()
    {
        timeElapsed = 0f;
        GameManager.I.SetState(GameManager.GameState.Play);
    }
}
