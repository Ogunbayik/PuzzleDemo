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
    public void SetupTile(int height, int width, Sprite tileSprite, Sprite hiddenSprite,Color color)
    {
        //Board da tilelarý nötr olarak ayarlýyoruz
        tileHeight = height;
        tileWidth = width;
        actualImage.sprite = tileSprite;
        hiddenImage.sprite = hiddenSprite;
        tileColor = color;
    }
    public void SetBackgroundColor(Color color)
    {
        tileBackgroundImage.color = color;
    }
    public Sprite GetActualSprite()
    {
        return actualImage.sprite;
    }
    public Color GetTileColor()
    {
        return tileColor;
    }
    public void OnMouseDown()
    {
        BoardManager.Instance.SelectTile(this);
    }
}
