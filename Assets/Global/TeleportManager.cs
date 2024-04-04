using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    /**
     * Teleport manager class.
     * This handles all player teleportation across and within scenes.
     * DO NOT use this or instantiate this directly, instead it should be a reference in all objects that teleport the player.
     * I.e., doors, portals, etc.
     * Please see DoorBehaviour.cs or PortalScript.cs for an example of how to use.
     *
     * Author: Aly
     */
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

        /**
         * For teleporting when there is no player in the current scene or when the player is dead
         */
        public void TeleportAcrossScenes(string destinationSceneName, Vector2 position)
        {
            StartCoroutine(LoadSceneAsync(destinationSceneName, position, null));
        }

        /**
         * For teleporting to a specific position in a scene
         */
        public void TeleportAcrossScenes(GameObject player, string destinationSceneName, Vector2 position)
        {
            PlayerState playerState = GetObjectState(player);

            // Load the destination scene asynchronously
            StartCoroutine(LoadSceneAsync(destinationSceneName, position, playerState));
        }

        /**
         * For portal teleportation
         */
        public void TeleportAcrossScenes(GameObject player, string destinationSceneName, string destinationPortalName)
        {
            PlayerState playerState = GetObjectState(player);
            StartCoroutine(LoadSceneAsync(destinationSceneName, destinationPortalName, playerState));
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

        /**
         * Scene loader
         * PlayerState may be null. 
         */
        private IEnumerator LoadSceneAsync(string sceneName, Vector2 position, PlayerState playerState)
        {
            // load the new scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // ------- Saves the game before the scene is swapped ---------
            InitializeGame.Save(sceneName);

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
            if (playerState != null)
                RestoreObjectState(player, playerState);
        }

        private IEnumerator LoadSceneAsync(string sceneName, string portalName, PlayerState playerState)
        {
            // load the new scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // ------- Saves the game before the scene is swapped ---------
            InitializeGame.Save(sceneName);

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
            
            // get ref to portal
            GameObject portal = GameObject.Find(portalName);

            if (portal == null)
            {
                Debug.Log("Couldn't find portal on scene");
            }
            
            // teleport player to position
            player.transform.position = portal.transform.position;
            
            // update player info
            if (playerState != null)
                RestoreObjectState(player, playerState);
        }
    }
}
