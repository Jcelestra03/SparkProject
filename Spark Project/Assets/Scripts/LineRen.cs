using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRen : MonoBehaviour
{
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {

    }
    public void PlacePoints(Vector3 first, Vector3 second)
    {
        Vector3 Posit;
        Posit = new Vector3(.5f, .5f, 0);
        lr.SetPosition(0, first + Posit);
        lr.SetPosition(1, second+ Posit);
        //needs a destroy function or off function
    }
}
