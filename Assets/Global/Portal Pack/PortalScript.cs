using System.Collections;
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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        float timeElapsed = Time.time - _timeOfPortalEnter;
        if (_canTeleport && timeElapsed > teleportDelayTime && other.name == "Player")
        {
            _canTeleport = false;
            
            // teleport the player
            if (teleportAcrossScenes) TeleportAcrossScenes(other.gameObject);
            else TeleportInScene(other.gameObject);
            
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

    private void TeleportInScene(GameObject obj)
    {
        var otherPortal = GameObject.Find(destinationPortalName);
        obj.transform.position = otherPortal.transform.position;
    }

    private void TeleportAcrossScenes(GameObject obj)
    {
        // Serialize object state here (position, rotation, etc.)
        SaveObjectState(obj);

        // Load the destination scene asynchronously
        StartCoroutine(LoadSceneAsync(destinationSceneName, obj));
    }

    private void SaveObjectState(GameObject obj)
    {
        // Implement object state serialization here
        // Store position, rotation, and any other necessary data
    }

    private IEnumerator LoadSceneAsync(string sceneName, GameObject obj)
    {
        Scene oldScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Activate the newly loaded scene
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(loadedScene);
        
        // move the player there
        GameObject otherPortal = GameObject.Find(destinationPortalName);
        obj.transform.position = otherPortal.transform.position;
    }

    private void RestoreObjectState(GameObject obj)
    {
        // Implement object state restoration here
        // Restore position, rotation, and any other necessary data
    }

    private void ResetTeleportFlag()
    {
        _canTeleport = true;
    }
}