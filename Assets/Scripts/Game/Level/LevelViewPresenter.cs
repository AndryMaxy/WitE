using UnityEngine;
using UnityEngine.UI;

public class LevelViewPresenter : EventBehaviour
{
    public Text gunCount;
    public Text bombCount;
    public GameObject key;

    void Start()
    {
        AddEvent(EventConstants.GOLD_KEY_PICKED, () => key.SetActive(true));
        AddEvent<int>(EventConstants.UPDATE_GUN_COUNT, count => gunCount.text = count.ToString());
        AddEvent<int>(EventConstants.UPDATE_BOMB_COUNT, count => bombCount.text = count.ToString());
    }
}