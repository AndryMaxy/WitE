using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    public Joystick joystick;

    public Text bombCountText;
    public Text gunCountText;
    public GameObject key;

    public AudioSource fireSound;
    public AudioSource kaboomSound;
    public AudioSource pickupSound;
    public AudioSource fallSound;

    private int gunCount = 0;
    private int bombCount = 0;

    private bool isStayWeakWall = false;
    private GameObject weakwall;

    private bool isStayEnemy = false;
    private Enemy enemy;

    private bool isKeyAvailable = false;

    private FACING facing;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facing = FACING.DOWN;
        printInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Fire();
        }

        if (Input.GetKeyDown("e"))
        {
            Kaboom();
        }
    }

    private void FixedUpdate()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Debug.Log("dir = " + joystick.Direction);
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

        Debug.Log("facing = " + facing);
        Debug.Log("goes = " + goesTo);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        var collObj = collision.gameObject;        
        if (collObj.CompareTag("WEAK_WALL"))
        {
            isStayWeakWall = true;
            weakwall = collObj;
        } 
        else if (collObj.CompareTag("ENEMY"))
        {
            isStayEnemy = true;
            enemy = collObj.GetComponent<Enemy>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var collObj = collision.gameObject;
        if (collObj.CompareTag("WEAK_WALL"))
        {
            isStayWeakWall = false;
            weakwall = null;
        }
        else if (collObj.CompareTag("ENEMY"))
        {
            isStayEnemy = false;
            enemy = null;
        }
    }

    private void handleInventory(GameObject obj)
    {
        pickupSound.Play();
        Inventory inventory = obj.GetComponent<Inventory>();
        switch (inventory.type)
        {
            case Inventory.Type.GUN: 
                gunCount += inventory.count;
                break;
            case Inventory.Type.BOMB:
                bombCount += inventory.count;
                break;
            case Inventory.Type.KEY:
                key.SetActive(true);
                isKeyAvailable = true;
                Debug.Log("Key picked up");
                break;
        }
        printInventory();
        Destroy(obj);
    }

    private void printInventory()
    {
        bombCountText.text = bombCount.ToString();
        gunCountText.text = gunCount.ToString();
    }

    public void Kaboom()
    {
        if (bombCount > 0 && isStayWeakWall)
        {
            kaboomSound.Play();
            --bombCount;
            Destroy(weakwall);
            weakwall = null;
            printInventory();
        }
    }

    public void Fire()
    {
        if (gunCount > 0 && isStayEnemy)
        {
            fireSound.Play();
            enemy.health = enemy.health - 1;
            --gunCount;
            if (enemy.health == 0)
            {
                Destroy(enemy.gameObject);
                enemy = null;
            }
 
        }
        printInventory();

    }

    enum FACING
    {
        UP = 90, DOWN = 270, LEFT = 180, RIGHT = 0
    }
}
