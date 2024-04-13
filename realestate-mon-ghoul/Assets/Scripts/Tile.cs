using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool isSet = false;
    [SerializeField] private Color _originalColor;
    [SerializeField] private SpriteRenderer forSale;
    [SerializeField] private SpriteRenderer ghost;
    [SerializeField] private ConfirmationWindow confirmationWindow;
    [SerializeField] private int x, y;
    [SerializeField] private GameState _gameState;

    public void Init(bool isOffset, int x, int y, GameState gameState) {
        Debug.Log("Offset " + isOffset);
        this.x = x;
        this.y = y;
        this._gameState = gameState;
        Debug.Log("gamestate: " +  gameState);
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _originalColor = _renderer.color;
        confirmationWindow.yesButton.onClick.AddListener(YesClick);
        confirmationWindow.noButton.onClick.AddListener(NoClick);
        confirmationWindow.gameObject.SetActive(false);
        Debug.Log("confirmation window: " + confirmationWindow.gameObject);
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

    public void YesClick()
    {
        GameState myGamesState = GameObject.Find("GameState").GetComponent<GameState>();
        Debug.Log("yes game state: " + myGamesState);
        Debug.Log("yes: " + myGamesState.getAddressX() + ", " + myGamesState.getAddressY());
        

        ConfirmationWindow myconfirmationWindow = GameObject.Find("ConfirmationWindowOnCanvas").GetComponent<ConfirmationWindow>();
        myconfirmationWindow.gameObject.SetActive(false);
    }

    public void NoClick()
    {
        Debug.Log("no");
        confirmationWindow.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse down now");
        _gameState.setAddress(x, y);
        isSet = !isSet;
        ConfirmationWindow[] onlyInactive = FindObjectsByType<ConfirmationWindow>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
        if (onlyInactive.Length > 0)
        {
            onlyInactive[0].gameObject.SetActive(true);
        }


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