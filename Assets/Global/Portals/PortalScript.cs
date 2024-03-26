using System;
using System.Collections;
using Global.Portal_Pack;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private PlayerTeleporter _playerTeleporter;

    private void Start()
    {
        _playerTeleporter = gameObject.AddComponent<PlayerTeleporter>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var timeElapsed = Time.time - _timeOfPortalEnter;
        
        if (_canTeleport && timeElapsed > teleportDelayTime && other.name == "Player")
        {
            GameObject destinationPortal = GameObject.Find(destinationPortalName); // get the dest. portal
            var destinationPosition = destinationPortal.transform.position;
            _canTeleport = false; // reset the flag
            
            // teleport the player
            if (teleportAcrossScenes)
            {
                _playerTeleporter.TeleportAcrossScenes(
                    other.gameObject, destinationSceneName, destinationPosition
                );
            }
            else _playerTeleporter.TeleportInScene(other.gameObject, destinationPosition);
            
            // call a delayed async func to prevent immediate re-teleportation
            Invoke(nameof(ResetTeleportFlag), 1.0f); // Delay to prevent immediate re-teleportation
        }
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