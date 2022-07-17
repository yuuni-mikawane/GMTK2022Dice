using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : SingletonBind<GameController>
{
    public float killCamSlowMoTime = 3f;
    public float slowTo = 0.1f;

    public Room[] rooms;
    public Room chosenRoom;
    public GameState currentState;

    //private RoomManager roomManager;
    private EnemyManager enemyManager;
    public GameObject tutorialObj;

    [Header("Scoring")]
    public int score;
    public float startTime;
    public float endTime;

    [Header("Text ingame")]
    public GameObject rollToStartObj;
    public GameObject gameOverObj;
    public TMP_Text scoreValueText;
    public TMP_Text timeValueText;

    [Header("UI objs")]
    public GameObject pauseMenu;

    private GameState previousState;

    private void Start()
    {
        Time.timeScale = 1f;
        currentState = GameState.SettingUp;
        //roomManager = RoomManager.Instance;
        enemyManager = EnemyManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Pause)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void UnPause()
    {
        currentState = previousState;
        HideTutorial();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void Pause()
    {
        previousState = currentState;
        HideTutorial();
        currentState = GameState.Pause;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Restart()
    {
        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowTutorial()
    {
        tutorialObj.SetActive(true);
    }

    public void HideTutorial()
    {
        tutorialObj.SetActive(false);
    }

    public void Quit()
    {
        //quit to scene 0
        SceneManager.LoadScene(0);
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
