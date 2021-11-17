using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;

    private int blockchange;
    
    public int[] tile;
    public Vector3[] Tposition;

    public int next;

    private int count;
    private int max;
    public GameObject enemy;

    private void Start()
    {
        next = 0;
        blockchange = 2;

        Tposition = new Vector3 [10];
        tile = new int[10];
    }
    public void Update()
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
            Tposition[next] = TargetTilemap.CellToWorld(clickPosition);
            tile[next] = blockchange;
            next = next + 1;
            

            //Debug.Log(Tposition[next-1]);
            //Debug.Log(next);
        }      
        if(Input.GetKeyDown(KeyCode.T))
        {
            TileCheck();
        }
    }

    private void TileCheck()
    {
        int count = 0;
        
        while (count <= next-1)
        {
            if (tile[count] == 2)
            {
                GameObject emy = Instantiate(enemy);
                emy.GetComponent<Transform>().position = Tposition[count];
                Debug.Log(tile[next]);
            }
            count++;
        }
    }
 
}
