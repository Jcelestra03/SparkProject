using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;

    private int blockchange;
    public int[] positionx;
    public int[] positiony;
    public int[] tile;

    public int next;

    public GameObject enemy;

    private void Start()
    {
        next = 0;
        blockchange = 2;
        positionx = new int[10];
        positiony = new int[10];
        tile = new int[10];
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            blockchange = blockchange + 1;
            if (blockchange >= 4)
            {
                blockchange = 1;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);
            tiles.Set(clickPosition.x, clickPosition.y, blockchange);
            positionx[next] = clickPosition.x;
            positiony[next] = clickPosition.y;
            tile[next] = blockchange;
            next = next + 1;

        }      
    }

    private void TileCheck()
    {
        next = 0;
        if ( tile[next] == 2 ) 
        {
            
        }
    }



}
