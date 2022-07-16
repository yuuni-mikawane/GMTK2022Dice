using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class EnemyManager : SingletonBind<EnemyManager>
{
    public GameObject enemyPrefab;
    private List<Enemy> enemies;

    private RoomManager roomManager;

    private void Start()
    {
        roomManager = RoomManager.Instance;
    }

    public void KillAllEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.Recycle();
            }
        }
        enemies.Clear();
    }

    public void SpawnEnemies(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Vector2 spawnPos = roomManager.currentRoom.GetRandomSpawnPoint();
            enemyPrefab.Spawn(spawnPos);
        }
    }
}
