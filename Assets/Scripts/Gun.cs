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
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
