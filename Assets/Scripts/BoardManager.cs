using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [Header("Game Settings")]
    [SerializeField] private int playerCount;
    [SerializeField] private int spriteCount;
    [SerializeField] private int bombCount;
    [Header("Board Settings")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int tileScale;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Sprite questionSprite;
    [SerializeField] private Sprite bombSprite;
    [Header("Color Settings")]
    [SerializeField] private Color checkColor;
    [SerializeField] private Color playerOneColor;
    [SerializeField] private Color playerTwoColor;
    [Header("Particle Settings")]
    [SerializeField] private GameObject explosionParticle;

    public List<Sprite> allGreenTypeSprites = new List<Sprite>();
    public List<Sprite> allRedTypeSprites = new List<Sprite>();
    public List<Sprite> availableSprites = new List<Sprite>();

    private Tile selectedTile;
    private Tile checkTile;

    private int playerSpriteCount;
    private int selectCount;
    private int maxSelectCount = 2;

    private bool isPlayerOneTurn = true;
    private void Awake()
    {
        Instance = this;
        selectedTile = null;
    }
    private void Start()
    {
        SetRandomAvailableList();
        SetupBoard();
    }
    private void SetRandomAvailableList()
    {
        //Board 25
        var boardTileCount = height * width;
        //Remain 24
        var remainTileCount = boardTileCount - bombCount;
        var copyGreenList = allGreenTypeSprites;
        var copyRedList = allRedTypeSprites;
        //PlayerSpriteCount = 12
        playerSpriteCount = remainTileCount / playerCount;
        for (int i = 0; i < spriteCount; i++)
        {
            var randomGreenIndex = Random.Range(0, allGreenTypeSprites.Count);
            var randomRedIndex = Random.Range(0, allRedTypeSprites.Count);

            //EachCount = 6
            var eachCount = playerSpriteCount / spriteCount;
            for (int j = 0; j < eachCount; j++)
            {
                availableSprites.Add(copyGreenList[randomGreenIndex]);
                availableSprites.Add(copyRedList[randomRedIndex]);
            }

            //Oyunu reset attýðýmýz zaman tüm spritelar silinmesin diye copy olarak aldýk.
            copyGreenList.RemoveAt(randomGreenIndex);
            copyRedList.RemoveAt(randomRedIndex);
        }

        availableSprites.Add(bombSprite);
    }
    private Sprite GetRandomAvailableSprite()
    {
        var randomIndex = Random.Range(0, availableSprites.Count);
        var randomSprite = availableSprites[randomIndex];
        availableSprites.RemoveAt(randomIndex);
        return randomSprite;
    }
    private void SetupBoard()
    {
        var totalCount = height * width;
        var remaintTileCount = totalCount - bombCount;
        //For PlayerCount = 2
        var playerOneCount = remaintTileCount / 2;
        var playerTwoCount = remaintTileCount / 2;
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

                var randomAvailableSprite = GetRandomAvailableSprite();
                tile.GetComponent<Tile>().SetupTile(i, j, randomAvailableSprite, questionSprite);
            }
        }
    }
    public void SelectTile(Tile tile)
    {
        if (selectCount >= maxSelectCount)
            return;

        if(selectedTile == null)
        {
            //Select any tile on the board

            selectedTile = tile;
            selectedTile.GetComponent<Tile>().SetBackgroundColor(checkColor);
            selectedTile.GetComponent<TileAnimationController>().PlaySelectTileAnimation();

            if (selectedTile.GetActualSprite() == bombSprite)
                BombEffectActivion(selectedTile.transform.position);

            selectCount++;
        }
        else if(selectedTile == tile)
        {
            selectedTile.GetComponent<TileAnimationController>().PlayWiggleTileAnimation();
            selectCount = 1;
            Debug.Log("Message: Please select new tile on the board");
        }
        else
        {
            //Check other tile is same or not
            checkTile = tile;
            CheckTile(selectedTile, checkTile);
            selectCount++;
        }
    }

    private void CheckTile(Tile selectedTile, Tile checkTile)
    {
        var selectedTileSprite = selectedTile.GetActualSprite();
        var checkTileSprite = checkTile.GetActualSprite();
        var openAnimationTime = 1.5f;
        if(checkTileSprite == bombSprite)
        {
            BombEffectActivion(checkTile.transform.position);
            Invoke(nameof(RefreshBoard), openAnimationTime);
        }
        else if(selectedTileSprite == checkTileSprite)
        {
            Invoke(nameof(HandleCorrectMatch), openAnimationTime);
        }
        else
        {
            Invoke(nameof(HandleMissMatch), openAnimationTime);
        }

        checkTile.GetComponent<Tile>().SetBackgroundColor(checkColor);
        checkTile.GetComponent<TileAnimationController>().PlaySelectTileAnimation();
    }
    private void RefreshBoard()
    {
        Debug.Log("Refreshed the game");
    }
    private void BombEffectActivion(Vector3 position)
    {
        var offsetZ = -2f;
        var explosion = Instantiate(explosionParticle);
        explosion.transform.position = position + new Vector3(0f, 0f, offsetZ);
    }
    private void HandleCorrectMatch()
    {
        Debug.Log("CONGRATZ.. Player can attack other one");
        selectedTile.SetBackgroundColor(isPlayerOneTurn ? playerOneColor : playerTwoColor);
        checkTile.SetBackgroundColor(isPlayerOneTurn ? playerOneColor : playerTwoColor);

        selectedTile.GetComponent<TileAnimationController>().PlayMatchTileAnimation();
        checkTile.GetComponent<TileAnimationController>().PlayMatchTileAnimation();
        selectedTile = null;
        checkTile = null;

        ChangePlayerTurn();
    }
    private void HandleMissMatch()
    {
        Debug.Log("NOOOOO.. Player turn must change");
        selectedTile.SetBackgroundColor(Color.white);
        checkTile.SetBackgroundColor(Color.white);

        selectedTile.GetComponent<TileAnimationController>().PlayMissTileAnimation();
        checkTile.GetComponent<TileAnimationController>().PlayMissTileAnimation();
        selectedTile = null;
        checkTile = null;

        ChangePlayerTurn();
    }

    private void ChangePlayerTurn()
    {
        isPlayerOneTurn = !isPlayerOneTurn;
    }
}
