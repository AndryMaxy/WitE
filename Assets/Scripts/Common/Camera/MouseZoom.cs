using UnityEngine;

public class MouseZoom : Zoom
{
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float newSize = cam.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * -1;
            
            Vector3 oldMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            SetOrthoSize(newSize);
            Vector3 newMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 posDiff = oldMousePos - newMousePos;

            Vector3 camPos = cam.transform.position;
            Vector3 targetPos = new Vector3(
                camPos.x + posDiff.x,
                camPos.y + posDiff.y,
                camPos.z);

            cam.transform.position = targetPos;
        }
    }
}