using UnityEngine;

public class GameManager : MonoBehaviour
{
    // どこからでもアクセスできるインスタンス
    public static GameManager Instance;

    // ゲーム開始時のスコア
    public const int DEFAULT_SCORE = 0;

    // ゲーム開始時の残機数
    public const int DEFAULT_LIVES = 3;

    // スコア
    public int score = DEFAULT_SCORE    ;

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
        score = DEFAULT_SCORE;
        remainingLives = DEFAULT_LIVES;
        isGameOver = false;
    }

    // スコアを加算する
    public void AddScore(int value)
    {
        // ゲーム終了後はスコアを加算しない
        if (isGameOver)
        {
            return;
        }

        score += value;
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