using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Portals
{
    /**
    * Portals
    * Author: Aly
    */

    public class PortalScript : MonoBehaviour
    {
        [SerializeField] private string destinationSceneName;
        [SerializeField] private string destinationPortalName;
        [SerializeField] private float teleportDelayTime = 2f;
        [SerializeField] private bool teleportAcrossScenes = true;
        private bool _canTeleport = true;
        private float _timeOfPortalEnter;

        private TeleportManager _teleportManager;

        private void Start()
        {
            _teleportManager = TeleportManager.Instance;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var timeElapsed = Time.time - _timeOfPortalEnter;
        
            if (_canTeleport && timeElapsed > teleportDelayTime && other.name == "Player")
            {
                Teleport(other);
            }
        }

        private void Teleport(Collider2D other)
        {
            // if we have to teleport across scenes
            if (teleportAcrossScenes)
            {
                // let the teleport manager handle it
                _teleportManager.TeleportAcrossScenes(
                    other.gameObject, destinationSceneName, destinationPortalName
                );
            }
            
            // if we can teleport in the same scene
            else
            {
                // find the appropriate location
                Vector2 destinationPosition = GameObject.Find(destinationPortalName).transform.position;
                
                // then ask the teleport manager to teleport the player
                _teleportManager.TeleportInScene(other.gameObject, destinationPosition);
            }
            
            // call a delayed async func to prevent immediate re-teleportation
            Invoke(nameof(ResetTeleportFlag), 1.0f); // Delay to prevent immediate re-teleportation
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_canTeleport && other.name == "Player")
            {
                _timeOfPortalEnter = Time.time;
            }
        }
        
        private void ResetTeleportFlag()
        {
            _canTeleport = true;
        }
    }
}