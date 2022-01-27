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

    public Dictionary<Vector3, int> portals;

    gridManager grid;
    
    private bool outof;
    private int count;

    public bool editing;

    public GameObject[] tileprefabs;

    
    private void Start()
    {
        
        blockchange = 2;
        entail = new Dictionary<Vector3, int>();
        NumbersMason = new List<Vector3>();
        portals = new Dictionary<Vector3, int>();
        editing = true;
    }
    public void Update()
    {

        //
        //if (editing == true)
        //{
            if (Input.GetKeyDown(KeyCode.R))
            {

                blockchange = blockchange + 1;
                if (blockchange >= tileprefabs.Length)
                {
                    blockchange = 0;
                }
                Debug.Log(tileprefabs[blockchange]);
            }

            if (Input.GetMouseButtonDown(0))
            {

                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);

                Vector3 here = TargetTilemap.CellToWorld(clickPosition);

                Vector3 outbounds;
                outbounds = new Vector3(0, 0, 0);


                if (clickPosition.x < 0 || clickPosition.x >= 100 || (clickPosition.y < 0 || clickPosition.y >= 100))
                {
                    outof = true;
                    Debug.Log(outof);
                }
                else
                {
                    outof = false;
                    //Debug.Log(outof);
                }
                if (!outof)
                {
                    if (entail.TryAdd(here, blockchange) == true)
                    {
                        if (blockchange == 7)
                        {
                            //if(2 portals exist) - UI menu(portal) is avalible;
                            // list or dictoary place key, leave value open/null;

                            //Force camera to spot World to cell 
                            //UI choose partner 
                            //dictionary.portal-ADD(here, int"partner")
                        }
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
        //}
        //
        //dropper(copy)
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);

            Vector3 here = TargetTilemap.CellToWorld(clickPosition);

            entail.TryGetValue(here, out int block);
            blockchange = block;
            Debug.Log(tileprefabs[blockchange]);
        }
        if ( Input.GetKeyDown(KeyCode.T))
        {
            //portalcheck();
            startb();
        }
    }

    //private void portalcheck()
    //{
    //    int count = 0;
    //    while (count <= NumbersMason.Count-1)
    //    {
    //        entail.TryGetValue(NumbersMason[count], out int block);
    //        if(block == 7)
    //        {
    //            Debug.Log("true");
    //        }
    //        count++;
    //    }
    //}

    private void TileCheck()
    {
        int count = 0;
        
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);

            //if(block == 7)
            //{
            //    portals.TryAdd(NumbersMason[count], )
            //}
            //Debug.Log(NumbersMason[count]);
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
        if (editing == false)
        {
            editing = true;
            //Self Destruction 
        }
        else
        {
            editing = false;
        }
    }

}
