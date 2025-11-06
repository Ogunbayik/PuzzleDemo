using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Image hiddenImage;
    [SerializeField] private Image actualImage;
    [SerializeField] private Image tileBackgroundImage;

    private Color tileColor;
    private int tileHeight;
    private int tileWidth;

    private bool canSelect = true;
    public void SetupTile(int height, int width, Sprite selected, Sprite hiddenSprite)
    {
        //Board da tilelarý nötr olarak ayarlýyoruz
        tileHeight = height;
        tileWidth = width;
        actualImage.sprite = selected;
        hiddenImage.sprite = hiddenSprite;
    }
    public void SetTileColor(Color color)
    {
        //Board daki tilelarýn rengini ayarlýyoruz
        tileColor = color;
    }
    public void SetBackgroundColor(Color color)
    {
        tileBackgroundImage.color = color;
    }
    public void SetTileSprite(Sprite sprite)
    {
        actualImage.sprite = sprite;
    }
    public void SetCanSelect(bool isSelect)
    {
        canSelect = isSelect;
    }
    public bool CanSelect()
    {
        return canSelect;
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
