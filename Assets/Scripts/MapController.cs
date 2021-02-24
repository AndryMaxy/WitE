using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{

    public void Open()
    {
        Hub.Publish(EventConstants.OPEN_MAP);
    }

    public void Close()
    {
        Hub.Publish(EventConstants.CLOSE_MAP);
    }
}