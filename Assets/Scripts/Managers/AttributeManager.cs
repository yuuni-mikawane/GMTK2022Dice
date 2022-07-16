using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class AttributeManager : SingletonBind<AttributeManager>
{
    private PlayerStats playerStats;
    private EnemyManager enemyManager;

    private void Start()
    {
        playerStats = PlayerStats.Instance;
        enemyManager = EnemyManager.Instance;
    }

    public void UpdateCurrentAttributes(int diceValue)
    {

    }
}
