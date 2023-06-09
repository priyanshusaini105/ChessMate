using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _activeTile;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _voidActiveSprite;
    public ChessPiece _chessPiece;


    // [SerializeField] private GameObject _movePlate;
    // private ChessPiece _chessPiece;

    private bool _isSelected = false;

    // private SpriteRenderer _chessPieceRenderer;

    // public ChessPiece ChessPiece => _chessPiece;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _highlight.SetActive(false);
    }

    // set active chess piece
    public void SetChessPieceActive(bool _isActive)
    {
        _chessPiece.gameObject.SetActive(_isActive);
    }

    // set active active tile
    public void SetActiveTheActiveTile(bool isActive)
    {
        _activeTile.SetActive(isActive);

        SpriteRenderer activeTileRenderer = _activeTile.GetComponent<SpriteRenderer>();
        if (isActive)
        {
            if (_chessPiece != null && _chessPiece.gameObject.activeSelf)
            {
                activeTileRenderer.sprite = _activeSprite;
            }
            else
            {
                activeTileRenderer.sprite = _voidActiveSprite;
            }
        }
    }


    //  activate the tile
    private void OnMouseDown()
    {
    GridManager gridManager = FindObjectOfType<GridManager>();
    if (gridManager != null)
    {
        gridManager.SelectTile(this);
        gridManager._selectedChessPiece=this._chessPiece;
    }
    }

    private void OnMouseEnter()
    {
        if (!_isSelected)
        {
            _highlight.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!_isSelected)
        {
            _highlight.SetActive(false);
        }
    }

    // remove chess piece
    public void RemoveChessPiece(){
        if(_chessPiece != null){
            _chessPiece._ChessPieceSpriteRenderer=null;
            _chessPiece.gameObject.SetActive(false);
        }
    }


   
}
