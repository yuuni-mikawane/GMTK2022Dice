using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class Enemy : MonoBehaviour
{
    public float hp = 1;
    public float firerate = 1; //atks per second
    public float bulletSpeed = 2;
    public float bulletDamage = 1;
    public GameObject attackPos;
    public GameObject bullet;
    public GameObject dieFX;
    private PlayerStats player;

    private float lastAttackTime;
    private float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.Instance;
        lastAttackTime = Time.time;
        nextAttackTime = lastAttackTime + 1f/firerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
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
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            dieFX.Spawn(transform.position);
            gameObject.Recycle();
        }
    }
}
