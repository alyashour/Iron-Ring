using UnityEngine;
using UnityEngine.UI;

namespace Scene_Assets
{
    public class ColorPreviewScript : MonoBehaviour
    {
        [SerializeField] private Slider redInput, greenInput, blueInput;
        [SerializeField] private GameObject playerObj, menuCanvas;
        
        private float _red, _green, _blue;
        private Image _image;
        
        public void Start()
        {
            _image = gameObject.GetComponent<Image>();
        }
    
        /**
         * This runs if the player updates through the sliders
         */
        public void UpdateColor()
        {
            // get value from inputs
            _red = redInput.value;
            _green = greenInput.value;
            _blue = blueInput.value;
        
            var color = new Color(_red, _green, _blue);
            _image.color = color;
        }

        /**
         * This runs if the player uses one of the preset values
         */
        public void SetColor(Color color)
        {
            // update the fields
            _red = color.r;
            _green = color.g;
            _blue = color.b;
            
            // update the player color
            _image.color = color;
            
            // update the sliders
            redInput.value = color.r;
            greenInput.value = color.g;
            blueInput.value = color.b;
        }

        public void Submit()
        {
            // update the color
            var color = new Color(_red, _green, _blue);
            playerObj.GetComponent<SpriteRenderer>().color = color;

            CloseMenu();
        }

        public void Cancel()
        {
            CloseMenu();
        }

        private void CloseMenu()
        {
            menuCanvas.SetActive(false);
        }

        public void OpenMenu()
        {
            menuCanvas.SetActive(true);
        }
    }
}
