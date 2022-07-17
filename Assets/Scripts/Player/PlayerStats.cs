using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : SingletonBind<PlayerStats>
{
    public float hp = 10;
    public float maxHp = 10;
    public bool isRolling = false;
    public float damage = 1;
    public int currentDiceValue;

    public TMP_Text hpText;
    private Animator animator;
    private GameController gameController;
    private AttributeManager attributeManager;

    private void Start()
    {
        hp = maxHp;
        currentDiceValue = Random.Range(1, 7);
        animator = GetComponent<Animator>();
        gameController = GameController.Instance;
        attributeManager = AttributeManager.Instance;
    }

    private void Update()
    {
        if (gameController.currentState == GameState.GameOver)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                //reload scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (gameController.currentState == GameState.Playing)
        {
            hpText.text = "HP " + ((int)hp).ToString();
            hpText.gameObject.SetActive(true);
        }
        else
        {
            hpText.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isRolling)
        {
            hp -= amount;
            if (hp <= 0)
            {
                hp = 0;
                //die
                gameController.GameOver();
            }
        }
    }

    public void RollDiceValue()
    {
        currentDiceValue = Random.Range(1, 7);
        //animation
        animator.SetInteger("diceValue", currentDiceValue);
        if (gameController.currentState == GameState.SettingUp)
        {
            gameController.SetUpRoom(currentDiceValue);
        }
        else if (gameController.currentState == GameState.Playing)
        {
            attributeManager.UpdateCurrentAttributes(currentDiceValue);
        }
    }
}
