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

    public AudioSource hurtFX;

    public TMP_Text hpText;
    private Color originalHpTextColor;
    private Animator animator;
    private GameController gameController;
    private AttributeManager attributeManager;

    private void Start()
    {
        originalHpTextColor = hpText.color;
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
            hpText.text = ((int)hp).ToString() + "HP";
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
            hurtFX.Play();
            if (hp <= 0)
            {
                hp = 0;
                //die
                gameController.GameOver();
            }
            else
            {
                StartCoroutine(TakeDamageTick());
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

    IEnumerator TakeDamageTick()
    {
        hpText.color = Color.red;
        yield return new WaitForSecondsRealtime(0.2f);
        hpText.color = originalHpTextColor;
    }
}
