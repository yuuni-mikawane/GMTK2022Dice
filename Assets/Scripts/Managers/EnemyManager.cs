using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class EnemyManager : SingletonBind<EnemyManager>
{
    public GameObject[] enemyPrefabs;
    public float enemySpawnRate; //number of enemies every 10 seconds
    public float spawnRateIncreaseMultiplier;
    public List<Enemy> enemies;
    public List<EnemyBullet> bullets;
    public float fireRateIncreaseMultiplier;
    public float bulletSpeedIncreaseMultiplier;
    private float fireRate;
    private float bulletSpeed;
    private float nextSpawnTime;
    private bool firstSpawn = true;

    //private RoomManager roomManager;
    private GameController gameController;

    private void Start()
    {
        //roomManager = RoomManager.Instance;
        gameController = GameController.Instance;
        fireRate = enemyPrefabs[0].GetComponent<Enemy>().firerate;
        bulletSpeed = enemyPrefabs[0].GetComponent<Enemy>().bulletSpeed;
    }

    private void Update()
    {
        if (gameController.currentState == GameState.Playing && Time.time > nextSpawnTime)
        {
            if (firstSpawn)
            {
                nextSpawnTime = Time.time + 3f;
                firstSpawn = false;
            }
            else
            {
                nextSpawnTime = Time.time + 10f / enemySpawnRate;
                SpawnEnemies();
            }
        }
    }

    public void KillAllBullets()
    {
        //foreach (GameObject enemy in enemies)
        //{
        //    if (enemy != null)
        //    {
        //        enemy.Recycle();
        //    }
        //}
        //enemies.Clear();

        foreach (EnemyBullet bullet in bullets)
        {
            if (bullet != null)
            {
                bullet.SelfDestruct();
                bullet.Recycle();
            }
        }
        bullets.Clear();
    }

    //public void CheckRoomClear()
    //{
    //    if (enemies.Count == 0)
    //    {
    //        KillAllBullets();
    //        gameController.RoomCleared();
    //    }
    //}

    public void AddFireRate(float amount)
    {
        fireRate += amount * fireRateIncreaseMultiplier;
    }

    public void AddBulletSpeed(float amount)
    {
        bulletSpeed += amount * bulletSpeedIncreaseMultiplier;
    }

    public void AddSpawnRate(float amount)
    {
        enemySpawnRate += spawnRateIncreaseMultiplier * amount;
    }

    public void SpawnEnemies()
    {
        Vector2 spawnPos = gameController.chosenRoom.GetRandomSpawnPoint();
        Enemy spawnedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].Spawn(gameObject.transform, spawnPos).GetComponent<Enemy>();
        spawnedEnemy.firerate = fireRate;
        spawnedEnemy.bulletSpeed = bulletSpeed;
        enemies.Add(spawnedEnemy);
    }
}
