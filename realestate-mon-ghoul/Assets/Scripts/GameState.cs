using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private int funds = 0;
    [SerializeField] private int houseAddressX;
    [SerializeField] private int houseAddressY;
    [SerializeField] private int ghostLevel = 0;
    private Dictionary<Vector2, Tile> _tiles;
    [SerializeField] public bool playerAction = true;
    [SerializeField] private int rentIncome = 100;
    Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint) DateTime.UtcNow.Ticks);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_tiles == null)
        {
            _tiles = new Dictionary<Vector2, Tile>();
        }

        if (!playerAction)
        {
            // do some things
            // for each property calculate rent
            foreach (var tile in _tiles.Values)
            {
                if (tile.youOwnHouse)
                {
                    funds += rentIncome;
                    Debug.Log("Rental income");
                    int randomNumber = random.NextInt(1, 12);
                    if (randomNumber >= 2 && randomNumber <= 5)
                    {
                        // deduct some amount
                        Debug.Log("Deducting");
                        funds -= 10;

                        // TODO: this will later need to repose the house if it falls below zero
                    }
                }

                // Reduce house price
                if (tile.houseForSale)
                {
                    tile.reduceHousePrice(100);
                }

                // Reduce the haunt level of each house

                // Move the ghostdusters
            }


            // Now it is players turn
            playerAction = true;

        }
        
    }

    public void setGhostLevel(int level)
    {
        ghostLevel = level;
    }

    public int getGhostLevel()
    {
        return ghostLevel;
    }

    public void resetGhostLevel()
    {
        ghostLevel = 0;
    }
    public Tile getTile(int x, int y)
    {
        return _tiles[new Vector2 (x, y)];
    }

    public void addTile(int x, int y, Tile spawnedTile)
    {
        if (_tiles == null)
        {
            _tiles = new Dictionary<Vector2, Tile>();
        }
        _tiles[new Vector2(x, y)] = spawnedTile;
    }

    public int getFunds()
    {
        return funds;
    }

    public void deductFunds(int deduct)
    {
        funds = funds - deduct;
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
