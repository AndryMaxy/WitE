using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void LoadLevel(int number)
    {
        SceneManager.LoadScene(SceneConstants.LEVEL + number);
    }


    public void UnloadLastScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(SceneConstants.MENU);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        bool isNextAvailable = curSceneIndex + 2 <= SceneManager.sceneCountInBuildSettings;

        if (isNextAvailable)
        {
            SceneManager.LoadScene(curSceneIndex + 1);
        }
        else
        {
            ToMenu();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}