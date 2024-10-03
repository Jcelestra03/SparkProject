using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//holds game data
[System.Serializable]
public class GameData
{

    public List<Vector3> Mason = new List<Vector3>();


    public SerializableDictionary<Vector3, int> newEntail;

    public GameData()
    {

        Mason = new List<Vector3>();

        newEntail = new SerializableDictionary<Vector3, int>();
    }

    
}
