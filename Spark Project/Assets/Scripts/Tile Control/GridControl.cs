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
    List<Vector3> NumbersMason = new List<Vector3>();

    


    private int count;
    private int max;
    public GameObject enemy;

    private void Start()
    {
        
        blockchange = 2;
        entail = new Dictionary<Vector3, int>();
        NumbersMason = new List<Vector3>();
        
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
            
            Vector3 here = TargetTilemap.CellToWorld(clickPosition);
            
            //Debug.Log(here);
            //Debug.Log(blockchange);
            if (entail.TryAdd(here , blockchange) == true)
            {
                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                NumbersMason.Add(here);
            }
            else
            {
                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                entail[here] = blockchange;
                NumbersMason.Add(here);
            }

            //tile[next] = blockchange;
            
            

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
        
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);

            if (block == 2)
            {
                GameObject emy = Instantiate(enemy);
                emy.GetComponent<Transform>().position = NumbersMason[count];
            }
            count++;
        }
    }
 
}
