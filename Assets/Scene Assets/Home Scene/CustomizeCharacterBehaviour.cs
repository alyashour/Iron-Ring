using System;
using Global;
using UnityEngine;

namespace Scene_Assets.Home_Scene
{
    public class CustomizeCharacterBehaviour : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; private set; } = false;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Player") CanInteract = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.name == "Player") CanInteract = false;
        }

        public void Interact()
        {
            
        }
    }
}
