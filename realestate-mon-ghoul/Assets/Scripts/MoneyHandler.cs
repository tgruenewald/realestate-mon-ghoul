using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    public TMP_Text moneyAmount;


    void Start (){
        
    }
    void Update()
    {
        if (gameState != null)
        {
            moneyAmount.text = gameState.getFunds().ToString();
        }

    }
    public void PurchaseGhost(int price){
        gameState.setGhostLevel(price);
        gameState.deductFunds(price);
        moneyAmount.text = gameState.getFunds().ToString();
    }
}
