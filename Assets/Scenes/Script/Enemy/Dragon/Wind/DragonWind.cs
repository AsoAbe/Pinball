using UnityEngine;

public class DragonWind : MonoBehaviour
{
    [Header("ó≥ä™ÇÃê›íË")]
    [SerializeField] private GameObject tornadoPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject currentTornado;

    public void StartWind()
    {
        if (currentTornado != null) return;

        currentTornado = Instantiate(
            tornadoPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }

    public void EndWind()
    {
        if (currentTornado == null) return;

        Destroy(currentTornado);
        currentTornado = null;
    }
}