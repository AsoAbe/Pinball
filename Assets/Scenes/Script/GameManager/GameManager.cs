using UnityEngine;

public class GameManager : MonoBehaviour
{
    // どこからでもアクセスできるインスタンス
    public static GameManager Instance;

    // ゲーム開始時の残機数
    public const int DEFAULT_LIVES = 3;

    // 残機数
    public int remainingLives = DEFAULT_LIVES;

    // ゲーム中かどうか
    public bool isGameOver = false;

    private void Awake()
    {
        // すでにインスタンスが存在する場合は重複を防ぐ
        if (Instance == null)
        {
            Instance = this;

            // シーンを切り替えてもオブジェクトを維持する
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ゲーム開始時に状態を初期化する
    public void ResetGame()
    {
        // スコアも初期化する
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        remainingLives = DEFAULT_LIVES;
        isGameOver = false;
    }

    // 残機を減らす
    public void LoseLife()
    {
        // ゲーム終了後は処理しない
        if (isGameOver)
        {
            return;
        }

        remainingLives--;

        // 残機がなくなったらゲーム終了
        if (remainingLives <= 0)
        {
            remainingLives = 0;
            isGameOver = true;
        }
    }
}