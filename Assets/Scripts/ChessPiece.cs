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
    public ChessPieceType _type;
    public ChessPieceColor _color;
    [SerializeField] public Sprite[] _chessPieceSprites;
    public SpriteRenderer _ChessPieceSpriteRenderer;
    private Sprite _chessPieceSprite;


    // place chess piece on tile
    public void PlaceChessPiece(ChessPieceType type, ChessPieceColor color)
    {
        _type = type;
        _color = color;
        _chessPieceSprite= GetChessPieceSprite(type, color);
        if(_chessPieceSprite != null){
            this.gameObject.SetActive(true);
            _ChessPieceSpriteRenderer = GetComponent<SpriteRenderer>();
            _ChessPieceSpriteRenderer.sprite = _chessPieceSprite;
        }
    }
    
    // get the sprite  of the chess piece
    private Sprite GetChessPieceSprite(ChessPieceType type, ChessPieceColor color)
    {
        int index = (int)type + ((int)color * 6);
        if (index >= 0 && index < _chessPieceSprites.Length)
        {
            return _chessPieceSprites[index];
        }
        return null;
    }

    // get chess piece 
    public Sprite getChessPiece(){
        return _chessPieceSprite;
    }

}
