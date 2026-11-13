using UnityEngine;

public class DragonWind : MonoBehaviour
{
    [Header("—³Šª‚Ì§Œä")]
    [SerializeField] private TornadoController tornadoController;

    // Animation Event‚©‚çŒÄ‚Ño‚·
    public void StartWind()
    {
        tornadoController.StartTornado();
    }

    // Animation Event‚©‚çŒÄ‚Ño‚·
    public void EndWind()
    {
        tornadoController.EndTornado();
    }
}