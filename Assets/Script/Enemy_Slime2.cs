using UnityEngine;

public class Enemy_Slime2 : Enemy
{
    private float lastDamageTime = 0f;
    [SerializeField] private float damageInterval = 0.5f;
    [SerializeField] private GameObject energyObject;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(10);
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
    protected override void Die()
    {
        if (energyObject != null)
        {
            GameObject energy = Instantiate(energyObject, transform.position,Quaternion.identity);
            Destroy(energy,5f);
        }
        base.Die();
    }

}
