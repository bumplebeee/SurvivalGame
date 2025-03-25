using UnityEngine;

public class Enemy_Monster : Enemy
{
    [SerializeField] private GameObject energyEnemyPrefab; // Prefab của quái energy
    [SerializeField] private int spawnCount = 5; // Số lượng quái energy sinh ra
    [SerializeField] private float spawnRadius = 2f; // Bán kính sinh quái

    protected override void Die()
    {
        SpawnEnergyEnemies();
        base.Die();
    }

    private void SpawnEnergyEnemies()
    {
        if (energyEnemyPrefab != null)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                Instantiate(energyEnemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
