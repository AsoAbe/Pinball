using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    // Œ‚”j‚É‰ÁZ‚·‚éƒXƒRƒA
    [SerializeField] private int scoreValue = 100;

    // Œ‚”jÏ‚İ‚©‚Ç‚¤‚©
    private bool isDefeated = false;

    private EnemySpawner enemySpawner;

    // Spawner‚ğ“o˜^‚·‚é
    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }
    private void Update()
    {
        // ƒfƒoƒbƒO—pFQƒL[‚Å“G‚ğ“|‚·
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DefeatEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ƒ{[ƒ‹ˆÈŠO‚Íˆ—‚µ‚È‚¢
        if (!other.CompareTag("Ball"))
        {
            return;
        }

        DefeatEnemy();
    }

    // “G‚Ì€–Sˆ—
    private void DefeatEnemy()
    {
        // Œ‚”jÏ‚İ‚È‚çˆ—‚µ‚È‚¢
        if (isDefeated)
        {
            return;
        }

        isDefeated = true;

        // Spawner‚É“G‚ª“|‚³‚ê‚½‚±‚Æ‚ğ’Ê’m
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated();
        }

        // ƒXƒRƒA‚ğ‰ÁZ‚·‚é
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddComboScore(scoreValue);
        }

        // “G‚ğíœ‚·‚é
        Destroy(gameObject);
    }
}