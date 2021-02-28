﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClickHandler : MonoBehaviour
{
    public void Shoot()
    {
        Hub.Publish(EventConstants.FIRE);
    }

    public void Kaboom()
    {
        Hub.Publish(EventConstants.KABOOM);
    }

    public void OpenMap()
    {
        Hub.Publish(EventConstants.OPEN_MAP);
    }
}