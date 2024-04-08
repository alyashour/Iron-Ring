using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // health
    public Image healthBar;
    public float healthAmount = PlayerAttributes.PlayerLevel;
    //[SerializeField] GameObject player;

    // xp
    //[SerializeField] TMP_Text level;
    //public Image xpBar;
    //public float xpAmount = PlayerAttributes.PlayerXP;

    [SerializeField] TextMeshProUGUI XPNumText;

    // Update is called once per frame
    void Update()
    {
        // update health bar
        healthAmount = PlayerAttributes.PlayerHealth;
        healthBar.fillAmount = healthAmount / 100f;

        // update level
        //level.text = PlayerAttributes.PlayerLevel.ToString();

        // update xp bar
        //xpAmount = PlayerAttributes.PlayerXP;
        //xpBar.fillAmount = xpAmount / 100f;

        XPNumText.text = ""+PlayerAttributes.PlayerXP;
    }
}