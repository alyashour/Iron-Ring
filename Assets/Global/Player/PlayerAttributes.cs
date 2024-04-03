using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Entity class for the player attributes - ouda would be proud
// Author: Aiden

public class PlayerAttributes : MonoBehaviour
{
    // Gameplay related attributes
    public static float PlayerHealth { get; set; }
    public static float PlayerSpeed { get; set; }
    public static float PlayerDefence { get; set; }
    public static float PlayerDamage { get; set; }
    public static float PlayerKnockback { get; set; }

    // Level related attributes
    public static int PlayerLevel { get; set; }
    public static int PlayerXP { get; set; }


    // Game state related metrics
    public static int GameStateA { get; set; }
    public static string CurrentScene { get; set; }

    public static bool Alive {  get; set; }

    public static Color PlayerColor { get; set; }

    // related to dialogue triggers - Author: Dylen

    public static bool StartScene { get; set; }
    public static bool GolemComplete { get; set; }
    public static bool KongComplete { get; set; }
    public static bool HornCollected { get; set; }
    public static bool MagicianDisappeared { get; set; }
    public static bool MetAduoForp { get; set; }
    public static bool PongComplete { get; set; }



    // Assigns the initial values for the player attributes - only call at the very start of the game
    public static void InitializeAttributes()
    {
        PlayerHealth = 100;
        PlayerSpeed = 1.75f;
        PlayerDefence = 0;
        PlayerDamage = 10;

        PlayerKnockback = 100;

        CurrentScene = SceneManager.GetActiveScene().name;

        PlayerLevel = 0;
        PlayerXP = 0;

        Alive = true;

        PlayerColor = new Color(255, 255, 255, 1);

        StartScene = false;
        GolemComplete = false;
        KongComplete = false;
        HornCollected = false;
        MagicianDisappeared = false;
        MetAduoForp = false;
        PongComplete = false;
    }
}