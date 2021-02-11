using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    public Joystick joystick;

    public Weapon gun;
    public Weapon bomb;

    public GameObject key;

    public AudioSource pickupSound;
    public AudioSource fallSound;

    private bool isKeyAvailable = false;
    private FACING facing;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public FACING GetFacing()
    {
        return facing;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //float horizontal = joystick.Horizontal;
        //float vertical = joystick.Vertical;
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

        //Debug.Log("facing = " + facing);
        //Debug.Log("goes = " + goesTo);
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
            GameManager gm = FindObjectOfType<GameManager>();
            gm.GameOver();
        }
        else if (collObj.CompareTag("EXIT") && isKeyAvailable)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.Win();
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
                key.SetActive(true);
                isKeyAvailable = true;
                Debug.Log("Key picked up");
                break;
        }
        Destroy(obj);
    }

    public enum FACING
    {
        UP = 90, DOWN = 270, LEFT = 180, RIGHT = 0
    }
}
