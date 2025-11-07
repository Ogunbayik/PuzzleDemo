using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image hiddenImage;
    [SerializeField] private Image actualImage;
    [SerializeField] private Image tileBackgroundImage;

    private Color tileColor;
    private int tileHeight;
    private int tileWidth;
    public void SetupTile(int height, int width, Sprite selected, Sprite hiddenSprite)
    {
        //Board da tilelarý nötr olarak ayarlýyoruz
        tileHeight = height;
        tileWidth = width;
        actualImage.sprite = selected;
        hiddenImage.sprite = hiddenSprite;
    }
    public void SetBackgroundColor(Color color)
    {
        tileBackgroundImage.color = color;
    }
    public void SetTileSprite(Sprite sprite)
    {
        actualImage.sprite = sprite;
    }
    public Sprite GetActualSprite()
    {
        return actualImage.sprite;
    }
    public void OnMouseDown()
    {
        BoardManager.Instance.SelectTile(this);
    }
}
