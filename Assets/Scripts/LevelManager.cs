using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int level;

    private AudioSource backgroundAudio;

    private List<Hub.Subscription> subs;

    void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
        backgroundAudio.Play();

        AddSubs();
    }

    private void AddSubs()
    {
        subs = new List<Hub.Subscription>();

        Hub.Subscribe(subs, EventConstants.GAME_OVER, delegate
        {
            backgroundAudio.Stop();
            SceneManager.LoadScene(SceneConstants.GAME_OVER, LoadSceneMode.Additive);
        });

        Hub.Subscribe(subs, EventConstants.LEVEL_COMPLETED, delegate
        {
            backgroundAudio.Stop();

            PlayerPrefs.SetInt(PlayerPrefsConstants.COMPLETED_LEVEL, level);

            SceneManager.LoadScene(SceneConstants.LEVEL_COMPLETED, LoadSceneMode.Additive);
        });

        Hub.Subscribe(subs, EventConstants.OPEN_MAP, delegate
        {
            SceneManager.LoadScene(SceneConstants.MAP, LoadSceneMode.Additive);
        });

        Hub.Subscribe(subs, EventConstants.CLOSE_MAP, delegate
        {
            SceneManager.UnloadSceneAsync(SceneConstants.MAP);
        });
    }

    private void OnDestroy()
    {
        Hub.Unsubsribe(subs);
    }

}