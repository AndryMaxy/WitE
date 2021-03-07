using UnityEngine;


public class Zoom : MonoBehaviour
{

    protected Camera cam;
    protected float maxZoom;

    void Start()
    {
        cam = GetComponent<Camera>();
        maxZoom = PlayerPrefs.GetFloat(PlayerPrefsConstants.OPTIMAL_ORTHO_Size, GameConstants.Camera.MAX_ZOOM);
    }

    protected void SetOrthoSize(float size)
    {
        cam.orthographicSize = Mathf.Clamp(size, GameConstants.Camera.MIN_ZOOM, maxZoom);
    }
}