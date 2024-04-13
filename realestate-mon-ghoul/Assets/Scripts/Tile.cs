using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public void Init(bool isOffset) {
        Debug.Log("Offset " + isOffset);
        _renderer.color = isOffset ? _offsetColor : _baseColor;
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
        //_highlight.SetActive(true);
    }
}