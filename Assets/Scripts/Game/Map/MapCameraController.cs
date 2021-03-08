using UnityEngine;

public class MapCameraController : MonoBehaviour
{

    private Camera cam;

    private Transform target;
    private Vector3 dragOrigin;

    private float screenRatio;
    private float halfHeight;
    private float haldWidth;

    private Bounds bounds;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {

        target = FindObjectOfType<Player>().transform;

        cam = GetComponent<Camera>();
        cam.transform.position = new Vector3(target.position.x, target.position.y, cam.transform.position.z);

        GameObject boundsObj = GameObject.FindGameObjectWithTag(GameTags.BOUNDS.ToString());
        Collider2D boundsColider = boundsObj.GetComponent<PolygonCollider2D>();
        bounds = boundsColider.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;

        screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = bounds.size.x / bounds.size.y;

        float differenceInSize = targetRatio / screenRatio;
        cam.orthographicSize = bounds.size.y / 2 * differenceInSize;

        SetHalfs();
    }

    void Update()
    {
        ControlCamera();
    }

    private void ControlCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 dif = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += dif;
        }

        SetHalfs();
        float clampedX = Mathf.Clamp(cam.transform.position.x, minBounds.x + haldWidth, maxBounds.x - haldWidth);
        float clampedY = Mathf.Clamp(cam.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        cam.transform.position = new Vector3(clampedX, clampedY, cam.transform.position.z);
    }

    private void SetHalfs()
    {
        halfHeight = cam.orthographicSize;
        haldWidth = halfHeight * screenRatio;
    }
}