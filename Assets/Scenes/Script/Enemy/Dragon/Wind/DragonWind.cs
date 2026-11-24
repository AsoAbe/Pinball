using UnityEngine;

public class DragonWind : MonoBehaviour
{
    [Header("—³Šª‚Ì§Œä")]
    [SerializeField] private TornadoController tornadoController;

    // Animation Event‚©‚çŒÄ‚Ño‚·
    public void StartWind()
    {
        Debug.Log("StartWind‚ªŒÄ‚Ño‚³‚ê‚Ü‚µ‚½");

        if (tornadoController == null)
        {
            return;
        }

        tornadoController.StartTornado();
    }

    // Animation Event‚©‚çŒÄ‚Ño‚·
    public void EndWind()
    {
        if (tornadoController == null)
        {
            return;
        }

        tornadoController.EndTornado();
    }
}