using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    void Start()
    {
        //int completedLvl = PlayerPrefs.GetInt(PlayerPrefsConstants.COMPLETED_LEVEL, 0);

    }


    public void LoadLevel(int number)
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
