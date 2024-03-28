using System;
using Scene_Assets;
using Scene_Assets.Home_Scene;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectorScript : MonoBehaviour
{
    [SerializeField] private ColorPreviewManager colorPreview;
    private Color _color;

    private void Start()
    {
        _color = GetComponent<Image>().color;
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        colorPreview.SetColor(_color);
    }
}
