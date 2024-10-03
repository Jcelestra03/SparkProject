using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataPersistence
{
    void LoadData(GameData data);

    void Saved(ref GameData data);
}
