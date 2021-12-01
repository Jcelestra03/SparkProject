using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroythis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name.Contains("killme"))
        {
            Death();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<GridControl>().editing == true)
        {
            Debug.Log("Long Live ");
            Death();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
