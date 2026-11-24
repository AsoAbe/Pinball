using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TornadoEffect : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private ParticleSystem windEffect;
    [SerializeField] private MeshFilter tornadoMesh;

    [Header("粒子の発生")]
    [SerializeField] private float emissionRate = 30.0f;
    [SerializeField] private float particleLifetime = 3.0f;
    [SerializeField] private float particleSize = 0.08f;

    [Header("移動")]
    [SerializeField] private float rotationSpeed = 360.0f;
    [SerializeField] private float riseSpeed = 2.0f;
    [SerializeField] private float extraRadius = 0.1f;

    [Header("形状取得")]
    [SerializeField] private int heightSampleCount = 100;

    private ParticleSystem.Particle[] particles;
    private float[] radiusByHeight;

    private float emissionTimer;

    private float tornadoHeight;
    private float bottomY;

    private void Awake()
    {
        if (windEffect == null)
        {
            windEffect = GetComponent<ParticleSystem>();
        }

        // 粒子のシミュレーションをローカル座標にする
        var main = windEffect.main;
        main.simulationSpace = ParticleSystemSimulationSpace.Local;
        main.maxParticles = 1000;

        // 発生位置と発生タイミングをスクリプトで制御
        var emission = windEffect.emission;
        emission.enabled = false;

        var shape = windEffect.shape;
        shape.enabled = false;

        particles = new ParticleSystem.Particle[main.maxParticles];

        CreateRadiusData();
    }

    private void Update()
    {
        if (tornadoMesh == null || radiusByHeight == null)
        {
            return;
        }

        EmitParticles();
        MoveParticles();
    }

    private void CreateRadiusData()
    {
        Mesh mesh = tornadoMesh.sharedMesh;

        if (mesh == null)
        {
            Debug.LogError("TornadoのMeshが設定されていません。");
            return;
        }

        Vector3[] vertices = mesh.vertices;

        // FBXの頂点をWindEffectのローカル座標に変換
        Vector3 first = transform.InverseTransformPoint(
            tornadoMesh.transform.TransformPoint(vertices[0])
        );

        bottomY = first.y;
        float topY = first.y;

        foreach (Vector3 vertex in vertices)
        {
            Vector3 worldPosition =
                tornadoMesh.transform.TransformPoint(vertex);

            Vector3 localPosition =
                transform.InverseTransformPoint(worldPosition);

            bottomY = Mathf.Min(bottomY, localPosition.y);
            topY = Mathf.Max(topY, localPosition.y);
        }

        tornadoHeight = topY - bottomY;

        if (tornadoHeight <= 0.001f)
        {
            Debug.LogError("Tornadoの高さを取得できません。");
            return;
        }

        radiusByHeight = new float[heightSampleCount];

        // 高さごとの最大半径を取得
        foreach (Vector3 vertex in vertices)
        {
            Vector3 worldPosition =
                tornadoMesh.transform.TransformPoint(vertex);

            Vector3 localPosition =
                transform.InverseTransformPoint(worldPosition);

            float t = Mathf.InverseLerp(
                bottomY,
                topY,
                localPosition.y
            );

            int index = Mathf.Clamp(
                Mathf.RoundToInt(t * (heightSampleCount - 1)),
                0,
                heightSampleCount - 1
            );

            float radius = new Vector2(
                localPosition.x,
                localPosition.z
            ).magnitude;

            radiusByHeight[index] =
                Mathf.Max(radiusByHeight[index], radius);
        }

        // 頂点が少ない高さの半径を補間
        for (int i = 0; i < heightSampleCount; i++)
        {
            if (radiusByHeight[i] > 0.001f)
            {
                continue;
            }

            int nearest = -1;

            for (int j = 1; j < heightSampleCount; j++)
            {
                int lower = i - j;
                int upper = i + j;

                if (lower >= 0 &&
                    radiusByHeight[lower] > 0.001f)
                {
                    nearest = lower;
                    break;
                }

                if (upper < heightSampleCount &&
                    radiusByHeight[upper] > 0.001f)
                {
                    nearest = upper;
                    break;
                }
            }

            if (nearest >= 0)
            {
                radiusByHeight[i] = radiusByHeight[nearest];
            }
        }
    }

    private void EmitParticles()
    {
        emissionTimer += emissionRate * Time.deltaTime;

        int emitCount = Mathf.FloorToInt(emissionTimer);
        emissionTimer -= emitCount;

        for (int i = 0; i < emitCount; i++)
        {
            ParticleSystem.EmitParams emitParams =
                new ParticleSystem.EmitParams();

            emitParams.position = Vector3.zero;
            emitParams.startLifetime = particleLifetime;
            emitParams.startSize = particleSize;
            emitParams.startColor = Color.white;
            emitParams.randomSeed =
                (uint)Random.Range(1, int.MaxValue);

            windEffect.Emit(emitParams, 1);
        }
    }

    private void MoveParticles()
    {
        int count = windEffect.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            float progress = 1.0f -
                particles[i].remainingLifetime /
                particles[i].startLifetime;

            // 上昇位置
            float height = Mathf.Clamp01(
            progress * riseSpeed
            ) * tornadoHeight;

            // 高さに対応する半径を取得
            float radius = GetRadius(progress) + extraRadius;

            // 粒子ごとに異なる回転開始角度
            float startAngle =
                (particles[i].randomSeed % 36000) / 100.0f;

            float angle =
                startAngle +
                rotationSpeed * progress * particleLifetime;

            float radian = angle * Mathf.Deg2Rad;

            particles[i].position = new Vector3(
                Mathf.Cos(radian) * radius,
                bottomY + height,
                Mathf.Sin(radian) * radius
            );
        }

        windEffect.SetParticles(particles, count);
    }

    private float GetRadius(float progress)
    {
        float index = progress * (heightSampleCount - 1);

        int lower = Mathf.FloorToInt(index);
        int upper = Mathf.Min(lower + 1, heightSampleCount - 1);

        return Mathf.Lerp(
            radiusByHeight[lower],
            radiusByHeight[upper],
            index - lower
        );
    }
}