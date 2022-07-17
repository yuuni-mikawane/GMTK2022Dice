using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TMPro;

public class AttributeManager : SingletonBind<AttributeManager>
{
    private PlayerStats playerStats;
    private EnemyManager enemyManager;

    public IngameAttribute nextEventID;

    public TMP_Text eventTitleText;
    public TMP_Text eventContentText;
    public TMP_Text diceValueText;
    public TMP_Text nextEventText;
    public TMP_Text nextTimerText;

    public int currentDiceValue = 0;
    public float eventGapTime;
    private float nextEventTime;

    private float timeLeftUntilNextEvent;

    private GameController gameController;
    private bool start = false;

    private void Start()
    {
        playerStats = PlayerStats.Instance;
        enemyManager = EnemyManager.Instance;
        gameController = GameController.Instance;
        RandomizeNextEvent();
    }

    private void Update()
    {
        if (gameController.currentState == GameState.Playing)
        {
            if (!start)
            {
                start = true;
                nextEventTime = Time.time + eventGapTime;
            }
            timeLeftUntilNextEvent = nextEventTime - Time.time;
            if (timeLeftUntilNextEvent <= 0)
            {
                DisplayCurrentEvent();
            }
            else
            {
                DisplayNextEvent();
            }
        }
        else if (gameController.currentState == GameState.GameOver)
        {
            nextEventText.gameObject.SetActive(false);
            nextTimerText.gameObject.SetActive(false);
            eventTitleText.gameObject.SetActive(false);
            eventContentText.gameObject.SetActive(false);
            diceValueText.gameObject.SetActive(false);
        }
    }

    private void DisplayCurrentEvent()
    {
        if (currentDiceValue == 0)
        {
            diceValueText.color = Color.white;
            diceValueText.text = "YOU ROLLED ?";
        }
        else
        {
            diceValueText.color = Color.green;
            diceValueText.text = "YOU ROLLED " + currentDiceValue;
        }

        switch (nextEventID)
        {
            case IngameAttribute.EnemyFireRate:
                eventContentText.text = "ENEMY FIRERATE";
                break;
            case IngameAttribute.EnemySpawnRate:
                eventContentText.text = "ENEMY SPAWN RATE";
                break;
            case IngameAttribute.EnemyBulletSpeed:
                eventContentText.text = "BULLET SPEED";
                break;
            case IngameAttribute.PlayerHeal:
                eventContentText.text = "PLAYER HP";
                break;
        }

        eventTitleText.gameObject.SetActive(true);
        eventContentText.gameObject.SetActive(true);
        diceValueText.gameObject.SetActive(true);
        nextEventText.gameObject.SetActive(false);
        nextTimerText.gameObject.SetActive(false);
    }

    private void DisplayNextEvent()
    {
        nextTimerText.text = timeLeftUntilNextEvent.ToString("F1");

        eventTitleText.gameObject.SetActive(false);
        eventContentText.gameObject.SetActive(false);
        diceValueText.gameObject.SetActive(false);
        nextEventText.gameObject.SetActive(true);
        nextTimerText.gameObject.SetActive(true);
    }

    public void UpdateCurrentAttributes(int diceValue)
    {
        //apply next event
        if (timeLeftUntilNextEvent <= 0 && currentDiceValue == 0)
        {
            currentDiceValue = diceValue;
            switch (nextEventID) {
                case IngameAttribute.EnemyFireRate:
                    enemyManager.AddFireRate(diceValue);
                    break;
                case IngameAttribute.EnemySpawnRate:
                    enemyManager.AddSpawnRate(diceValue);
                    break;
                case IngameAttribute.EnemyBulletSpeed:
                    enemyManager.AddBulletSpeed(diceValue);
                    break;
                case IngameAttribute.PlayerHeal:
                    playerStats.hp += diceValue;
                    if (playerStats.hp > playerStats.maxHp)
                    {
                        playerStats.hp = playerStats.maxHp;
                    }
                    break;
            }

            StartCoroutine(WaitForEventDisplay());
        }
    }

    IEnumerator WaitForEventDisplay()
    {
        //apply the event
        yield return new WaitForSecondsRealtime(5f);
        nextEventTime = Time.time + eventGapTime;
        currentDiceValue = 0;
        RandomizeNextEvent();
    }

    private void RandomizeNextEvent()
    {
        nextEventID = (IngameAttribute)Random.Range(0, (int)IngameAttribute.AmountOfEvents);
    }
}
