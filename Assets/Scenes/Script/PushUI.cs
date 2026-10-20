using UnityEngine;
using UnityEngine.UI;

public class PushUI : MonoBehaviour
{
    // スコア表示
    public Text ScoreText;

    // 残機表示
    public Text RemainingLivesText;

    private void Update()
    {
        // GameManagerが存在しない場合は処理しない
        if (GameManager.Instance == null)
        {
            return;
        }
        
        // ScoreManagerが存在しない場合は処理しない
        if (ScoreManager.Instance == null)
        {
            return;
        }

        // スコアを表示
        if (ScoreText != null)
        {
            ScoreText.text =
                ScoreManager.Instance.Score.ToString();
        }

        // 残機を表示
        if (RemainingLivesText != null)
        {
            RemainingLivesText.text =
                GameManager.Instance.remainingLives.ToString();
        }
    }
}