using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("“GPrefab‚Ü‚½‚Í¶¬ˆÊ’u‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
            return;
        }

        Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}