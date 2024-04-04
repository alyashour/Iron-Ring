using Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// I am losing sanity

public class PortalDoor : MonoBehaviour
{
    [SerializeField] private string sceneName; // the scene the door loads
    [SerializeField] private Vector2 position; // the position in the new scene to load the player into
    private TeleportManager _playerTeleporter;

    GameObject player;

    private void Start()
    {
        _playerTeleporter = TeleportManager.Instance;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        updateGameState();
        player = other.gameObject;
        if (other.gameObject.name == "Player")
        {
            Invoke(nameof(teleport), 1f);
        }
    }

    private void updateGameState()
    {
        if (PlayerAttributes.GlobalGameState == 0 && SceneManager.GetActiveScene().name == "Home")
        {
            PlayerAttributes.GlobalGameState = 1;
            PlayerAttributes.PlayerSpeed = 1.75f;
        }
    }
    private void teleport()
    {
        _playerTeleporter.TeleportAcrossScenes(player, sceneName, position);
    }
}
