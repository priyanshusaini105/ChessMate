using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Sprite[] _chessPieceSprites;
    public GameObject movePlate;

    private Dictionary<Vector2, Tile> _tiles;
    private Tile _selectedTile;
    private ChessPiece _selectedChessPiece;

    void Start()
    {
        GenerateGrid();
        PlaceInitialChessPieces();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
            return tile;
        return null;
    }

    // place chess pieces initial
    void PlaceInitialChessPieces()
    {
        // place pawns
        for (int x = 0; x < _width; x++)
        {
            PlaceChessPieceAt(x, 1, ChessPieceType.Pawn, ChessPieceColor.Black);
            PlaceChessPieceAt(x, 6, ChessPieceType.Pawn, ChessPieceColor.White);
        }

        // Place rooks
        PlaceChessPieceAt(0, 0, ChessPieceType.Rook, ChessPieceColor.Black);
        PlaceChessPieceAt(7, 0, ChessPieceType.Rook, ChessPieceColor.Black);
        PlaceChessPieceAt(0, 7, ChessPieceType.Rook, ChessPieceColor.White);
        PlaceChessPieceAt(7, 7, ChessPieceType.Rook, ChessPieceColor.White);

        // Place knights
        PlaceChessPieceAt(1, 0, ChessPieceType.Knight, ChessPieceColor.Black);
        PlaceChessPieceAt(6, 0, ChessPieceType.Knight, ChessPieceColor.Black);
        PlaceChessPieceAt(1, 7, ChessPieceType.Knight, ChessPieceColor.White);
        PlaceChessPieceAt(6, 7, ChessPieceType.Knight, ChessPieceColor.White);

        // Place bishops
        PlaceChessPieceAt(2, 0, ChessPieceType.Bishop, ChessPieceColor.Black);
        PlaceChessPieceAt(5, 0, ChessPieceType.Bishop, ChessPieceColor.Black);
        PlaceChessPieceAt(2, 7, ChessPieceType.Bishop, ChessPieceColor.White);
        PlaceChessPieceAt(5, 7, ChessPieceType.Bishop, ChessPieceColor.White);

        // Place queens
        PlaceChessPieceAt(3, 0, ChessPieceType.Queen, ChessPieceColor.Black);
        PlaceChessPieceAt(3, 7, ChessPieceType.Queen, ChessPieceColor.White);

        // Place kings
        PlaceChessPieceAt(4, 0, ChessPieceType.King, ChessPieceColor.Black);
        PlaceChessPieceAt(4, 7, ChessPieceType.King, ChessPieceColor.White);
    }

    void PlaceChessPieceAt(int x, int y, ChessPieceType type, ChessPieceColor color)
    {
        Tile tile = GetTileAtPosition(new Vector2(x, y));
        tile._chessPiece.PlaceChessPiece(type,color);
        tile.SetChessPieceActive(true);
    }

    

    // public void SelectTile(Tile tile)
    // {
    //     if (_selectedTile != null)
    //     {
    //         _selectedTile.ToggleHighlight(); // Deselect previous tile
    //         _selectedTile = tile; // Select new tile
    //         _selectedTile.ToggleHighlight();
    //         if (_selectedChessPiece != null)
    //         {
    //             MoveChessPiece(tile);
    //         }
    //         else
    //         {
    //             SelectChessPiece(tile.ChessPiece);
    //         }
    //     }
    //     else
    //     {
    //         _selectedTile = tile;
    //         _selectedTile.ToggleHighlight();
    //     }
    // }

    // public void SelectChessPiece(ChessPiece chessPiece)
    // {
    //     if (_selectedChessPiece != null)
    //     {
    //         _selectedChessPiece.ToggleHighlight(false); // Deselect previous chess piece
    //         _selectedChessPiece = chessPiece; // Select new chess piece
    //         _selectedChessPiece.ToggleHighlight(true);
    //     }
    //     else
    //     {
    //         _selectedChessPiece = chessPiece;
    //         _selectedChessPiece.ToggleHighlight(true);
    //     }
    // }

    // public void MoveChessPiece(Tile targetTile)
    // {
    //     if (_selectedChessPiece != null)
    //     {
    //         Tile currentTile = _selectedChessPiece.CurrentTile;

    //         // Check if the target tile is a valid move for the selected chess piece
    //         if (IsValidMove(_selectedChessPiece, currentTile, targetTile))
    //         {
    //             _selectedChessPiece.MoveToTile(targetTile);
    //             currentTile.RemoveChessPiece();
    //             targetTile.PlaceChessPiece(_selectedChessPiece.Sprite);
    //         }

    //         // Deselect the chess piece and the tile
    //         _selectedChessPiece.ToggleHighlight(false);
    //         _selectedTile.ToggleHighlight();
    //         _selectedChessPiece = null;
    //         _selectedTile = null;
    //     }
    // }

    // private bool IsValidMove(ChessPiece chessPiece, Tile currentTile, Tile targetTile)
    // {
    //     // Implement your own logic to determine if the move is valid based on the chess piece's rules
    //     // You can access the type and color of the chess piece to determine its movement rules
    //     // You can also check the positions of the current tile and target tile to determine the validity of the move

    //     // Return true if the move is valid, otherwise return false
    //     return true;
    // }
}
