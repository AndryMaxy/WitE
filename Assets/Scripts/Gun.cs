using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    public Transform firePoint;
    public Bullet bullet;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Apply()
    {
        Fire();
    }

    void Fire()
    {
        Debug.Log("fire point position: " + firePoint.position);
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
