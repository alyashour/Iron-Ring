using Global;
using System.IO;
using Global.Teleportation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector2 position;
    private TeleportManager _teleportManager;

    private void Start()
    {
        _teleportManager = TeleportManager.Instance;
    }

    public void OnClick()
    {
        _teleportManager.TeleportAcrossScenes(sceneName, position);
    }

    // Loads a new game and deletes the save file
    public void NewGame()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            File.Delete(Application.dataPath + "/save.txt");
        }
        SceneManager.LoadScene("Home");
    }

    // Loads the save file
    public void LoadGame()
    {
        InitializeGame.Load();
        PlayerAttributes.PlayerHealth = 100;
        SceneManager.LoadScene(PlayerAttributes.CurrentScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}