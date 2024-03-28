using System;
using Global;
using UnityEngine;

namespace Scene_Assets.Home_Scene
{
    public class CupboardBehaviour : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; private set; }
        [SerializeField] private ColorPreviewManager preview;
        
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
            // open customization menu
            preview.OpenMenu();
        }
    }
}
