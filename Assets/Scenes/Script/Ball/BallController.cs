using UnityEngine;

public class BallController : MonoBehaviour
{
    // スコアを獲得する
    public void AddScore(int value)
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddComboScore(value);
        }
    }

    /// <summary>
    /// ボールが失われたときに呼び出す。
    /// </summary>
    public void OnBallLost()
    {
        Destroy(gameObject);
    }
}