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

    private SpriteRenderer _spriteRenderer;

    public void Init(ChessPieceType type, ChessPieceColor color)
    {
        _type = type;
        _color = color;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the sorting order of the sprite renderer to ensure it is above the tiles
        _spriteRenderer.sortingOrder = 2;
    }
}
