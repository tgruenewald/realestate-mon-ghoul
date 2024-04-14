using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDuster
{
    int homeX = 0;
    int homeY = 0;
    Dictionary<int, int> xp = new Dictionary<int, int>();

    public void setHome(int x, int y)
    {
        homeX = x;
        homeY = y;
    }

    public bool isHome(Tile tile)
    {
        if (tile.x ==  homeX)
        {
            if (tile.y == homeY)
            {
                return true;
            }
        }
        return false;
    }

    public void increaseXP(int hauntLevel)
    {
        if (!xp.ContainsKey(hauntLevel))
        {
            xp.Add(hauntLevel, 1);
        } else
        {
            xp[hauntLevel] += 1;
        }

        Debug.Log("For " + hauntLevel + ", xp is " + xp[hauntLevel]);
    }

    public int getXP(int hauntLevel)
    {
        if (xp.ContainsKey(hauntLevel))
        {
            return xp[hauntLevel];
        }
        return 0;
    }

}
