using UnityEngine;

public class PinchZoom : Zoom
{
    public float step = GameConstants.Camera.ZOOM_STEP;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMag = prevTouchDeltaMag - touchDeltaMag;

            float size =cam.orthographicSize + deltaMag * step;
            SetOrthoSize(size);
        }
    }
}