using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;

    private int blockchange;

    private void Start()
    {

        blockchange = 2;
    }
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            blockchange = blockchange + 1;
            if(blockchange >= 4)
            {
                blockchange = 1;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);
            tiles.Set(clickPosition.x, clickPosition.y, blockchange);
        }

    }
}
