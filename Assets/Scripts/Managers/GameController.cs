using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TMPro;

public class GameController : SingletonBind<GameController>
{
    public float killCamSlowMoTime = 3f;
    public float slowTo = 0.1f;

    public Room[] rooms;
    public Room chosenRoom;
    public GameState currentState;

    //private RoomManager roomManager;
    private EnemyManager enemyManager;
    private AttributeManager attributeManager;

    [Header("Scoring")]
    public int score;
    public float startTime;
    public float endTime;

    [Header("Text ingame")]
    public GameObject rollToStartObj;
    public GameObject gameOverObj;
    public TMP_Text scoreValueText;
    public TMP_Text timeValueText;

    private void Start()
    {
        Time.timeScale = 1f;
        currentState = GameState.SettingUp;
        //roomManager = RoomManager.Instance;
        enemyManager = EnemyManager.Instance;
        attributeManager = AttributeManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Pause)
            {
                
            }
            else
            {

            }
        }
    }

    public void ActivateKillCam()
    {
        StartCoroutine(KillCam(killCamSlowMoTime, slowTo));
    }

    private IEnumerator KillCam(float slowmoTime, float slowAmount)
    {
        Time.timeScale = slowAmount;
        yield return new WaitForSecondsRealtime(slowmoTime);
        Time.timeScale = 1f;
    }

    public void SetUpRoom(int diceValue)
    {
        if (currentState == GameState.SettingUp)
        {
            startTime = Time.time;
            currentState = GameState.Playing;
            chosenRoom = rooms[diceValue - 1];
            chosenRoom.gameObject.SetActive(true);
            rollToStartObj.SetActive(false);

        }
    }

    public void GameOver()
    {
        endTime = Time.time;
        gameOverObj.SetActive(true);
        scoreValueText.text = score.ToString();
        timeValueText.text = (endTime - startTime).ToString("F1") + "s";
        currentState = GameState.GameOver;
        enemyManager.KillAllBullets();
        Time.timeScale = 0.1f;
    }
}
