using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float spawnDelay = 2f;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    public void SetDelaySpawwnTime(float newDelay)
    {
        spawnDelay = newDelay;
    }
    public void Start()
    {
        for(int i = 0; i<poolSize; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab);
            enemy.SetActive(false);

            enemyPool.Enqueue(enemy);
        }
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnDelay);

    }
    private void SpawnEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            float randomX = Random.Range(-20f, 20f);;
            enemy.transform.position = new Vector3(randomX, transform.position.y, 0);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetEnemyGenerator(this);
                enemyScript.ResetEnemy();
            }

            enemy.SetActive(true);

        }
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
