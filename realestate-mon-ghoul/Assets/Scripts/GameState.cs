using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private int funds = 0;
    [SerializeField] private int houseAddressX;
    [SerializeField] private int houseAddressY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getAddressX()
    {
        return houseAddressX; 
    }
    public int getAddressY()
    {
        return houseAddressY;
    }

    public void setAddress(int x, int y)
    {
        houseAddressX = x;
        houseAddressY = y;
    }
}
