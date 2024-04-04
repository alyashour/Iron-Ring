using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInit : MonoBehaviour
{
    [SerializeField] Button loadbtn;
    private void Start()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            loadbtn.interactable = true;
        } else
        {
            loadbtn.interactable = false;
        }
    }
}