using UnityEngine;

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
        Vector2 diraction = Utils.DefineDiraction2D(transform.rotation); //just a direction of a ray

        //trying to raycast a wall
        RaycastHit2D wall = Physics2D.Raycast(origin, diraction, wallCastdistance, wallLayerMask);

        //if the ray touched a wall then take the distance of the ray otherwise set any default value
        float distance = wall.distance > 0 ? wall.distance : fogCastdistance;
        Debug.DrawRay(new Vector3(origin.x, origin.y), diraction * distance, Color.red, 5f);

        //looking for fog objects on the specified distance and destroy them
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, diraction, distance, fogLayerMask);
        foreach (var hit in hits)
        {
            Destroy(hit.collider?.gameObject);
        }

    }

}