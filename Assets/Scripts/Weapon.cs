using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public abstract class Weapon : EventBehaviour
{
    public int count = 0;

    private AudioSource sound;

    protected virtual void Start()
    {
        subs = new List<Hub.Subscription>();

        sound = GetComponent<AudioSource>();
        FireUpdateEvent();
    }

    protected virtual void Update()
    {

    }

    public void Add(int count)
    {
        this.count += count;
        FireUpdateEvent();
    }

    public void Use()
    {
        if(isUseable())
        {
            sound.Play();
            Apply();
            --count;
            FireUpdateEvent();
        }
 
    }
    protected abstract void Apply();

    protected virtual bool isUseable()
    {
        return count > 0;
    }

    protected abstract void FireUpdateEvent();
}
