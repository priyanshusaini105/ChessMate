using UnityEngine;

public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum ChessPieceColor
{
    Black,
    White
}

public class ChessPiece : MonoBehaviour
{
    [SerializeField] private ChessPieceType _type;
    [SerializeField] private ChessPieceColor _color;

    // // get the sprite  of the chess piece
    // public Sprite Sprite => _spriteRenderer.sprite;

    // private SpriteRenderer _spriteRenderer;
    // private Vector3 _initialScale = new Vector3(0.8f, 0.8f, 1f);

    // public Tile CurrentTile { get; private set; }

    // public void Init(ChessPieceType type, ChessPieceColor color)
    // {
    //     _type = type;
    //     _color = color;
    //     _spriteRenderer = GetComponent<SpriteRenderer>();

    //     // Set the sorting order of the sprite renderer to ensure it is above the tiles
    //     _spriteRenderer.sortingOrder = 2;
    //     transform.localScale = _initialScale;
    // }

    // public void ToggleHighlight(bool isSelected)
    // {
    //     // Implement the highlight toggling logic for the chess piece here
    //     // You can change the color, scale, or any other visual representation of the chess piece to indicate it is selected or deselected
    //     // The `isSelected` parameter can be used to determine the state of the highlight (selected or deselected)
    // }

    // public void MoveToTile(Tile targetTile)
    // {
    //     // Implement the movement logic for the chess piece here
    //     // You can update the position or transform of the chess piece to move it to the target tile
    //     // You may also need to update the reference to the current tile

    //     // Example code to update the current tile reference:
    //     if (CurrentTile != null)
    //         CurrentTile.RemoveChessPiece();

    //     CurrentTile = targetTile;
    // }
}
