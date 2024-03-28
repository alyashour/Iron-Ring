using UnityEngine;
using UnityEngine.UI;

namespace Scene_Assets
{
    public class ColorPreviewScript : MonoBehaviour
    {
        private float _red, _green, _blue;
        private Image _image;

        [SerializeField] private Slider redInput, greenInput, blueInput;

        public void Start()
        {
            _image = gameObject.GetComponent<Image>();
        }
    
        public void UpdateColor()
        {
            // get value from inputs
            _red = redInput.value;
            _green = greenInput.value;
            _blue = blueInput.value;
        
            var color = new Color(_red, _green, _blue);
            _image.color = color;
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}
