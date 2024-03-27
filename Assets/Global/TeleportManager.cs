using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class TeleportManager : MonoBehaviour
    {
        private class PlayerState
        {
            // add player state information stuff in here
        }
        
        private static TeleportManager _instance; // Singleton instance
        public static TeleportManager Instance
        {
            get
            {
                // if there is no previous instance
                if (_instance == null)
                {
                    // try to find one in the scene
                    _instance = FindObjectOfType<TeleportManager>();
                    
                    // if there isn't one in the scene, create a new one
                    if (_instance == null) CreateTeleportManager();
                }
                return _instance;
            }
        }

        private static void CreateTeleportManager()
        {
            GameObject gameObject = new GameObject("TeleportManager");
            DontDestroyOnLoad(gameObject);
            
            _instance = gameObject.AddComponent<TeleportManager>();
        }
        
        public void TeleportInScene(GameObject obj, Vector2 destination)
        {
            obj.transform.position = destination;
        }

        public void TeleportAcrossScenes(GameObject player, string destinationSceneName, Vector2 position)
        {
            PlayerState playerState = GetObjectState(player);

            // Load the destination scene asynchronously
            StartCoroutine(LoadSceneAsync(destinationSceneName, position, playerState));
        }

        public void TeleportAcrossScenes(GameObject player, string destinationSceneName, string destinationPortalName)
        {
            // teleport to 0, 0 first
            TeleportAcrossScenes(player, destinationSceneName, new Vector2(0, 0));
            
            // find the destination portal
            GameObject portal = GameObject.Find(destinationPortalName);

            // if we couldn't find the portal
            if (portal != null)
            {
                Vector2 destinationPosition = portal.transform.position;
                TeleportInScene(player, destinationPosition);
            }
            else
            {
                Debug.LogError("Couldn't find destination portal");
            }
        }
        
        private PlayerState GetObjectState(GameObject obj)
        {
            PlayerState state = new PlayerState();
            
            // save the obj data to state here

            return state;
        }
        
        private void RestoreObjectState(GameObject playerObj, PlayerState state)
        {
            // Restore the playerObj from state
        }

        private IEnumerator LoadSceneAsync(string sceneName, Vector2 position, PlayerState playerState)
        {
            // get a ref to the old scene
            Scene oldScene = SceneManager.GetActiveScene();
            
            // load the new scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // wait until its done
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // set scene as active
            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);
            
            // get ref to player
            GameObject player = GameObject.Find("Player");

            if (player == null) 
                Debug.Log("Couldn't find player in new scene!");
            
            // teleport player to position
            player.transform.position = position;
            // update player info
            RestoreObjectState(player, playerState);
        }
    }
}
