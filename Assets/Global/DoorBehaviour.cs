using Global.Portal_Pack;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private string sceneName; // the scene the door loads
    [SerializeField] private Vector2 position; // the position in the new scene to load the player into
    private PlayerTeleporter _playerTeleporter;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTeleporter = gameObject.AddComponent<PlayerTeleporter>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject;
        if (other.gameObject.name == "Player")
        {
            _playerTeleporter.TeleportAcrossScenes(player, sceneName, position);
        }
    }
}

