using System.Collections;
using System.Collections.Generic;
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

    public void Init(ChessPieceType type, ChessPieceColor color) {
        _type = type;
        _color = color;
    }
}
