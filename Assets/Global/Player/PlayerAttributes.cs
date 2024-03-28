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
    public static int BossesDefeated { get; set; }
    public static string CurrentScene { get; set; }

    public static bool Alive {  get; set; }


    // Assigns the initial values for the player attributes - only call at the very start of the game
    public static void InitializeAttributes()
    {
        PlayerHealth = 100;
        PlayerSpeed = 1.75f;
        PlayerDefence = 0;
        PlayerDamage = 10;

        PlayerKnockback = 20;

        CurrentScene = "Village";

        PlayerLevel = 0;
        PlayerXP = 0;

        Alive = true;
    }
}