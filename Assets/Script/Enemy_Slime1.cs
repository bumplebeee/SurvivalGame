using UnityEngine;

public class Enemy_Slime1 : Enemy
{
    private float lastDamageTime = 0f;
    [SerializeField] private float damageInterval = 0.5f;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(5);
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= lastDamageTime + damageInterval)
        {
            player.TakeDamage(damageInterval);
            lastDamageTime = Time.time;
        }
    }

}
