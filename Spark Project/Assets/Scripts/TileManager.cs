using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Tilemap))]

public class tilemanager : MonoBehaviour
{

    Tilemap tilemap;

    gridmanager grid;

    [SerializeField] TileBase tilebase;
    [SerializeField] TileBase tilebase2;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<gridmanager>();
        grid.Init(25, 12);
        Set(9, 1, true);
        UpdateTileMap();
    }

    void UpdateTileMap()
    {
        for(int x = 0; x < grid.width; x++)
        {
            for(int y = 0; y < grid.height; y++)
            {
                UpdateTile(x, y);
            }
        }
    }

    private void UpdateTile(int x, int y)
    {
        if (grid.Get(x, y) == true)
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), tilebase);
        }
        else
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), tilebase2);
        }
    }

    public void Set(int x, int y, bool to)
    {
        grid.Set(x, y, to);
        UpdateTile(x,y);
    }

}
