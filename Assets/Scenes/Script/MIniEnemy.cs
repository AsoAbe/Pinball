using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    // 撃破時に加算するスコア
    [SerializeField] private int scoreValue = 100;

    // 撃破済みかどうか
    private bool isDefeated = false;

    private void OnCollisionEnter(Collision collision)
    {
        // ボール以外は処理しない
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        // 撃破済みなら処理しない
        if (isDefeated)
        {
            return;
        }

        isDefeated = true;

        // スコアを加算する
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddComboScore(scoreValue);
        }

        // 敵を削除する
        Destroy(gameObject);
    }
}