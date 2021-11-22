using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;

    private int blockchange;
    

    public Dictionary<Vector3,int> entail;
    List<Vector3> NumbersMason = new List<Vector3>();


    gridManager grid;
    
    private bool outof;
    private int count;
    private int max;
    
    public GameObject[] tileprefabs;

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

            Vector3 outbounds;
            outbounds = new Vector3 (0, 0, 0);

            
            if(clickPosition.x < 0 || clickPosition.x >= 100 || (clickPosition.y < 0 || clickPosition.y >= 100))
            {
                outof = true;
                Debug.Log(outof);
            }
            else
            {
                outof = false;
                Debug.Log(outof);
            }
            if(!outof)
            {
                if (entail.TryAdd(here, blockchange) == true)
                {
                    tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                    NumbersMason.Add(here);
                }
                else
                {
                    entail[here] = blockchange;
                    tiles.Set(clickPosition.x, clickPosition.y, blockchange);

                }
            }

        }      
        
    }

    private void TileCheck()
    {
        int count = 0;
        
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);

            
            Debug.Log(NumbersMason[count]);
            //Debug.Log(entail.ContainsValue(1));
            GameObject placed = Instantiate(tileprefabs[block]);
            Vector3 Posit;
            Posit = new Vector3(.5f, .5f, 0);
            placed.GetComponent<Transform>().position = NumbersMason[count]+Posit;
            
            count++;
        }
    }

    public void startb()
    {
        TileCheck();
    }
}
