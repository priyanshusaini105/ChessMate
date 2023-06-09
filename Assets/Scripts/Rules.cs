using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    public static bool IsLegalMove(ChessPieceType type,ChessPieceColor color, Vector2Int startPos, Vector2Int endPos)
    {
        switch (type)
        {
            case ChessPieceType.Pawn:
                return IsLegalPawnMove(startPos, endPos,color);
            case ChessPieceType.Rook:
                return IsLegalRookMove(startPos, endPos);
            case ChessPieceType.Knight:
                return IsLegalKnightMove(startPos, endPos);
            case ChessPieceType.Bishop:
                return IsLegalBishopMove(startPos, endPos);
            case ChessPieceType.Queen:
                return IsLegalQueenMove(startPos, endPos);
            case ChessPieceType.King:
                return IsLegalKingMove(startPos, endPos);
            default:
                return false;
        }
    }

    private static bool IsLegalPawnMove(Vector2Int startPos, Vector2Int endPos,ChessPieceColor color)
    {
        Debug.Log(startPos.x + " " + startPos.y + " " + endPos.x + " " + endPos.y);
        int direction = color == ChessPieceColor.White ? -1 : 1;
        if (startPos.x == endPos.x)
        {
            if (startPos.y + direction == endPos.y)
            {
                return true;
            }
            else if (startPos.y + (direction * 2) == endPos.y)
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsLegalRookMove(Vector2Int startPos, Vector2Int endPos)
    {
        if (startPos.x == endPos.x || startPos.y == endPos.y)
        {
            return true;
        }
        return false;
    }

    private static bool IsLegalKnightMove(Vector2Int startPos, Vector2Int endPos)
    {
        if (startPos.x + 2 == endPos.x && startPos.y + 1 == endPos.y)
        {
            return true;
        }
        else if (startPos.x + 2 == endPos.x && startPos.y - 1 == endPos.y)
        {
            return true;
        }
        else if (startPos.x - 2 == endPos.x && startPos.y + 1 == endPos.y)
        {
            return true;
        }
        else if (startPos.x - 2 == endPos.x && startPos.y - 1 == endPos.y)
        {
            return true;
        }
        else if (startPos.x + 1 == endPos.x && startPos.y + 2 == endPos.y)
        {
            return true;
        }
        else if (startPos.x + 1 == endPos.x && startPos.y - 2 == endPos.y)
        {
            return true;
        }
        else if (startPos.x - 1 == endPos.x && startPos.y + 2 == endPos.y)
        {
            return true;
        }
        else if (startPos.x - 1 == endPos.x && startPos.y - 2 == endPos.y)
        {
            return true;
        }
        return false;
    }

    private static bool IsLegalBishopMove(Vector2Int startPos, Vector2Int endPos)
    {
        if (Mathf.Abs(startPos.x - endPos.x) == Mathf.Abs(startPos.y - endPos.y))
        {
            return true;
        }
        return false;
    }

    private static bool IsLegalQueenMove(Vector2Int startPos, Vector2Int endPos)
    {
        if (IsLegalRookMove(startPos, endPos) || IsLegalBishopMove(startPos, endPos))
        {
            return true;
        }
        return false;
    }

    private static bool IsLegalKingMove(Vector2Int startPos, Vector2Int endPos)
    {
        if (Mathf.Abs(startPos.x - endPos.x) <= 1 && Mathf.Abs(startPos.y - endPos.y) <= 1)
        {
            return true;
        }
        return false;
    }


}
