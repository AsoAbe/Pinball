using UnityEngine;

public class TornadoController : MonoBehaviour
{
    [Header("竜巻の設定")]
 
    [SerializeField] private float windPower = 5.0f;

    private GameObject currentTornado;

    private void OnTriggerStay(Collider other)
    {
        // Ball以外は対象にしない
        if (!other.CompareTag("Ball"))
        {
            return;
        }

        Rigidbody ball = other.GetComponent<Rigidbody>();

        if (ball == null)
        {
            return;
        }

        // 竜巻の中心から外側への方向
        Vector3 direction = ball.position - transform.position;

        direction.y = 0.0f;

        if (direction.sqrMagnitude < 0.001f)
        {
            return;
        }

        direction.Normalize();

        // 風の力を継続的に加える
        ball.AddForce(
            direction * windPower,
            ForceMode.Acceleration
        );
    }
}