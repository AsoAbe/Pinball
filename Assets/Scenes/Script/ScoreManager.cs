
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // どこからでもアクセスできるインスタンス
    public static ScoreManager Instance;

    // 初期スコア
    public const int DEFAULT_SCORE = 0;

    // 初期連続ヒット数
    public const int DEFAULT_COMBO = 0;

    // 連続ヒットの判定時間（秒）
    public const float COMBO_TIME_LIMIT = 1.0f;

    // 現在のスコア
    public int Score { get; private set; } = DEFAULT_SCORE;

    // 連続ヒット数
    public int Combo { get; private set; } = DEFAULT_COMBO;

    // スコア倍率
    public int Multiplier { get; private set; } = 1;

    // 前回ヒットした時刻
    private float lastHitTime = -1.0f;

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

    private void Update()
    {
        // 一定時間ヒットがなければコンボをリセット
        if (Combo > 0 &&
            Time.time - lastHitTime > COMBO_TIME_LIMIT)
        {
            ResetCombo();
        }
    }

    /// <summary>
    /// ヒット時にスコアを加算する。
    /// </summary>
    public void AddComboScore(int value)
    {
        // 0以下の加算は行わない
        if (value <= 0)
        {
            return;
        }

        // 前回ヒットから1秒以内ならコンボを継続
        if (Combo > 0 &&
            Time.time - lastHitTime <= COMBO_TIME_LIMIT)
        {
            Combo++;
        }
        else
        {
            // 初回ヒット、または時間切れならコンボを開始
            Combo = 1;
        }

        // コンボ数を倍率にする
        Multiplier = Combo;

        // ヒット時刻を更新
        lastHitTime = Time.time;

        // 倍率を適用してスコアを加算
        Score += value * Multiplier;

        Debug.Log(
            "Combo: " + Combo +
            " / 倍率: " + Multiplier + "倍" +
            " / スコア: " + Score
        );
    }

    /// <summary>
    /// 連続ヒットをリセットする。
    /// </summary>
    public void ResetCombo()
    {
        Combo = DEFAULT_COMBO;
        Multiplier = 1;
        lastHitTime = -1.0f;
    }

    /// <summary>
    /// スコアを初期値に戻す。
    /// </summary>
    public void ResetScore()
    {
        Score = DEFAULT_SCORE;
        ResetCombo();
    }
}