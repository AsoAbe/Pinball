using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    [SerializeField] private BallLauncher ballLauncher;

    private void OnTriggerEnter(Collider collider)
    {
        // ボール以外は処理しない
        if (!collider.CompareTag("Ball"))
        {
            return;
        }

        // GameManagerが存在しない場合は処理しない
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManagerが見つかりません。");
            return;
        }

        // ボールを削除する
        Destroy(collider.gameObject);

        // 残機を減らす
        GameManager.Instance.LoseLife();

        // ゲームオーバーならシーンを遷移する
        if (GameManager.Instance.isGameOver)
        {
            if (SceneChanger.Instance != null)
            {
                SceneChanger.Instance.GoToGameOverScene();
            }

            return;
        }

        // 残機があれば新しいボールを自動発射する
        if (ballLauncher != null)
        {
            ballLauncher.LaunchBall();
        }
        else
        {
            Debug.LogWarning("BallLauncherが設定されていません。");
        }
    }
}