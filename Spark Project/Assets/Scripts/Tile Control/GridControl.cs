using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;

    private int blockchange;
    

    public Dictionary<Vector3,int> entail;

    //public int[] tile;
    //public Vector3[] Tposition;

    public int next;

    private int count;
    private int max;
    public GameObject enemy;

    private void Start()
    {
        next = 0;
        blockchange = 2;
        entail = new Dictionary<Vector3, int>();
        //Tposition = new Vector3 [10];
        //tile = new int[10];
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
            //tiles.Set(clickPosition.x, clickPosition.y, blockchange);
            //Tposition[next] = TargetTilemap.CellToWorld(clickPosition);
            Vector3 here = TargetTilemap.CellToWorld(clickPosition);
            Debug.Log(here);
            Debug.Log(blockchange);
            if (entail.TryAdd(here , blockchange) == true)
            {
                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
            }
            else
            {
                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                entail[here] = blockchange;
            }

            //tile[next] = blockchange;
            next = next + 1;
            

            //Debug.Log(Tposition[next-1]);
            //Debug.Log(next);
        }      
        if(Input.GetKeyDown(KeyCode.T))
        {
            int count = 0;
            while (count <= entail.Count -1)
            {
                
                count++;
                Debug.Log(count);
            }
            //TileCheck();
        }
    }

    private void TileCheck()
    {
        int count = 0;
        
        while (count <= entail.Count-1)
        {
            if (entail.ContainsValue(2))
            {
                GameObject emy = Instantiate(enemy);
                //emy.GetComponent<Transform>().position = Tposition[count];
            }
            count++;
        }
    }
 
}
