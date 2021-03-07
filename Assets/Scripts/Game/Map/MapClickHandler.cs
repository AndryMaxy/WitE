using UnityEngine;

public class MapClickHandler : MonoBehaviour
{

    public void CloseMap()
    {
        Hub.Publish(EventConstants.CLOSE_MAP);
    }
}