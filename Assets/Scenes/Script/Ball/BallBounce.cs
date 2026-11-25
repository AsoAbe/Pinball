
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    [Header("反発設定")]
    public float bouncePower = 12f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突面の法線方向を取得
        Vector3 normal = collision.contacts[0].normal;

        // 衝突前の速度から反射方向を計算
        Vector3 direction = Vector3.Reflect(
            rb.linearVelocity,
            normal
        );

        // 反発後の速度を設定
        rb.linearVelocity = direction.normalized * bouncePower;
    }
}

