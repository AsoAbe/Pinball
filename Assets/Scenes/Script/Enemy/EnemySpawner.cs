using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("生成する敵Prefab")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("敵の生成位置")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("生成設定")]
    [SerializeField] private float coolTime = 5.0f;

    // 現在存在している敵の数
    private int aliveEnemyCount = 0;

    // クールタイム中のコルーチン
    private Coroutine spawnCoroutine;

    private void Start()
    {
        // 最初の敵を生成
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0 ||
            spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("敵Prefabまたは生成位置が登録されていません。");
            return;
        }

        // 登録された敵をすべて生成
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (enemyPrefabs[i] == null)
            {
                Debug.LogWarning("敵Prefabが登録されていません。");
                continue;
            }

            int spawnIndex = i % spawnPoints.Length;

            if (spawnPoints[spawnIndex] == null)
            {
                Debug.LogWarning("敵の生成位置が登録されていません。");
                continue;
            }

            GameObject enemy = Instantiate(
                enemyPrefabs[i],
                spawnPoints[spawnIndex].position,
                spawnPoints[spawnIndex].rotation
            );

            // 生成した敵にSpawnerを登録
            MiniEnemy miniEnemy = enemy.GetComponentInChildren<MiniEnemy>();

            if (miniEnemy != null)
            {
                miniEnemy.SetSpawner(this);
                aliveEnemyCount++;
            }
            else
            {
                Debug.LogWarning("生成した敵にMiniEnemyがありません。");
                Destroy(enemy);
            }
        }

        Debug.Log("現在の敵数：" + aliveEnemyCount);
    }

    public void EnemyDefeated()
    {
        // 敵の残数を減らす
        aliveEnemyCount--;

        if (aliveEnemyCount < 0)
        {
            aliveEnemyCount = 0;
        }

        Debug.Log("敵を撃破：残り " + aliveEnemyCount + "体");

        // 敵が全滅し、クールタイムが開始されていない場合
        if (aliveEnemyCount == 0 && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnAfterCoolTime());
        }
    }

    private IEnumerator SpawnAfterCoolTime()
    {
        Debug.Log("敵が全滅！クールタイム開始");

        // クールタイム
        yield return new WaitForSeconds(coolTime);

        // 再び敵を生成
        SpawnEnemies();

        // コルーチンの管理を終了
        spawnCoroutine = null;
    }
}