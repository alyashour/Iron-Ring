using Global;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector2 position;
    private TeleportManager _teleportManager;

    private void Start()
    {
        _teleportManager = TeleportManager.Instance;
    }

    public void OnClick()
    {
        _teleportManager.TeleportAcrossScenes(sceneName, position);
    }
}