﻿using UnityEngine;
using System;

public class Player : EventBehaviour
{
    public Joystick joystick;

    public Weapon gun;
    public Weapon bomb;

    public AudioSource pickupSound;
    public AudioSource fallSound;

    private bool isKeyAvailable = false;
    private FACING facing;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        AddEvents();
    }


    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            gun.Use();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            bomb.Use();
        }
    }

    public FACING GetFacing()
    {
        return facing;
    }

    private void FixedUpdate()
    {
        float horizontal = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Input.GetAxis("Horizontal");
        } else
        {
            horizontal = joystick.Horizontal;
        }


        float vertical = 0;
        if (Input.GetAxis("Vertical") != 0)
        {
            vertical = Input.GetAxis("Vertical");
        } else
        {
            vertical = joystick.Vertical;
        }
        rb.velocity = new Vector2(horizontal * 4f, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, vertical * 4f);

        var goesTo = facing;
        if (horizontal > 0.2)
        {
            goesTo = FACING.RIGHT;
        } else if (horizontal < -0.2)
        {
            goesTo = FACING.LEFT;

        }
        if (vertical > 0.2)
        {
            goesTo = FACING.UP;
        }
        else if (vertical < -0.2)
        {
            goesTo = FACING.DOWN;

        }

        if (goesTo != facing)
        {
            Vector3 vector = new Vector3(0, 0, (float)goesTo);
            if (goesTo == FACING.LEFT)
            {
                vector.z = 0;
                vector.y = 180;
            }
            transform.rotation = Quaternion.Euler(vector);
            facing = goesTo;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collObj = collision.gameObject;
        bool isInventory = Enum.IsDefined(typeof(Inventory.Type), collObj.tag);
        if (isInventory)
        {
            handleInventory(collObj);
        } 
        else if (collObj.CompareTag("HOLE"))
        {
            fallSound.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Hub.Publish(EventConstants.GAME_OVER);
        }
        else if (collObj.CompareTag("EXIT") && isKeyAvailable)
        {
            Hub.Publish(EventConstants.LEVEL_COMPLETED);
        }
    }

    private void handleInventory(GameObject obj)
    {
        pickupSound.Play();
        Inventory inventory = obj.GetComponent<Inventory>();
        switch (inventory.type)
        {
            case Inventory.Type.GUN:
                gun.Add(inventory.count);
                break;
            case Inventory.Type.BOMB:
                bomb.Add(inventory.count);
                break;
            case Inventory.Type.KEY:
                isKeyAvailable = true;
                Hub.Publish(EventConstants.GOLD_KEY_PICKED);
                break;
        }
        Destroy(obj);
    }

    public void Disable()
    {
        enabled = false;
        rb.velocity = new Vector2(0f, 0f);
    }

    public void Enable()
    {
        enabled = true;
    }

    private void AddEvents()
    {
    }

    public enum FACING
    {
        UP = 90, DOWN = 270, LEFT = 180, RIGHT = 0
    }
}
