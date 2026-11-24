using UnityEngine;

public class TornadoController : MonoBehaviour
{
    [Header("竜巻の設定")]
    [SerializeField] private GameObject tornadoPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float windPower = 5.0f;

    private GameObject currentTornado;

    // 竜巻を発生させる
    public void StartTornado()
    {
        if (currentTornado != null) return;

        currentTornado = Instantiate(
            tornadoPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Debug.Log("StartTornadoが呼び出されました");
        gameObject.SetActive(true);
    }

    // 竜巻を消す
    public void EndTornado()
    {
        if (currentTornado == null) return;

        Destroy(currentTornado);
        currentTornado = null;
    }

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