using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;
 
    [SerializeField] private Tile _tilePrefab;
 
    [SerializeField] private Transform _cam;
 
    private Dictionary<Vector2, Tile> _tiles;

    [SerializeField] private Sprite[] _chessPieceSprites;

 
    void Start() {
        GenerateGrid();
        PlaceInitialChessPieces();
    }
 
    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
 
 
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
 
        _cam.transform.position = new Vector3((float)_width/2 -0.5f, (float)_height / 2 - 0.5f,-10);
    }
 
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

      void PlaceInitialChessPieces() {
        // place pawns
        for (int x = 0; x < _width; x++) {
            PlaceChessPiece(x, 1, ChessPieceType.Pawn, ChessPieceColor.Black);
            PlaceChessPiece(x, 6, ChessPieceType.Pawn, ChessPieceColor.White);
        }

        // Placer rooks
        PlaceChessPiece(0, 0, ChessPieceType.Rook, ChessPieceColor.Black);
        PlaceChessPiece(7, 0, ChessPieceType.Rook, ChessPieceColor.Black);
        PlaceChessPiece(0, 7, ChessPieceType.Rook, ChessPieceColor.White);
        PlaceChessPiece(7, 7, ChessPieceType.Rook, ChessPieceColor.White);

        // Place knights
        PlaceChessPiece(1, 0, ChessPieceType.Knight, ChessPieceColor.Black);
        PlaceChessPiece(6, 0, ChessPieceType.Knight, ChessPieceColor.Black);
        PlaceChessPiece(1, 7, ChessPieceType.Knight, ChessPieceColor.White);
        PlaceChessPiece(6, 7, ChessPieceType.Knight, ChessPieceColor.White);

        // Place bishops
        PlaceChessPiece(2, 0, ChessPieceType.Bishop, ChessPieceColor.Black);
        PlaceChessPiece(5, 0, ChessPieceType.Bishop, ChessPieceColor.Black);
        PlaceChessPiece(2, 7, ChessPieceType.Bishop, ChessPieceColor.White);
        PlaceChessPiece(5, 7, ChessPieceType.Bishop, ChessPieceColor.White);

        // Place queens
        PlaceChessPiece(4, 0, ChessPieceType.Queen, ChessPieceColor.Black);
        PlaceChessPiece(4, 7, ChessPieceType.Queen, ChessPieceColor.White);

        // Place kings
        PlaceChessPiece(3, 0, ChessPieceType.King, ChessPieceColor.Black);
        PlaceChessPiece(3, 7, ChessPieceType.King, ChessPieceColor.White);
    }

    
    void PlaceChessPiece(int x, int y, ChessPieceType type, ChessPieceColor color) {
        Tile tile = GetTileAtPosition(new Vector2(x, y));
        Sprite chessPieceSprite = GetChessPieceSprite(type, color);
        tile.PlaceChessPiece(chessPieceSprite);
    }

     private Sprite GetChessPieceSprite(ChessPieceType type, ChessPieceColor color) {
        int index = (int)type + ((int)color * 6); 
        if (index >= 0 && index < _chessPieceSprites.Length) {
            return _chessPieceSprites[index];
        }
        return null; 
    }
}