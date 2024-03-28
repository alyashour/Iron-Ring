using System;
using Global.Portals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Global
{
    public class DoorBehaviour : MonoBehaviour
    {
        [SerializeField] private string sceneName; // the scene the door loads
        [SerializeField] private Vector2 position; // the position in the new scene to load the player into
        private TeleportManager _playerTeleporter;

        private void Start()
        {
            _playerTeleporter = TeleportManager.Instance;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject;
            if (other.gameObject.name == "Player")
            {
                _playerTeleporter.TeleportAcrossScenes(player, sceneName, position);
            }
        }
    }
}

