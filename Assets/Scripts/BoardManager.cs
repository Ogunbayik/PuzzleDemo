using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public event Action OnMatch;
    public event Action OnMiss;
    public event Action OnRefreshBoard;

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
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color playerOneColor;
    [SerializeField] private Color playerTwoColor;
    [Header("Particle Settings")]
    [SerializeField] private GameObject explosionParticle;

    public List<Sprite> allGreenTypeSprites = new List<Sprite>();
    public List<Sprite> allRedTypeSprites = new List<Sprite>();
    public List<Sprite> availableSprites = new List<Sprite>();

    private Tile selectedTile;
    private Tile checkedTile;

    private int playerSpriteCount;
    private int selectCount;
    private int maxSelectCount = 2;
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
            var randomGreenIndex = UnityEngine.Random.Range(0, allGreenTypeSprites.Count);
            var randomRedIndex = UnityEngine.Random.Range(0, allRedTypeSprites.Count);

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
        var randomIndex = UnityEngine.Random.Range(0, availableSprites.Count);
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
            selectCount++;
            selectedTile = tile;
            selectedTile.GetComponent<Tile>().SetBackgroundColor(selectedColor);
            selectedTile.GetComponent<TileAnimationController>().PlayOpenTileAnimation();

            if(selectedTile.GetActualSprite() == bombSprite)
            {
                StartCoroutine(RefreshBoardSequence(selectedTile, null));
            }

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
            if (checkedTile != null)
                return;

            checkedTile = tile;
            selectCount++;
            checkedTile.SetBackgroundColor(selectedColor);
            checkedTile.GetComponent<TileAnimationController>().PlayOpenTileAnimation();
            CheckedForMatch(selectedTile, checkedTile);
        }
    }
    private void CheckedForMatch(Tile selectedTile, Tile checkedTile)
    {
        if (checkedTile != null)
        {
            var selectedTileSprite = selectedTile.GetActualSprite();
            var checkTileSprite = checkedTile.GetActualSprite();
            if (checkTileSprite == bombSprite)
            {
                StartCoroutine(RefreshBoardSequence(selectedTile, checkedTile));
                OnRefreshBoard?.Invoke();
            }
            else if (selectedTileSprite == checkTileSprite)
            {
                StartCoroutine(HandleCorrectMatchSequence(selectedTile, checkedTile));
                OnMatch?.Invoke();
            }
            else
            {
                StartCoroutine(HandleMissMatchSequence(selectedTile, checkedTile));
                OnMiss?.Invoke();
            }
        }
    }
    private IEnumerator RefreshBoardSequence(Tile selectedTile, Tile checkedTile)
    {
        yield return new WaitForSeconds(Consts.TileAnimationTime.OPEN_ANIMATION_TIME);
        if (checkedTile != null)
            BombEffectActivion(checkedTile.transform.position);
        else
            BombEffectActivion(selectedTile.transform.position);
        yield return new WaitForSeconds(2f);
        RefreshBoard();
    }
    private IEnumerator HandleCorrectMatchSequence(Tile selectedTile, Tile checkedTile)
    {
        TurnManager.Instance.SetTargetList();
        Debug.Log("CONGRATZ.. Player can attack other one");
        yield return new WaitForSeconds(Consts.TileAnimationTime.OPEN_ANIMATION_TIME);
        //selectedTile.SetBackgroundColor(isPlayerOneTurn ? playerOneColor : playerTwoColor);
        //checkedTile.SetBackgroundColor(isPlayerOneTurn ? playerOneColor : playerTwoColor);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Write Correct or Incorret");
        yield return new WaitForSeconds(0.5f);
        selectedTile.GetComponent<TileAnimationController>().PlayMatchTileAnimation();
        checkedTile.GetComponent<TileAnimationController>().PlayMatchTileAnimation();
        yield return new WaitForSeconds(1f);
        //Burada Player için seçim yapýlacak diðer playerlar görünecek.
        int targetCount = TurnManager.Instance.GetTargetList().Count;
        GameUI.Instance.SetupPanel(targetCount);
    }
    private IEnumerator HandleMissMatchSequence(Tile selectedTile, Tile checkedTile)
    {
        Debug.Log("NOOOOO.. Player turn must change");
        yield return new WaitForSeconds(Consts.TileAnimationTime.OPEN_ANIMATION_TIME);
        selectedTile.GetComponent<TileAnimationController>().PlayMissTileAnimation();
        checkedTile.GetComponent<TileAnimationController>().PlayMissTileAnimation();
        yield return new WaitForSeconds(0.5f);
        selectedTile.SetBackgroundColor(Color.white);
        checkedTile.SetBackgroundColor(Color.white);
        yield return new WaitForSeconds(1f);
        ResetSelectCount();
        TurnManager.Instance.AdvanceTurn();
    }
    private void RefreshBoard()
    {
        List<GameObject> allTiles = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
            allTiles.Add(transform.GetChild(i).gameObject);

        foreach (var tile in allTiles)
        {
            tile.GetComponent<TileAnimationController>().PlayRandomFallAnimation();
        }
    }
    private void BombEffectActivion(Vector3 position)
    {
        var offsetZ = -2f;
        var explosion = Instantiate(explosionParticle);
        explosion.transform.position = position + new Vector3(0f, 0f, offsetZ);
    }
    public void ResetSelectCount()
    {
        selectCount = 0;
        selectedTile = null;
        checkedTile = null;
    }
}
