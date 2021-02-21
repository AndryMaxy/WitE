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


    void Update()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 diraction = Utils.DefineDiraction2D(transform.rotation);

        RaycastHit2D wall = Physics2D.Raycast(origin, diraction, wallCastdistance, wallLayerMask);

        float distance = wall.distance > 0 ? wall.distance : fogCastdistance;
        Debug.DrawRay(new Vector3(origin.x, origin.y), diraction * distance, Color.red, 5f);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, diraction, distance, fogLayerMask);
        foreach (var hit in hits)
        {
            Destroy(hit.collider?.gameObject);
        }

    }

}