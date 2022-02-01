using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int burstDensity = 2;
    public float attackRate = 1;
    public GameObject bullet;
    public Transform shootPos;

    public int mag;
    public float coolDown;

    private void Start()
    {
        mag = burstDensity;
    }

    void Update()
    {
        if (coolDown > 0)
            coolDown -= attackRate * Time.deltaTime;

        if (coolDown <= 0)
        {
            if (mag > 0)
            {
                Instantiate(bullet, shootPos);
                mag--;
            }
            else if (mag <= 0)
            {
                coolDown = 1;
                int mag = burstDensity;
            }
        }
    }
}
