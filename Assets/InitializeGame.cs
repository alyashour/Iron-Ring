using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Initializes the game - sets up the save file, and sets the initial player attributes
//      - will be called when the game is loaded for the first time
// Author: Aiden

public class InitializeGame : MonoBehaviour
{

    // The data to be saved

    // The current state of the game - essentially the # of bosses defeated
    private int gameState;

    // The current scene the player is in, at their save point
    private string scene;

    // The players level and xp
    private int level;
    private int xp;

    // Other attributes of the player
    private float playerHealth;
    private float playerSpeed;
    private float playerDefence;
    private float playerDamage;

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Save();
            print("testing save!");
        }
    }

    private class SaveObject
    {
        public int gameState;
        public string scene;
        public int level;
        public int xp;
        public float playerHealth;
        public float playerSpeed;
        public float playerDefence;
        public float playerDamage;
    }

    public void Save()
    {

        gameState = PlayerAttributes.BossesDefeated;
        scene = PlayerAttributes.CurrentScene;
        level = PlayerAttributes.PlayerLevel;
        xp = PlayerAttributes.PlayerXP;
        playerHealth = PlayerAttributes.PlayerHealth;
        playerSpeed = PlayerAttributes.PlayerSpeed;
        playerDefence = PlayerAttributes.PlayerDefence;
        playerDamage = PlayerAttributes.PlayerDamage;


        SaveObject saveObject = new SaveObject
        {
            gameState = gameState,
            scene = scene,
            level = level,
            xp = xp,
            playerHealth = playerHealth,
            playerSpeed = playerSpeed,
            playerDefence = playerDefence,
            playerDamage = playerDamage
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/save.txt", json);


    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {

            print("loading from file...");

            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            // Assigns the attributes
            PlayerAttributes.BossesDefeated = saveObject.gameState;
            PlayerAttributes.CurrentScene = saveObject.scene;
            PlayerAttributes.PlayerLevel = saveObject.level;
            PlayerAttributes.PlayerXP = saveObject.xp;
            PlayerAttributes.PlayerHealth = saveObject.playerHealth;
            PlayerAttributes.PlayerSpeed = saveObject.playerSpeed;
            PlayerAttributes.PlayerDefence = saveObject.playerDefence;
            PlayerAttributes.PlayerDamage = saveObject.playerDamage;

        } else
        {
            print("loading from defaults");
            PlayerAttributes.InitializeAttributes();
        }


    }

}