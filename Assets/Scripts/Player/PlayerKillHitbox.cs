using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillHitbox : MonoBehaviour
{
    private PlayerStats player;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.Instance;
        damage = player.damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.isRolling)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
