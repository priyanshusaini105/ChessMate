using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dialogs;

public class GridManager: MonoBehaviour {
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Sprite[] _chessPieceSprites;
    public GameObject movePlate;
    private Tile _currentTile;
    private Tile _targetTile;
    private bool _isWhiteTurn = true;

    private Dictionary < Vector2, Tile > _tiles;
    public ChessPiece _selectedChessPiece;

    void Start() {
        GenerateGrid();
        PlaceInitialChessPieces();
        uDialog.NewDialog()
        .SetDimensions(400, 200)
        .SetThemeImageSet(eThemeImageSet.SciFi)
        .SetColorScheme("Blue Glow")
        .SetTitleText("Welcome to Chess")
        .SetContentText("This is a simple chess game made in Unity. Click OK to start the game.")
        .AddButton("OK", () => {
            Debug.Log("OK");
        });

    }

    void GenerateGrid() {
        _tiles = new Dictionary < Vector2, Tile > ();
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset, x, y);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float) _height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out
                var tile))
            return tile;
        return null;
    }

    // place chess pieces initial
    void PlaceInitialChessPieces() {
        // place pawns
        for (int x = 0; x < _width; x++) {
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

    void PlaceChessPieceAt(int x, int y, ChessPieceType type, ChessPieceColor color) {
        Tile tile = GetTileAtPosition(new Vector2(x, y));
        tile._chessPiece.PlaceChessPiece(type, color);
        tile.SetChessPieceActive(true);
    }

    // move tile from current to target
    public void MoveChessPiece(Tile targetTile) {
        if (_currentTile != null) {
            targetTile._chessPiece.PlaceChessPiece(_selectedChessPiece._type, _selectedChessPiece._color);
            _currentTile.RemoveChessPiece();
            _currentTile.SetActiveTheActiveTile(false);
            _targetTile.SetActiveTheActiveTile(false);
            _currentTile = null;
            _targetTile = null;
            _isWhiteTurn = !_isWhiteTurn;
        }
    }

   // select current tile
    public void SelectTile(Tile tile)
    {

        if (_currentTile == null)
        {
            if (!tile._chessPiece.gameObject.activeSelf)
            {
                return;
            }
            _currentTile = tile;
            _currentTile.SetActiveTheActiveTile(true);
        }
        else
        {
            // do not move if others turn
            if(_isWhiteTurn && _currentTile._chessPiece._color == ChessPieceColor.White)
            {
                DeselectTile();
                log("w-Not your turn ");
                return;
            }
            else if (!_isWhiteTurn && _currentTile._chessPiece._color == ChessPieceColor.Black)
            {
                DeselectTile();
                log("b-Not your turn ");
                return;
            }
            log("your turn ");
            _targetTile = tile;

            // if target tile is the same as current tile, deselect
            if (_currentTile == _targetTile)
            {
                DeselectTile();
                return;
            }

            _currentTile.SetActiveTheActiveTile(true);
            bool isValid = Rules.IsLegalMove(_currentTile._chessPiece._type, _currentTile._chessPiece._color, new Vector2Int(_currentTile.x, _currentTile.y), new Vector2Int(_targetTile.x, _targetTile.y));
            Debug.Log(isValid);
            if (isValid)
            {
                // if the target tile has a chess piece of the same color, do not move
                if (_targetTile._chessPiece.gameObject.activeSelf && (_targetTile._chessPiece._color == _currentTile._chessPiece._color))
                {
                    DeselectTile();
                    return;
                }
                else if (_targetTile._chessPiece.gameObject.activeSelf && (_targetTile._chessPiece._type == ChessPieceType.King))
                {
                    // if the target tile has a chess piece is king so game over win opponent and display dialog
                    MoveChessPiece(_targetTile);
                    // display dialog
                    uDialog.NewDialog()
                        .SetDimensions(400, 200)
                        .SetThemeImageSet(eThemeImageSet.SciFi)
                        .SetColorScheme("Blue Glow")
                        .SetTitleText("Game Over")
                        .AddOnCloseEvent(() =>
                        {
                            PlaceInitialChessPieces();
                        })
                        .SetContentText((_targetTile._chessPiece._color == ChessPieceColor.Black ? "Black" : "White") + " win!")
                        .AddButton("OK", () =>
                        {
                            PlaceInitialChessPieces();
                        });
                    return;
                }else
                // if the target tile has any chess piece, and cureent tile is pawn and target tile is not in corner, do not move
                 if(_currentTile._chessPiece._type == ChessPieceType.Pawn && _targetTile._chessPiece.gameObject.activeSelf){
                    DeselectTile();
                    return;
                }
                MoveChessPiece(_targetTile);
            }
            else if (_currentTile._chessPiece._type == ChessPieceType.Pawn && Rules.IsLegalPawnKillMove(new Vector2Int(_currentTile.x, _currentTile.y), new Vector2Int(_targetTile.x, _targetTile.y), _currentTile._chessPiece._color))
            {
                // if opponent chess piece is in corner and then cross move is allowed
                if (tile._chessPiece.gameObject.activeSelf && (_targetTile._chessPiece._color != _currentTile._chessPiece._color))
                {
                    MoveChessPiece(_targetTile);
                    DeselectTile();
                    return;
                }
            }
            else
            {
                DeselectTile();
                return;
            }
        }
    }

    // do not move 
    private void DeselectTile()
    {
        _currentTile.SetActiveTheActiveTile(false);
        if(this._targetTile != null)
        _targetTile.SetActiveTheActiveTile(false);
        _currentTile = null;
        _targetTile = null;
    }

    void log(string message)
    {
        Debug.Log(message);
    }
}
