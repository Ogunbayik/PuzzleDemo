using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public Color[,] boardColors;

    [Header("Board Settings")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int tileScale;
    [SerializeField] private Sprite questionSprite;
    [SerializeField] private Image executeButton;
    [SerializeField] private Image cancelButton;
    [Header("Player Settings")]
    [SerializeField] private Sprite[] playerOneSprites;
    [SerializeField] private Color playerOneColor;
    [SerializeField] private Sprite[] playerTwoSprites;
    [SerializeField] private Color playerTwoColor;

    public List<Tile> playerOneTiles = new List<Tile>();
    public List<Tile> playerTwoTiles = new List<Tile>();

    private Tile selectedTile;

    private bool isPlayerOneTurn = true;
    private void Awake()
    {
        Instance = this;
        selectedTile = null;
    }
    private void Start()
    {
        SetupBoard();
        SetButtonsActivate(false);
    }
    private void SetupBoard()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var spawnPosition = Vector3.zero;
                spawnPosition.Set(i * tileScale, j * tileScale, 0f);

                var tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                tile.transform.position = spawnPosition;
                tile.transform.SetParent(this.transform);
                tile.name = $"[{i},{j}]";
                tile.GetComponent<Tile>().SetupTile(i, j, questionSprite);
            }
        }
    }

    public void SelectTile(Tile tile)
    {
        if (!tile.IsClickable())
            return;

        if(selectedTile == null)
        {
            //Select any tile on the board
            selectedTile = tile;
            selectedTile.SetBackgroundColor(isPlayerOneTurn  ? playerOneColor : playerTwoColor);
            SetButtonsActivate(true);
        }
        else if(selectedTile == tile)
        {
            //Deselect Tile and select new one
            Debug.Log("Same Tile");
            selectedTile.SetBackgroundColor(Color.white);
            selectedTile = null;
            SetButtonsActivate(false);
        }
        else
        {
            //Use different moves in the board like swaping
            Debug.Log("Use some different moves");
        }
    }

    private void SetButtonsActivate(bool isActive)
    {
        executeButton.gameObject.SetActive(isActive);
        cancelButton.gameObject.SetActive(isActive);
    }
    public void Execute()
    {
        if(isPlayerOneTurn)
        {
            var randomIndex = Random.Range(0, playerOneSprites.Length);
            var randomSprite  = playerOneSprites[randomIndex];
            selectedTile.SetTileSprite(randomSprite);
            playerOneTiles.Add(selectedTile);
        }
        else
        {
            var randomIndex = Random.Range(0, playerTwoSprites.Length);
            var randomSprite = playerTwoSprites[randomIndex];
            selectedTile.SetTileSprite(randomSprite);
            playerTwoTiles.Add(selectedTile);
        }

        SetButtonsActivate(false);
        selectedTile.SetClickable(false);
        selectedTile = null;
        ChangePlayerTurn();
    }
    private void ChangePlayerTurn()
    {
        isPlayerOneTurn = !isPlayerOneTurn;
    }
    public void Cancel()
    {
        selectedTile.SetBackgroundColor(Color.white);
        selectedTile = null;
        SetButtonsActivate(false);
    }
}
