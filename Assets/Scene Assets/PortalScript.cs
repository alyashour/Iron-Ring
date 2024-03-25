using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private string destinationSceneName;
    [SerializeField] private Vector2 spawnPoint = Vector2.zero; // the location that the player will spawn in in the new scene
    private bool _canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canTeleport && collision.name == "Player") // Adjust the tag based on what you want to teleport
        {
            _canTeleport = false;
            Teleport(collision.gameObject);
            Invoke("ResetTeleportFlag", 0.5f); // Delay to prevent immediate re-teleportation
        }
    }

    private void Teleport(GameObject obj)
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