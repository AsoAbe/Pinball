using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    // 撃破時に加算するスコア
    [SerializeField] private int scoreValue = 100;

    // 撃破済みかどうか
    private bool isDefeated = false;

    private EnemySpawner enemySpawner;

    // 生成時のPrefabのインデックス
    private int prefabIndex;
    private int spawnIndex;

    // Spawnerを登録する
    public void SetSpawner(EnemySpawner spawner, int prefabIndex, int spawnIndex)
    {
        enemySpawner = spawner;
        this.prefabIndex = prefabIndex;
        this.spawnIndex = spawnIndex;
    }

    private void Update()
    {
        // デバッグ用：Qキーで敵を倒す
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DefeatEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ボール以外は処理しない
        if (!other.CompareTag("Ball"))
        {
            return;
        }

        DefeatEnemy();
    }

    // 敵の死亡処理
    private void DefeatEnemy()
    {
        // 撃破済みなら処理しない
        if (isDefeated)
        {
            return;
        }

        isDefeated = true;

        // Spawnerに敵が倒されたことを通知
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated(prefabIndex, spawnIndex);
        }

        // スコアを加算する
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddComboScore(scoreValue);
        }

        // 敵を削除する
        Destroy(gameObject);
    }
}