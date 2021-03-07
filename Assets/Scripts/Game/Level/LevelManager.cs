using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : EventBehaviour
{
    public int level;
    public Collider2D bounds;

    private AudioSource backgroundAudio;

    void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
        backgroundAudio.Play();

        SetOptimalOrthoSize();

        AddSubs();
    }

    private void AddSubs()
    {
        AddEvent(EventConstants.GAME_OVER, () =>
         {
             backgroundAudio.Stop();
             SceneManager.LoadScene(SceneConstants.GAME_OVER, LoadSceneMode.Additive);
         });

        AddEvent(EventConstants.LEVEL_COMPLETED, () =>
        {
            backgroundAudio.Stop();

            PlayerPrefs.SetInt(PlayerPrefsConstants.COMPLETED_LEVEL, level);

            SceneManager.LoadScene(SceneConstants.LEVEL_COMPLETED, LoadSceneMode.Additive);
        });

        AddEvent(EventConstants.OPEN_MAP, () =>
            SceneManager.LoadScene(SceneConstants.MAP, LoadSceneMode.Additive)
        );

        AddEvent(EventConstants.CLOSE_MAP, () =>
            SceneManager.UnloadSceneAsync(SceneConstants.MAP)
        );
    }

    private void SetOptimalOrthoSize()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = bounds.bounds.size.x / bounds.bounds.size.y;

        float differenceInSize = targetRatio / screenRatio;
        float optimalSize = bounds.bounds.size.y / 2 * differenceInSize - 0.1f;

        PlayerPrefs.SetFloat(PlayerPrefsConstants.OPTIMAL_ORTHO_Size, optimalSize);
    }
}