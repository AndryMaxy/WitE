using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{

    public void ToMenu()
    {
        SceneManager.LoadScene(SceneConstants.MENU);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(int number)
    {
        SceneManager.LoadScene(SceneConstants.LEVEL + number);
    }

    public void LoadNextLevel()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        bool isNextAvailable = curSceneIndex + 2 <= SceneManager.sceneCount;

        if (isNextAvailable)
        {
            SceneManager.LoadScene(curSceneIndex + 1);
        } 
        else
        {
            ToMenu();
        }
    }
}