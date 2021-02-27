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

        AddEvent(EventConstants.FIRE, () => Use()); ;
    }

    protected override void Apply()
    {
        Shoot();
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    protected override void FireUpdateEvent()
    {
        Hub.Publish(EventConstants.UPDATE_GUN_COUNT, count);
    }
}
