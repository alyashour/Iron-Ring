using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Global.Dialogue_System
{
    public class DialogueManager : MonoBehaviour
    {
        public TMP_Text nameText;
        public TMP_Text dialogueText;

        public Animator animator;

        private Queue<string> names;
        private Queue<string> sentences;

        public bool isOpen;

        [SerializeField] private Dialogue dialogue;

        [SerializeField] private UnityEvent OnEnd;

        // Start is called before the first frame update
        void Start()
        {
            names = new Queue<string>();
            sentences = new Queue<string>();

            // Check if an EventSystem already exists in the scene
            EventSystem existingEventSystem = FindObjectOfType<EventSystem>();

            // If no EventSystem exists, create one
            if (existingEventSystem == null)
            {
                GameObject eventSystemObject = new GameObject("EventSystem");
                eventSystemObject.AddComponent<EventSystem>();
                eventSystemObject.AddComponent<StandaloneInputModule>();
            }
        }

        public void StartDialogue()
        {
            isOpen = true;
            animator.SetBool("IsOpen", isOpen);

            names.Clear();
            sentences.Clear();

            foreach(string name in dialogue.names)
            {
                names.Enqueue(name);
            }

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0) {
                EndDialogue();
                return;
            }
         
            string name = names.Dequeue();
            nameText.text = name;

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence (string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            isOpen = false;
            OnEnd.Invoke();
            animator.SetBool("IsOpen", isOpen);
        }
    }
}
