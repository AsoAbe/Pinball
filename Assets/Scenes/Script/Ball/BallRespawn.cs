
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    [Header("落下判定")]
    public float fallHeight = -5f;

    private Rigidbody rb;

    // 最初の位置と回転を保存
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 初期位置を保存
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // 一定の高さより下に落ちたら
        if (rb.position.y < fallHeight)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // ボールの位置と回転を戻す
        rb.position = startPosition;
        rb.rotation = startRotation;

        // 移動速度と回転速度をリセット
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 物理演算を再開できる状態にする
        rb.WakeUp();
    }
}
