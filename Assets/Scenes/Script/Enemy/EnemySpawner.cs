using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("¶¬‚·‚é“GPrefab")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("“G‚Ì¶¬ˆÊ’u")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("¶¬İ’è")]
    [SerializeField] private float coolTime = 5.0f;

    private void Start()
    {
        // Å‰‚Ì“G‚ğ¶¬
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            int spawnIndex = i % spawnPoints.Length;
            SpawnEnemy(i, spawnIndex);
        }
    }

    // “G‚ğ1‘Ì¶¬
    private void SpawnEnemy(int prefabIndex, int spawnIndex)
    {
        if (enemyPrefabs[prefabIndex] == null ||
            spawnPoints[spawnIndex] == null)
        {
            Debug.LogWarning("“GPrefab‚Ü‚½‚Í¶¬ˆÊ’u‚ª“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
            return;
        }

        GameObject enemy = Instantiate(
            enemyPrefabs[prefabIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );

        // ¶¬‚µ‚½“G‚ÉSpawner‚Æ¯•Ê”Ô†‚ğ“o˜^
        MiniEnemy miniEnemy = enemy.GetComponentInChildren<MiniEnemy>();

        if (miniEnemy != null)
        {
            miniEnemy.SetSpawner(this, prefabIndex, spawnIndex);
        }
        else
        {
            Debug.LogWarning("¶¬‚µ‚½“G‚ÉMiniEnemy‚ª‚ ‚è‚Ü‚¹‚ñB");
            Destroy(enemy);
        }
    }

    // “G‚ª“|‚³‚ê‚½‚Æ‚«‚ÉŒÄ‚Ño‚·
    public void EnemyDefeated(int prefabIndex, int spawnIndex)
    {
        StartCoroutine(RespawnEnemy(prefabIndex, spawnIndex));
    }

    // ŒÂ•Ê‚ÌƒN[ƒ‹ƒ^ƒCƒ€Œã‚É•œŠˆ
    private IEnumerator RespawnEnemy(int prefabIndex, int spawnIndex)
    {
        Debug.Log("“G‚Ì•œŠˆƒN[ƒ‹ƒ^ƒCƒ€ŠJn");

        yield return new WaitForSeconds(coolTime);

        SpawnEnemy(prefabIndex, spawnIndex);

        Debug.Log("“G‚ª•œŠˆ‚µ‚Ü‚µ‚½");
    }
}