using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class background : MonoBehaviour
{
    private int height;
    private int width;

    bool[,] grid;

    public void Init(int width, int height)
    {
        grid = new int[width, height];
        this.width = width;
        this.height = height;
    }

    public void Set(int x, int y, bool to)
    {
        if(CheckPosition(x,y) == false) { return; }
        {

        }
        grid[x, y] = to;
    }

    public int Get(int x, int y)
    {
        if(CheckPosition(x,y) == false)
        {
            return false;
        }
        return grid[x, y];
    }

    public bool CheckPosition(int x, int y)
    {
        if(x < 0 || x >= width)
        {
            return false;
        }
        if(y < 0 || y >= height)
        {
            return false;
        }
        return true;
    }
}
