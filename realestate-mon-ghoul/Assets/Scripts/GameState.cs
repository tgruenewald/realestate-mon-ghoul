using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [SerializeField] private int rentIncome = 20;
    Queue<Tile> hauntedHouses = new Queue<Tile>();
    Tile targetedHouse = null;
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
                        funds -= 50;

                        // TODO: this will later need to repose the house if it falls below zero
                        if (funds <= 0)
                        {
                            funds = 0;
                            // reposses house
                            tile.houseOffMarket();
                        }
                    }
                }

                // Reduce house price
                if (tile.houseForSale)
                {
                    tile.reduceHousePrice(10);
                }

                // Reduce the haunt level of each house


                    
            }

            StartCoroutine(ghostDusterTurn());


            // Now it is players turn
            playerAction = true;

        }
        
    }

    IEnumerator ghostDusterTurn()
    {
        int moves = random.NextInt(1, 6);
        for (int i = 0; i < moves; i++)
        {

            Debug.Log("Starting turn: " + i + " of " + moves);

            // Move the ghostdusters
            if (hauntedHouses.Count > 0 && targetedHouse == null)
            {
                // target the next house
                targetedHouse = pullOffNextHauntedHouse();
                Debug.Log("Targeted house is " + targetedHouse.x + ", " + targetedHouse.y);
            }


            if (targetedHouse == null)
            {
                // return home
                Debug.Log("Heading home");
                Tile ghostDuster = findGhostDuster();
                moveToCloserHouse(ghostDuster, getTile(0, 0));
            }

            if (targetedHouse != null && !targetedHouse.houseIsHaunted)
            {
                // house is no longer haunted (possibly bought)
                targetedHouse = null;
            }

            if (targetedHouse != null && targetedHouse.houseIsHaunted && !targetedHouse.ghostDusterAreThere)
            {
                // move ghost dusters closer
                Debug.Log("Moving closer");
                Tile ghostDuster = findGhostDuster();
                moveToCloserHouse(ghostDuster, targetedHouse);
            }

            if (targetedHouse != null && targetedHouse.houseIsHaunted && targetedHouse.ghostDusterAreThere)
            {
                Debug.Log("Exorcise the house");
                int randomNumber = random.NextInt(1, 20);
                if (targetedHouse.exorcise(randomNumber))
                {
                    targetedHouse = null;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }


    private void moveToCloserHouse(Tile ghostDuster, Tile hauntedHouse)
    {
        int nextX = System.Math.Abs(ghostDuster.x - hauntedHouse.x);
        int dirX = ghostDuster.x - hauntedHouse.x ;
        if (nextX > 0)
        {
            if (dirX > 0)
            {
                // move to the left
                Tile newTile = getTile(ghostDuster.x - 1, ghostDuster.y);
                newTile.setGhostDuster();
                ghostDuster.removeGhostDuster();
            } else
            {
                // move to the right
                Tile newTile = getTile(ghostDuster.x + 1, ghostDuster.y);
                newTile.setGhostDuster();
                ghostDuster.removeGhostDuster();
            }
            return;
        }
        Debug.Log("Moving to y since x diff is zero: " + nextX);

        int nextY = System.Math.Abs(ghostDuster.y - hauntedHouse.y);
        int dirY = ghostDuster.y - hauntedHouse.y ;
        if (nextY > 0)
        {
            if (dirY > 0)
            {
                // move down
                Tile newTile = getTile(ghostDuster.x, ghostDuster.y - 1);
                newTile.setGhostDuster();
                ghostDuster.removeGhostDuster();
            }
            else
            {
                // move up
                Tile newTile = getTile(ghostDuster.x, ghostDuster.y + 1);
                newTile.setGhostDuster();
                ghostDuster.removeGhostDuster();
            }
        }

    }

    private Tile findGhostDuster()
    {
        Tile ghostDuster = null;
        foreach (var tile in _tiles.Values)
        {
            if (tile.ghostDusterAreThere)
            {
                ghostDuster = tile;
                break;
            }
        }   
        return ghostDuster;
    }

    public void hauntHouse(Tile houseToHaunt)
    {
        hauntedHouses.Enqueue(houseToHaunt);
    }

    public Tile pullOffNextHauntedHouse()
    {
        return hauntedHouses.Dequeue();
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
