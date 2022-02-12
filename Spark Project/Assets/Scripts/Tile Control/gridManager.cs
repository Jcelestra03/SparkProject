using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    public int height;
    public int width;

    int[,] grid;
    private void Awake()
    {
        //Init(width, height);
    }
    private void Start()
    {
        //Init(width, height);
    }
    public void Init(int width, int height)
    {
        grid = new int[width, height];
        this.width = width;
        this.height = height;
    }


    public void Set(int x, int y, int to)
    {
        if (CheckPosition(x, y) == false) { return; }
        {
            
        }
        grid[x, y] = to;
    }

    public int Get(int x, int y)
    {
        if (CheckPosition(x, y) == false)
        {
            return -1;
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
