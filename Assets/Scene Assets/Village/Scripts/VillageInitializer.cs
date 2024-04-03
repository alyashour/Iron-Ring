using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // trigger start scene dialogue if it hasn't been shown yet
        /*if (!PlayerAttributes.StartScene)
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            PlayerAttributes.StartScene = true;
        }*/

        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
