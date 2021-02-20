using UnityEngine;
using System.Collections;


public class PlayerRaycast : MonoBehaviour
{

    public LayerMask fogLayerMask;
    public LayerMask wallLayerMask;
    public float radius = 3f;
    public float wallCastdistance = 10f;
    public float fogCastdistance = 5f;

    // Use this for initialization
    void Start()
    {

    }

    private void Remove(Vector2 origin, Vector2 direction)
    {
        //RaycastHit2D[] hits = Physics2D.CapsuleCastAll(origin, radius, direction, distance, layerMask);
        //RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, distance, layerMask);

        //foreach (var hit in hits)
        //{
        //    if (hit.collider?.gameObject.GetComponent<FakeWall>())
        //    {
        //        Destroy(hit.collider.gameObject);
        //    }
        //}

    }

    void Update()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        Vector2 size;
        Vector2 dir;
        var rotation = transform.rotation;
        if ((rotation.y == 0 && rotation.z == 0))
        {
            size = new Vector2(5f, 2.5f);
            dir = Vector2.right;

        }
        else if ((rotation.y < 0 && rotation.z == 0))
        {
            size = new Vector2(5f, 2.5f);
            dir = Vector2.left;

        }
        else if (rotation.y == 0 && rotation.z <= 0)
        {
            size = new Vector2(2.5f, 5f);
            dir = Vector2.down;

        }
        else
        {
            size = new Vector2(2.5f, 5f);
            dir = Vector2.up;

        }

        RaycastHit2D wall = Physics2D.Raycast(origin, dir, wallCastdistance, wallLayerMask);
        Debug.Log("wall dist:" + wall.distance);

        float distance = wall.distance > 0 ? wall.distance : fogCastdistance;

        Debug.DrawRay(new Vector3(origin.x, origin.y), dir * distance, Color.red, 5f);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, dir, distance, fogLayerMask);

        foreach (var hit in hits)
        {
            Destroy(hit.collider?.gameObject);
        }

    }

}