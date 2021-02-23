using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Level : MonoBehaviour
{
    public int level;
    public GameObject levelCompleted;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        int completedLvl = PlayerPrefs.GetInt(PlayerPrefsConstants.COMPLETED_LEVEL, 0);
        if (completedLvl >= level)
        {
            Instantiate(levelCompleted, gameObject.transform);
        }

        int availableLvl = completedLvl + 1;
        if (level <= completedLvl || availableLvl == level)
        {
            button.interactable = true;
        }
    }

}