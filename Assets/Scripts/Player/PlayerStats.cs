using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class PlayerStats : SingletonBind<PlayerStats>
{
    public float hp = 10;
    public float maxHp = 10;
    public bool isRolling = false;
    public float damage = 1;
    public int currentDiceValue;

    public SpriteRenderer sprite;
    private Animator animator;
    private GameController gameController;

    private void Start()
    {
        hp = maxHp;
        currentDiceValue = Random.Range(1, 7);
        animator = GetComponent<Animator>();
        gameController = GameController.Instance;
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
            }
        }
    }

    public void Roll()
    {
        currentDiceValue = Random.Range(1, 7);
        //animation
        animator.SetInteger("diceValue", currentDiceValue);
        if (gameController.currentState == GameState.SettingUp)
        {
            gameController.SetUpRoom(currentDiceValue);
        }
    }
}
