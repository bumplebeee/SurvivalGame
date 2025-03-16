using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float lastDamageTime = 0f;
    [SerializeField] private float damageInterval = 0.5f;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firepoint;
    [SerializeField]
    private float speed = 20f;
    [SerializeField] private float skillCooldown = 2f;
    private float nextTimeSkill = 0f;

    protected override void Update()
    {
        base.Update();
        if (Time.time > nextTimeSkill)
        {
            ActiveSkill();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(5);
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damageInterval);
            lastDamageTime = Time.time;

        }
    }
    protected override void Die()
    {
        Debug.Log("Endgame");
        base.Die();
        SceneManager.LoadScene(2);
    }
    public void GayDam()
    {
        if (player != null) { 
        Vector3 direction=player.transform.position-firepoint.position;
        direction.Normalize();
        GameObject bullet=Instantiate(bulletPrefab,firepoint.position,Quaternion.identity);
        EnemyBulletsc enemyBulletsc =bullet.AddComponent<EnemyBulletsc>();
        enemyBulletsc.SetMovement(direction*speed);
        }
    }
    public void ActiveSkill()
    {
        nextTimeSkill = Time.time + skillCooldown;
        GayDam();
    }
    
}
