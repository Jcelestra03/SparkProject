using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dropblock : MonoBehaviour
{
    public int Block;
    public Dropdown myD;

    public void blockchange()
    {
        Block = myD.value;
    }
}
