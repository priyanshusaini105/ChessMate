using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private SpriteRenderer _chessPieceRenderer;
 
    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
 
    void OnMouseEnter() {
        Debug.Log("Mouse enter");
        _highlight.SetActive(true);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

     public void PlaceChessPiece(Sprite chessPieceSprite) {
        if (_chessPieceRenderer == null) {
            GameObject chessPieceObject = new GameObject("ChessPiece");
            chessPieceObject.transform.SetParent(transform);
            chessPieceObject.transform.localPosition = Vector3.zero;
            _chessPieceRenderer = chessPieceObject.AddComponent<SpriteRenderer>();
        }

        _chessPieceRenderer.sprite = chessPieceSprite;
    }
}