using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class dropblock : MonoBehaviour
{
    public int Block;
    public TMP_Dropdown myD;

    //public void blockchange()
    //{
    //    Block = myD.value;
    //}
    public TMP_Dropdown.DropdownEvent OnValueChanged()
    {
        Block = myD.value;
        return null;
    }
}
