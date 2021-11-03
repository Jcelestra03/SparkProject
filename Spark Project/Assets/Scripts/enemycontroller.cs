using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{

    private Rigidbody2D myRB;
    private Vector2 velocity;
    public float movementSpeed = 2;
    public bool isfollowing = false;
    public GameObject playertarget;







    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
