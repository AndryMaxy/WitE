using UnityEngine;
using System.Collections;

public class Bomb : Weapon
{

    public Player player;

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire2"))
        {
            Use();
        }
    }

    protected override void Apply()
    {
        Kaboom();
    }

    private void Kaboom()
    {
        GameObject target = FindTarget();

        IExplosive explosive = target.GetComponent<IExplosive>();
        explosive.explode();
    }

    protected override bool isUseable()
    {
        return base.isUseable() && FindTarget() != null;
    }

    private GameObject FindTarget()
    {
        int x = 0;
        int y = 0;
        var rotation = transform.rotation;
        if (rotation.y == 0 && rotation.z == 0)
        {
            x = 1;
            y = 0;
        }
        else if (rotation.y < 0 && rotation.z == 0)
        {
            x = -1;
            y = 0;
        }
        else if (rotation.y == 0 && rotation.z <= 0)
        {
            x = 0;
            y = -1;
        }
        else
        {
            x = 0;
            y = 1;
        }

        var pointX = transform.position.x + x * 0.85f;
        var pointY = transform.position.y + y * 0.85f;
        Vector2 bomb = new Vector2(transform.position.x, transform.position.y);
        Vector2 area = new Vector2(pointX, pointY);
        Collider2D[] coliders = Physics2D.OverlapAreaAll(bomb, area);
        foreach (var colider in coliders)
        {
            if (colider.gameObject.CompareTag(GameTags.WEAK_WALL.ToString()))
            {
                return colider.gameObject;
            }
        }

        return null;
    }
}