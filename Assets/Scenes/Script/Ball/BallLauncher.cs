using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private GameObject ballPrefab;
    private Transform launchPoint;
    private float launchForce = 10f;

    /// <summary>
    /// ボールを生成して自動発射する。
    /// </summary>
    public void LaunchBall()
    {
        if (ballPrefab == null || launchPoint == null)
        {
            Debug.LogWarning("ボールPrefabまたは発射位置が設定されていません。");
            return;
        }

        GameObject ball = Instantiate(
            ballPrefab,
            launchPoint.position,
            launchPoint.rotation
        );

        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("ボールPrefabにRigidbodyがありません。");
            Destroy(ball);
            return;
        }

        rb.AddForce(
            launchPoint.forward * launchForce,
            ForceMode.Impulse
        );
    }
}
