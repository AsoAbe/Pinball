using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // どこからでもアクセスできるインスタンス
    public static ScoreManager Instance;

    // 初期スコア
    public const int DEFAULT_SCORE = 0;

    // 現在のスコア
    public int Score { get; private set; } = DEFAULT_SCORE;

    private void Awake()
    {
        // すでにインスタンスが存在する場合は重複を防ぐ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// スコアを加算する。
    /// </summary>
    public void AddScore(int value)
    {
        // 0以下の加算は行わない
        if (value <= 0)
        {
            return;
        }

        Score += value;
    }

    /// <summary>
    /// スコアを初期値に戻す。
    /// </summary>
    public void ResetScore()
    {
        Score = DEFAULT_SCORE;
    }
}