using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    // タイマー本体
    [SerializeField]
    private GameTimer gameTimer;

    // 残り時間の表示
    [SerializeField]
    private Text timerText;

    private void Update()
    {
        // 必要な参照が設定されていない場合は処理しない
        if (gameTimer == null || timerText == null)
        {
            return;
        }

        // 残り時間を整数で表示
        timerText.text =
            Mathf.CeilToInt(gameTimer.remainingTime).ToString();
    }
}