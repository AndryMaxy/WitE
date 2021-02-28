﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : EventBehaviour
{
    public int level;

    private AudioSource backgroundAudio;

    void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
        backgroundAudio.Play();

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
}