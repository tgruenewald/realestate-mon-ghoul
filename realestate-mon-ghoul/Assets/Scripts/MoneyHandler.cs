using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyHandler : MonoBehaviour
{
    public TMP_Text moneyAmount;
    public int priceMin = 0;
    public int priceMax = 100;
    public int startingMoney = 0;
    private int currMoney = 0;
    void Start (){
        currMoney = startingMoney;
        moneyAmount.text = currMoney.ToString();
    }
    public void PurchaseGhost(int price){
         currMoney = currMoney - price;
         moneyAmount.text = currMoney.ToString();
    }
}
