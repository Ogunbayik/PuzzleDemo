using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField] private Image tileImage;
    [SerializeField] private Image tileBackgroundImage;

    private int tileHeight;
    private int tileWidth;

    private bool isClickable = true;
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    public void SetupTile(int height, int width, Sprite sprite)
    {
        tileHeight = height;
        tileWidth = width;
        tileImage.sprite = sprite;
    }
    public void SetTileType(int row, int col)
    {

    }
    public void SetBackgroundColor(Color color)
    {
        tileBackgroundImage.color = color;
    }
    public void SetTileSprite(Sprite sprite)
    {
        tileImage.sprite = sprite;
    }
    public void SetClickable(bool canClick)
    {
        isClickable = canClick;
    }
    public bool IsClickable()
    {
        return isClickable;
    }
    public void OnMouseDown()
    {
        BoardManager.Instance.SelectTile(this);
    }

    public int GetTileWidth() { return tileWidth; }
    public int GetTileHeight() { return tileHeight; }
}
