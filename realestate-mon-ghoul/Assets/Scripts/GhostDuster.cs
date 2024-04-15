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

    private int maxOutXP(int hauntLevel)
    {
        int exper = xp[hauntLevel];
        switch(hauntLevel)
        {
            case 10:
                if (exper > 10)
                {
                    return 10;
                } else
                {
                    return exper;
                }
            case 100:
                if (exper > 5)
                {
                    return 5;
                }
                else
                {
                    return exper;
                }
            case 1000:
                if (exper > 3)
                {
                    return 3;
                }
                else
                {
                    return exper;
                }
        }
        return exper;
    }

    public int getXP(int hauntLevel)
    {
        if (xp.ContainsKey(hauntLevel))
        {
            return maxOutXP(hauntLevel);
        }
        return 0;
    }

}
