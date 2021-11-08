using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tile Set")]
public class TileSet : ScriptableObject
{
    public List<TileBase> tiles;
}
