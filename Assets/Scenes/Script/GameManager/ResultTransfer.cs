using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultTransfer : MonoBehaviour
{
    // ゲームオーバー時のシーン名
    public string ResultScene = "ResultScene";

    // ゲームオーバー処理
    public void OnResult()
    {
        Debug.Log("リザルト処理開始");

        SceneManager.LoadScene(ResultScene);
    }
}
