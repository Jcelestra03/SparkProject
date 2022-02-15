using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int burstDensity = 2;
    public float attackRate = 1;
    public float burstSpeed = 1;
    public GameObject bullet;
    public Transform shootPos;
    public AudioClip TurretFiring;
    public AudioClip Death;

    private int mag;
    private float coolDown;
    private float miniCoolDown;
    private AudioSource speaker;

    private void Start()
    {
        mag = burstDensity;
    }

    void Update()
    {
        if (miniCoolDown > 0)
            miniCoolDown -= burstSpeed * Time.deltaTime;

        if (coolDown > 0)
            coolDown -= attackRate * Time.deltaTime;

        if (coolDown <= 0)
        {
            if (mag > 0)
            {
                Shoot();
            }
            else if (mag <= 0)
            {
                coolDown = 1;
                mag = burstDensity;
            }
        }
    }

    void Shoot()
    {
        if (miniCoolDown <= 0)
        {
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = TurretFiring;
            GetComponent<AudioSource>().Play();

            Instantiate(bullet, shootPos);
            mag--;
            miniCoolDown = 0.1f;
        }
    }
}
