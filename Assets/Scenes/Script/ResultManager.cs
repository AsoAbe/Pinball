using System.Collections;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    // スコア表示の更新間隔（秒）
    public const float DEFAULT_UPDATE_INTERVAL = 0.01f;

    // リザルト画面のスコア表示
    public TextMeshProUGUI resultText;

    // 最終スコア
    public int result;

    private void Start()
    {
        // GameManagerから最終スコアを取得
        if (GameManager.Instance != null)
        {
            result = GameManager.Instance.score;
        }
        else
        {
            result = 0;
            Debug.LogWarning("GameManagerが見つかりません。");
        }

        // スコア表示を開始
        StartCoroutine(ShowResult());
    }

    // スコアを段階的に表示する
    private IEnumerator ShowResult()
    {
        int count = 0;

        while (count < result)
        {
            // スコアを表示
            if (resultText != null)
            {
                resultText.text = "スコア : " + count;
            }

            count++;

            // 一時停止中でも演出を進める
            yield return new WaitForSecondsRealtime(
                DEFAULT_UPDATE_INTERVAL
            );
        }

        // 最後に正確なスコアを表示
        if (resultText != null)
        {
            resultText.text = "スコア : " + result;
        }
    }
}