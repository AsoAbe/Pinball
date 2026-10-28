using UnityEngine;
using System;

public class GameTimer : MonoBehaviour
{
    // タイマーの初期時間（秒）
    public const float DEFAULT_TIME = 30f;

    // 残り時間
    public float remainingTime = DEFAULT_TIME;

    // タイマーが動作中かどうか
    public bool IsRunning { get; private set; }

    // 時間切れになったときに通知するイベント
    public event Action OnTimeUp;

    private bool hasFinished;

    private void Update()
    {
        // 停止中または終了済みなら処理しない
        if (!IsRunning || hasFinished)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        // 時間切れ
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            IsRunning = false;
            hasFinished = true;

            // 登録された処理に時間切れを通知
            OnTimeUp?.Invoke();
        }
    }

    // タイマーを初期化して開始
    public void StartTimer(float duration = DEFAULT_TIME)
    {
        remainingTime = Mathf.Max(0f, duration);
        hasFinished = false;
        IsRunning = remainingTime > 0f;

        // 0秒で開始した場合も時間切れを通知
        if (remainingTime <= 0f)
        {
            hasFinished = true;
            OnTimeUp?.Invoke();
        }
    }

    // タイマーを停止
    public void StopTimer()
    {
        IsRunning = false;
    }

    // タイマーを再開
    public void ResumeTimer()
    {
        if (!hasFinished && remainingTime > 0f)
        {
            IsRunning = true;
        }
    }
}