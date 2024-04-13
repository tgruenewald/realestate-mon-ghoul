using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool isSet = false;
    [SerializeField] private Color _originalColor;
    [SerializeField] private SpriteRenderer forSale;
    [SerializeField] private SpriteRenderer ghost;

    public void Init(bool isOffset) {
        Debug.Log("Offset " + isOffset);
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _originalColor = _renderer.color;
    }

    void OnMouseEnter() {
        //_highlight.SetActive(true);
        Debug.Log("Mouse enter");
    }

    void OnMouseExit()
    {
        //_highlight.SetActive(false);
        Debug.Log("Mouse exit!");
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse down");
        isSet = !isSet;
        if (isSet)
        {
            // Color alpha = forSale.color;
            // alpha.a = 1f;
            // forSale.color = alpha;
            forSale.enabled = true;
            ghost.enabled = true;
        } else
        {
            // Color alpha = forSale.color;
            // alpha.a = 0f;
            // forSale.color = alpha; 
            forSale.enabled = false;           
            ghost.enabled = false;
        }
        
        
    }
}