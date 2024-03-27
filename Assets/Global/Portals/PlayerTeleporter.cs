using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Portal_Pack
{
    public class PlayerTeleporter: MonoBehaviour
    {
        public PlayerTeleporter()
        {
            
        }
        
        public void TeleportInScene(GameObject obj, Vector2 destination)
        {
            obj.transform.position = destination;
        }

        public void TeleportAcrossScenes(GameObject player, string destinationSceneName, Vector2 position)
        {
            // Serialize object state here (position, rotation, etc.)
            SaveObjectState(player);

            // Load the destination scene asynchronously
            StartCoroutine(LoadSceneAsync(destinationSceneName, player, position));
        }
        
        private void SaveObjectState(GameObject obj)
        {
            // Implement object state serialization here
            // Store position, rotation, and any other necessary data
        }
        
        private void RestoreObjectState(GameObject obj)
        {
            // Implement object state restoration here
            // Restore position, rotation, and any other necessary data
        }
        
        private IEnumerator LoadSceneAsync(string sceneName, GameObject player, Vector2 position)
        { 
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
            player.transform.position = position;
            
            // restore the object data
            RestoreObjectState(player);
        }
    }
}