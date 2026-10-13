using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // どこからでもアクセスできるインスタンス
    public static SceneChanger Instance;

    // シーン名のデフォルト設定
    public const string TITLE_SCENE = "TitleScene";
    public const string RESULT_SCENE = "ResultScene";
    public const string GAME_OVER_SCENE = "GameOverScene";

    // 通常のゲーム速度
    public const float NORMAL_TIME_SCALE = 1f;

    private void Awake()
    {
        // すでに存在する場合は重複を防ぐ
        if (Instance == null)
        {
            Instance = this;

            // シーンを切り替えても維持する
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 指定したシーンへ移動する共通処理
    public void LoadScene(string sceneName)
    {
        // 一時停止状態を解除
        Time.timeScale = NORMAL_TIME_SCALE;

        // シーンを切り替える
        SceneManager.LoadScene(sceneName);
    }

    // タイトルへ戻る
    public void GoToTitleScene()
    {
        // ゲーム状態を初期化
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }

        LoadScene(TITLE_SCENE);
    }

    // リザルト画面へ移動
    public void GoToResultScene()
    {
        LoadScene(RESULT_SCENE);
    }

    // ゲームクリア画面へ移動
    public void GoToClearScene()
    {
        LoadScene(RESULT_SCENE);
    }

    // ゲームオーバー画面へ移動
    public void GoToGameOverScene()
    {
        LoadScene(GAME_OVER_SCENE);
    }
}