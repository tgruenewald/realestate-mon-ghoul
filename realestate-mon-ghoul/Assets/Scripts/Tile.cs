using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool isSet = false;
    [SerializeField] private Color _originalColor;
    [SerializeField] private SpriteRenderer forSale;
    [SerializeField] private SpriteRenderer ghost;
    [SerializeField] private SpriteRenderer ghostDuster;
    [SerializeField] private SpriteRenderer rental;
    [SerializeField] private GameObject floatingText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private ConfirmationWindow confirmationWindow;
    [SerializeField] public int x, y;
    [SerializeField] private GameState _gameState;
    [SerializeField] public int houseCost = 300;
    private int originalHousePrice;
    [SerializeField] public bool houseForSale = false;
    [SerializeField] public bool houseIsHaunted = false;
    [SerializeField] public bool ghostDusterAreThere = false;
    [SerializeField] public bool youOwnHouse = false;
    [SerializeField] public int hauntLevel = 0;
    [SerializeField] public GameObject housePrice;
    [SerializeField] private GhostDuster ghostDusterCar;
    private Queue<string> floatingMessagesQueues = new Queue<string>();


    public void Init(bool isOffset, int x, int y, GameState gameState) {
        originalHousePrice = houseCost;
        this.x = x;
        this.y = y;
        this._gameState = gameState;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _originalColor = _renderer.color;
        confirmationWindow.yesButton.onClick.AddListener(YesClick);
        confirmationWindow.noButton.onClick.AddListener(NoClick);
        confirmationWindow.gameObject.SetActive(false);
        if (x == 0 && y == 0)
        {
            // ghostduster initial home
            ghostDuster.enabled = true;
            ghostDusterAreThere = true;
            ghostDusterCar = new GhostDuster();
            ghostDusterCar.setHome(x, y);

        }
        StartCoroutine(DoFloatingTextAfterTime());
    }

    public void increaseHousePrice(int price)
    {
        houseCost = houseCost + price;
        originalHousePrice = houseCost;
    }

    void OnMouseEnter() {
        //_highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        //_highlight.SetActive(false);
    }

    public void setGhostDuster(GhostDuster car)
    public void setGhostDuster(GhostDuster car)
    {
        ghostDuster.enabled = true;
        ghostDusterAreThere = true;
        ghostDusterCar = car;
        ghostDusterCar = car;
    }

    public GhostDuster removeGhostDuster()
    public GhostDuster removeGhostDuster()
    {
        GhostDuster temp = ghostDusterCar;
        ghostDusterCar = null;
        GhostDuster temp = ghostDusterCar;
        ghostDusterCar = null;
        ghostDuster.enabled = false;
        ghostDusterAreThere = false;
        return temp;
    }

    public GhostDuster getGhostDusterCar()
    {
        return ghostDusterCar;
    }


    private IEnumerator DoFloatingTextAfterTime()
    {
        while (true)
        {
            if (floatingMessagesQueues.Count > 0)
            {
                string text = floatingMessagesQueues.Dequeue();
                Debug.Log("Creating floating text: " + text);
                var myText = Instantiate(floatingText, housePrice.transform);
                myText.GetComponent<TMP_Text>().text = text;
                myText.transform.SetParent(canvas.transform, false);
                //myText.transform.position = new Vector2(housePrice.transform.position.x, housePrice.transform.position.y - 8f);
                myText.transform.position = housePrice.transform.position;
                myText.transform.Translate(0, -1f, 0, Space.Self);
                myText.transform.localScale = housePrice.transform.localScale;
                //myText.GetComponent<TMP_Text>().transform.position = transform.position;
                //myText.transform.localPosition = housePrice.transform.localPosition;
            }
            yield return new WaitForSeconds(0.2f);  // Wait for the specified duration
        }
        

    }

    public void CreateFloatingText(string text)
    {
        floatingMessagesQueues.Enqueue(text);

    }

    public void YesClick()
    {
        GameState myGamesState = GameObject.Find("GameState").GetComponent<GameState>();
        Debug.Log("yes game state: " + myGamesState);
        Debug.Log("yes: " + myGamesState.getAddressX() + ", " + myGamesState.getAddressY());
        housePrice.GetComponent<TMP_Text>().text = "";
        if (myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseForSale)
        {
            myGamesState.deductFunds(myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseCost);
            myGamesState.deductFunds(myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseCost);
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseOffMarket();
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseForSale = false;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).youOwnHouse = true;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).houseIsHaunted = false;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).housePrice.GetComponent<TMP_Text>().text = "";
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).rental.enabled = true;
            myGamesState.getTile(myGamesState.getAddressX(), myGamesState.getAddressY()).CreateFloatingText("Bought");


        }
        

        ConfirmationWindow myconfirmationWindow = GameObject.Find("ConfirmationWindowOnCanvas").GetComponent<ConfirmationWindow>();
        myconfirmationWindow.gameObject.SetActive(false);
        myGamesState.playerAction = false;
    }

    public void NoClick()
    {
        Debug.Log("no");
        ConfirmationWindow myconfirmationWindow = GameObject.Find("ConfirmationWindowOnCanvas").GetComponent<ConfirmationWindow>();
        myconfirmationWindow.gameObject.SetActive(false);
    }

    public void houseOffMarket()
    {
        housePrice.GetComponent<TMP_Text>().text = "";
        rental.enabled = false;
        rental.enabled = false;
        houseCost = originalHousePrice;
        houseForSale = false;
        houseIsHaunted = false;
        houseIsHaunted = false;
        hauntLevel = 0;
        forSale.enabled = false;
        ghost.enabled = false;
    }

    public void reduceHousePrice(int price)
    {
        houseCost -= price;
        Debug.Log("House reduced to " + houseCost);
        if (houseCost < 200)
        {
            CreateFloatingText("Sale ending");
        }
        if (houseCost < 101)
        {
            // house goes off market is and no longer for sale
            houseOffMarket();

        } else
        {
            housePrice.GetComponent<TMP_Text>().text = "" + houseCost;
        }


    }

    public bool exorcise(int randomNumber)
    {
        if (hauntLevel == 10)
        {
            if (randomNumber >= 1 && randomNumber <= 3 + ghostDusterCar.getXP(10))
            if (randomNumber >= 1 && randomNumber <= 3 + ghostDusterCar.getXP(10))
            {
                CreateFloatingText("No longer haunted");
                Debug.Log("House is no longer haunted");
                ghostDusterCar.increaseXP(10);
                ghostDusterCar.increaseXP(10);
                houseOffMarket();
                return true;

            }
            else
            {
                Debug.Log("House is still haunted");
                CreateFloatingText("Removing spirits...");
                return false;
            }
        } else if (hauntLevel == 100) {
            if (randomNumber >= 10 && randomNumber <= 11 + ghostDusterCar.getXP(100))
        } else if (hauntLevel == 100) {
            if (randomNumber >= 10 && randomNumber <= 11 + ghostDusterCar.getXP(100))
            {
                Debug.Log("House is no longer haunted");
                CreateFloatingText("No longer haunted");
                ghostDusterCar.increaseXP(100);
                houseOffMarket();
                return true;

            }
            else
            {
                Debug.Log("House is still haunted");
                return false;
            }
        } else if (hauntLevel == 1000)
        } else if (hauntLevel == 1000)
        {
            if (randomNumber >= 2 && randomNumber <= 2 + ghostDusterCar.getXP(1000))
            if (randomNumber >= 2 && randomNumber <= 2 + ghostDusterCar.getXP(1000))
            {
                Debug.Log("House is no longer haunted");
                CreateFloatingText("No longer haunted");
                ghostDusterCar.increaseXP(1000);
                houseOffMarket();
                return true;

            }
            else
            {
                Debug.Log("House is still haunted");
                return false;
            }
        } else if (hauntLevel > 0)
        {
            // shouldn't be possible
            if (randomNumber >= 2 && randomNumber <= 15)
            {
                Debug.Log("House is no longer haunted");
                CreateFloatingText("No longer haunted");
                houseOffMarket();
                return true;

            }
            else
            {
                Debug.Log("House is still haunted");
                return false;
            }
        } else
        {
            return true;
        }

    }

    private void OnMouseDown()
    {
        
        if (!_gameState.playerAction)
        {
            Debug.Log("not players turn");
            return;
        }
        Debug.Log("mouse down now2");
        _gameState.setAddress(x, y);
        if (houseForSale && _gameState.getFunds() > houseCost)
        if (houseForSale && _gameState.getFunds() > houseCost)
        {
            CreateFloatingText("Attempting to buy house");
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

        if (youOwnHouse)
        {
            return;
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        _gameState.setHint("Spirit has been summoned");
        CreateFloatingText("Spirit has been summoned");
        hauntLevel = _gameState.getGhostLevel();
        // TODO: I'll need to use the ghost level to set a different sprite
        _gameState.resetGhostLevel();
        rental.enabled = false;
        forSale.enabled = true;
        housePrice.GetComponent<TMP_Text>().text = "" + houseCost;
        houseForSale = true;
        youOwnHouse = false;
        ghost.enabled = true;
        houseForSale = true;
        houseIsHaunted = true;
        _gameState.hauntHouse(this);


        _gameState.playerAction = false;

    }
}