using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector2 direction;
    private Rigidbody2D rb;
    private PlayerStats player;
    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        player = PlayerStats.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.TakeDamage(damage);
        }
        //ContactPoint2D[] contactPoints = new ContactPoint2D[10];
        //collision.GetContacts(contactPoints);
        //particles.Spawn(contactPoints[0].point);
        particles.Spawn(collision.ClosestPoint(transform.position));
        Destroy(gameObject);
    }

    public void InitializeBullet(float dmg, float spd, Vector2 dir)
    {
        damage = dmg;
        speed = spd;
        direction = dir;
    }
}
