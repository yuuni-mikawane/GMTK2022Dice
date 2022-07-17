using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using UnityEngine.Audio;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float hp = 1;
    public float firerate = 1; //atks per 2 second
    public float bulletSpeed = 2;
    public float bulletDamage = 1;
    public GameObject attackPos;
    public GameObject bullet;
    public GameObject dieFX;
    private PlayerStats player;

    private float lastAttackTime;
    private float nextAttackTime;

    private GameController gameController;
    private EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutCirc);
        gameController = GameController.Instance;
        player = PlayerStats.Instance;
        enemyManager = EnemyManager.Instance;
        lastAttackTime = Time.time;
        nextAttackTime = lastAttackTime + 2f/firerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.currentState == GameState.Playing && Time.time >= nextAttackTime)
        {
            lastAttackTime = Time.time;
            nextAttackTime = lastAttackTime + 2f / firerate;
            Attack();
        }
    }
     
    public void Attack()
    {
        Vector2 dir = player.transform.position - attackPos.transform.position;
        EnemyBullet shotBullet = bullet.Spawn(attackPos.transform.position).GetComponent<EnemyBullet>();
        shotBullet.InitializeBullet(bulletDamage, bulletSpeed, dir);
        enemyManager.bullets.Add(shotBullet);
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            dieFX.Spawn(transform.position);
            gameController.ActivateKillCam();
            gameController.score++;
            enemyManager.enemies.Remove(this);
            gameObject.Recycle();
        }
    }
}
