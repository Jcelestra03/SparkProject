using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<int> TList = new List<int>();
    public List<Vector3> Mason = new List<Vector3>();
    public List<int> MasonX = new List<int>();
    public List<int> MasonY = new List<int>();

    public SerializableDictionary<Vector3, int> newEntail;

    public GameData()
    {
        TList = new List<int>();
        Mason = new List<Vector3>();
        MasonX = new List<int>();
        MasonY = new List<int>();
        newEntail = new SerializableDictionary<Vector3, int>();
    }

    
}
