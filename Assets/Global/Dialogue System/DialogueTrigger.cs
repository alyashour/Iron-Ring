using System.Collections;
using System.Collections.Generic;
using Global.Dialogue_System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue();
    }
}
