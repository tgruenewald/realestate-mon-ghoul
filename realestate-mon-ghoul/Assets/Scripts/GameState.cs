using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private int funds = 0;
    [SerializeField] private int houseAddressX;
    [SerializeField] private int houseAddressY;
    private Dictionary<Vector2, Tile> _tiles;

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
