using UnityEngine;

public class BallController : MonoBehaviour
{
    /// <summary>
    /// ボールが失われたときに呼び出す。
    /// </summary>
    public void OnBallLost()
    {
        Destroy(gameObject);
    }
}