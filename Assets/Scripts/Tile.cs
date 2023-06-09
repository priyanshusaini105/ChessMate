using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    public ChessPiece _chessPiece;


    // [SerializeField] private GameObject _movePlate;
    // private ChessPiece _chessPiece;

    // private bool _isSelected = false;

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

    // private void OnMouseEnter()
    // {
    //     if (!_isSelected)
    //     {
    //         _highlight.SetActive(true);
    //     }
    // }

    // private void OnMouseExit()
    // {
    //     if (!_isSelected)
    //     {
    //         _highlight.SetActive(false);
    //     }
    // }

    // private void OnMouseDown()
    // {
    //     GridManager gridManager = FindObjectOfType<GridManager>();
    //     if (gridManager != null)
    //     {
    //         gridManager.SelectTile(this);
    //     }
    // }

    // public void ActivateMovePlate()
    // {
    //     _movePlate.SetActive(true);
    // }
    // public void DeactivateMovePlate()
    // {
    //     _movePlate.SetActive(false);
    // }

    // public void ActivateHighlight()
    // {
    //     _highlight.SetActive(true);
    // }

    // public void DeactivateHighlight()
    // {
    //     _highlight.SetActive(false);
    // }

    // public void ToggleHighlight()
    // {
    //     _highlight.SetActive(!_isSelected);
    //     _isSelected = !_isSelected;
    // }

    // public void PlaceChessPiece(Sprite chessPieceSprite)
    // {
    //     if (_chessPieceRenderer == null)
    //     {
    //         GameObject chessPieceObject = new GameObject(name);
    //         chessPieceObject.transform.SetParent(transform);
    //         chessPieceObject.transform.localPosition = Vector3.zero;
    //         _chessPieceRenderer = chessPieceObject.AddComponent<SpriteRenderer>();
    //         _chessPieceRenderer.sortingOrder = 1;
    //         _chessPiece = chessPieceObject.AddComponent<ChessPiece>();
    //     }
    //     _chessPieceRenderer.sprite = chessPieceSprite;
    // }


    // public void RemoveChessPiece()
    // {
    //     if (_chessPieceRenderer != null)
    //     {
    //         Destroy(_chessPieceRenderer.gameObject);
    //         _chessPieceRenderer = null;
    //         _chessPiece = null;
    //     }
    // }
}
