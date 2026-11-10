using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GameExit()
    {
        Debug.Log("ƒQ[ƒ€I—¹");

        Application.Quit();
    }
}

