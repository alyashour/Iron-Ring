using System;
using Scene_Assets;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectorScript : MonoBehaviour
{
    [SerializeField] private ColorPreviewScript colorPreview;
    private Color _color;

    private void Start()
    {
        _color = GetComponent<Image>().color;
    }

    public void OnClick()
    {
        Debug.Log($"Setting the players value to {_color}");
        colorPreview.SetColor(_color);
    }
}
