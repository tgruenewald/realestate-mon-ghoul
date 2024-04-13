using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] public int houseCost = 600;
    [SerializeField] public bool houseForSale = false;
    [SerializeField] public bool houseIsHaunted = false;
    [SerializeField] public bool youOwnHouse = false;
    [SerializeField] public GameObject housePrice;

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
        housePrice.GetComponent<TMP_Text>().text = "";
        if (myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseForSale)
        {
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseForSale = false;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).youOwnHouse = true;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseIsHaunted = false;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).housePrice.GetComponent<TMP_Text>().text = "Rent";
        }
        myGamesState.deductFunds(myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseCost);

        ConfirmationWindow myconfirmationWindow = GameObject.Find("ConfirmationWindowOnCanvas").GetComponent<ConfirmationWindow>();
        myconfirmationWindow.gameObject.SetActive(false);
    }

    public void NoClick()
    {
        Debug.Log("no");
        ConfirmationWindow myconfirmationWindow = GameObject.Find("ConfirmationWindowOnCanvas").GetComponent<ConfirmationWindow>();
        myconfirmationWindow.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse down now2");
        if (houseForSale)
        {
            ConfirmationWindow[] onlyInactive = FindObjectsByType<ConfirmationWindow>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
            if (onlyInactive.Length > 0)
            {
                onlyInactive[0].gameObject.SetActive(true);
                return;
            }
        }
        if (_gameState.getGhostLevel() == 0 )
        {
            Debug.Log("ghost level is zero");
            return;
        }

        // TODO: I'll need to use the ghost level to set a different sprite
        _gameState.resetGhostLevel();

        _gameState.setAddress(x, y);
        isSet = !isSet;




        if (isSet)
        {
            

            // Color alpha = forSale.color;
            // alpha.a = 1f;
            // forSale.color = alpha;
            forSale.enabled = true;
            housePrice.GetComponent<TMP_Text>().text = "" + houseCost;
            houseForSale = true;
            ghost.enabled = true;
            houseForSale = true;
        } else
        {
            // Color alpha = forSale.color;
            // alpha.a = 0f;
            // forSale.color = alpha; 
            forSale.enabled = false;           
            ghost.enabled = false;
            houseForSale = false;
        }
        
        
    }
}