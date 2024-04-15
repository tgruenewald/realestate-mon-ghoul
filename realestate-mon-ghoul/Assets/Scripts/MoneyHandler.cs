using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject turnIndicator;
    [SerializeField] private GameObject hint;
    private Texture2D ghostCursor;

    public TMP_Text moneyAmount;


    void Start (){
        ghostCursor = Resources.Load<Texture2D>("ghostPointer");
        Debug.Log("ghost cursor: " +  ghostCursor);

    }
    void Update()
    {
        if (gameState != null)
        {
            moneyAmount.text = gameState.getFunds().ToString();
            turnIndicator.GetComponent<TMP_Text>().text = "Turn " + gameState.turnNumber + " of " + gameState.maxTurns;
            if (gameState.turnNumber >= gameState.maxTurns)
            {
                turnIndicator.GetComponent<TMP_Text>().text = "GAME OVER";
            }

            hint.GetComponent<TMP_Text>().text = gameState.getHint();
        }

    }
    public void PurchaseGhost(int price){
        if (gameState.playerAction)
        {
            if (price == 0)
            {
                Debug.Log("Skipping turn");
                gameState.playerAction = false;
            } else
            {
                if (gameState.getFunds() > price && gameState.getGhostLevel() == 0)
                {
                    Cursor.SetCursor(ghostCursor, Vector2.zero, CursorMode.Auto);
                    gameState.setHint("Pick a house to haunt");
                    gameState.setGhostLevel(price);
                    gameState.deductFunds(price);
                    moneyAmount.text = gameState.getFunds().ToString();
                }

            }

        }

    }
}
